using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;
using Zamagon.Domain;

namespace Zamagon.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // This container is used to resolve ViewModels since WPF does not allow them to be injected into views using normal
        // constructor injection.  AdaptiveClient uses constructor injection and does not require access to the container to be 
        // used properly.  Please don't write me an email about service locator anti-pattern. Thank you!! :)
        public IContainer Container { get; private set; } 

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IEnumerable<IEndPointConfiguration> endPoints = ReadEndPointsFromDisk();
            Container = App.CreateContainer(endPoints);
            IDatabaseUtilities databaseUtilities = Container.Resolve<IDatabaseUtilities>();

            // Create all databases or apply migrations
            foreach (IEndPointConfiguration ep in endPoints.Where(x => x.EndPointType == EndPointType.DBMS))
                Task.Run(() => databaseUtilities.CreateOrUpdateDatabase(ep)).Wait();

            MainWindow mainWindow = Container.Resolve<MainWindow>();
            this.MainWindow = mainWindow;
            mainWindow.Show();
        }

        public static IEnumerable<IEndPointConfiguration> ReadEndPointsFromDisk()
        {
            IEnumerable<IEndPointConfiguration> endPoints = EndPointUtilities.LoadEndPoints("EndPoints.json");
            endPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
            endPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
            return endPoints;
        }

        public static IContainer CreateContainer(IEnumerable<IEndPointConfiguration> endPoints)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacModule());
            builder.RegisterModule(new LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.AutofacModule());
            RegistrationHelper registrationHelper = new RegistrationHelper(builder);


            registrationHelper
                .RegisterEndPoints(endPoints)
                .RegisterModule(new Zamagon.Services.Common.AdaptiveClientModule())
                .RegisterModule(new Zamagon.Services.BackOffice.AdaptiveClientModule())
                .RegisterModule(new Zamagon.Services.StoreFront.AdaptiveClientModule());


            return builder.Build();
        }
    }
}
