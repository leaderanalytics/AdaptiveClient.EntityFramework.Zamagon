namespace Zamagon.API;

public class AutofacModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        // Autofac & AdaptiveClient
        List<IEndPointConfiguration> endPoints = EndPointUtilities.LoadEndPoints(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json")).ToList();
        IEndPointConfiguration backOffice = endPoints.FirstOrDefault(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL);
        IEndPointConfiguration frontOffice = endPoints.FirstOrDefault(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL);

        if (backOffice != null)
            backOffice.ConnectionString = ConnectionstringUtility.BuildConnectionString(backOffice.ConnectionString);

        if (frontOffice != null)
            frontOffice.ConnectionString = ConnectionstringUtility.BuildConnectionString(frontOffice.ConnectionString);

        builder.RegisterModule(new LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.AutofacModule());
        builder.RegisterInstance(endPoints).SingleInstance();
        RegistrationHelper registrationHelper = new RegistrationHelper(builder);

        registrationHelper
            .RegisterEndPoints(endPoints)
            .RegisterModule(new Zamagon.Services.Common.AdaptiveClientModule())
            .RegisterModule(new Zamagon.Services.BackOffice.AdaptiveClientModule())
            .RegisterModule(new Zamagon.Services.StoreFront.AdaptiveClientModule());
    }
}
