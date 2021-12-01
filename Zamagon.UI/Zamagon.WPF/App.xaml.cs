namespace Zamagon.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    // This container is used to resolve ViewModels since WPF does not allow them to be injected into views using normal
    // constructor injection.  AdaptiveClient uses constructor injection and does not require access to the container to be 
    // used properly.  Please don't write me an email about service locater anti-pattern. Thank you!! :)
    public Autofac.IContainer Container { get; private set; }


    private void Application_Startup(object sender, StartupEventArgs e)
    {
        DispatcherUnhandledException += App_DispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        System.Threading.Tasks.TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        IEnumerable<IEndPointConfiguration> endPoints = ReadEndPointsFromDisk();
        Container = App.CreateContainer(endPoints, null, null);
        IDatabaseUtilities databaseUtilities = Container.Resolve<IDatabaseUtilities>();

        // Create all databases or apply migrations
        foreach (IEndPointConfiguration ep in endPoints.Where(x => x.EndPointType == EndPointType.DBMS))
            Task.Run(() => databaseUtilities.CreateOrUpdateDatabase(ep)).Wait();

        MainWindow mainWindow = Container.Resolve<MainWindow>();
        this.MainWindow = mainWindow;
        mainWindow.Show();
    }

    private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        // We get here if AdaptiveClient is unable to make a connection and runs out of EndPoints.
    }

    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {

    }

    private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {

    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {

    }

    public static IEnumerable<IEndPointConfiguration> ReadEndPointsFromDisk()
    {
        List<IEndPointConfiguration> endPoints = EndPointUtilities.LoadEndPoints(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json")).ToList();
        IEndPointConfiguration backOffice = endPoints.FirstOrDefault(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL);
        IEndPointConfiguration frontOffice = endPoints.FirstOrDefault(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL);

        if (backOffice != null)
            backOffice.ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);

        if (frontOffice != null)
            frontOffice.ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);

        return endPoints;
    }

    public static Autofac.IContainer CreateContainer(IEnumerable<IEndPointConfiguration> endPoints, string apiName, Action<string> logger)
    {
        /*
         AdaptiveClient filters the collection of EndPoints that is registered and only uses the ones where IsActive = true. 
         However, for this demo app we want to force AdaptiveClient to encounter EndPoints that throw exceptions.
         To do this we create a copy of the EndPoints list (fakeEndPoints) and set IsActive to true for each EndPoint in the copied list.
         This effectively fools AdaptiveClient into thinking that all EndPoints are valid and should be used.
         At the end of this method we look in the original EndPoint list and create service mocks for EndPoints where IsActive = false.
         The resulting functionality is that AdaptiveClient attempts to use every EndPoint that is registered.  When it encounters a mock
         it fails and tries to use the next EndPoint. 
        */

        List<IEndPointConfiguration> fakeEndPoints = new List<IEndPointConfiguration>();

        foreach (IEndPointConfiguration ep in endPoints)
            fakeEndPoints.Add(new PresentationEndPoint(ep) { IsActive = true });

        ContainerBuilder builder = new ContainerBuilder();
        builder.RegisterModule(new AutofacModule());
        builder.RegisterModule(new LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.AutofacModule());
        RegistrationHelper registrationHelper = new RegistrationHelper(builder);

        registrationHelper
            .RegisterEndPoints(fakeEndPoints)
            .RegisterModule(new Zamagon.Services.Common.AdaptiveClientModule());

        if (apiName == API_Name.BackOffice || apiName == null)
            registrationHelper.RegisterModule(new Zamagon.Services.BackOffice.AdaptiveClientModule());

        if (apiName == API_Name.StoreFront || apiName == null)
            registrationHelper.RegisterModule(new Zamagon.Services.StoreFront.AdaptiveClientModule());


        if (logger != null)
            registrationHelper.RegisterLogger(logger);

        // Create mocks
        Mock<IOrdersService> ordersServiceMock = new Mock<IOrdersService>();
        ordersServiceMock.Setup(x => x.GetOrders()).Throws(new Exception("Mock failure"));
        IOrdersService ordersService = ordersServiceMock.Object;

        Mock<IProductsService> productsServiceMock = new Mock<IProductsService>();
        productsServiceMock.Setup(x => x.GetProducts()).Throws(new Exception("Mock failure"));
        IProductsService productsService = productsServiceMock.Object;

        Mock<IEmployeesService> empServiceMock = new Mock<IEmployeesService>();
        empServiceMock.Setup(x => x.GetEmployees()).Throws(new Exception("Mock failure"));
        IEmployeesService empService = empServiceMock.Object;

        Mock<ITimeCardsService> tcServiceMock = new Mock<ITimeCardsService>();
        tcServiceMock.Setup(x => x.GetTimeCards()).Throws(new Exception("Mock failure"));
        ITimeCardsService tcService = tcServiceMock.Object;

        // look at the list of endPoints that was passed to us and register a mock for each
        // one that is not marked active

        foreach (IEndPointConfiguration ep in endPoints.Where(x => !x.IsActive))
        {
            builder.RegisterInstance(ordersService).Keyed<IOrdersService>(ep.EndPointType + ep.ProviderName);
            builder.RegisterInstance(productsService).Keyed<IProductsService>(ep.EndPointType + ep.ProviderName);
            builder.RegisterInstance(empService).Keyed<IEmployeesService>(ep.EndPointType + ep.ProviderName);
            builder.RegisterInstance(tcService).Keyed<ITimeCardsService>(ep.EndPointType + ep.ProviderName);
        }

        return builder.Build();
    }


    //    This is an example of how a container might be built in a real application.
    //    This method should be called once only from Application_Startup.

    //public static IContainer CreateContainer(IEnumerable<IEndPointConfiguration> endPoints, Action<string> logger)
    //{
    //    ContainerBuilder builder = new ContainerBuilder();
    //    builder.RegisterModule(new AutofacModule());
    //    builder.RegisterModule(new LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.AutofacModule());
    //    RegistrationHelper registrationHelper = new RegistrationHelper(builder);

    //    registrationHelper
    //        .RegisterEndPoints(endPoints)
    //        .RegisterModule(new Zamagon.Services.Common.AdaptiveClientModule())
    //        .RegisterModule(new Zamagon.Services.BackOffice.AdaptiveClientModule())
    //        .RegisterModule(new Zamagon.Services.StoreFront.AdaptiveClientModule());


    //    if (logger != null)
    //        registrationHelper.RegisterLogger(logger);

    //    return builder.Build();
    //}
}
