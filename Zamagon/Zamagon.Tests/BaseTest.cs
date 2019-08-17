using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;
using Autofac;
using Zamagon.Domain;
using Zamagon.Domain.BackOffice;
using Zamagon.Domain.StoreFront;

namespace Zamagon.Tests
{
    public abstract class BaseTest
    {
        protected IContainer Container { get; set; }
        protected IEnumerable<IEndPointConfiguration> EndPoints { get; set; }
        protected IAdaptiveClient<IBOServiceManifest> BOServiceClient;
        protected IAdaptiveClient<ISFServiceManifest> SFServiceClient;
        protected IDatabaseUtilities DatabaseUtilities;
        protected readonly string CurrentDatabaseProviderName;

        public BaseTest(string databaseProviderName)
        {
            CurrentDatabaseProviderName = databaseProviderName;  // passed in by NUnit based on TestFixture attribute
        }

        protected async Task DropAndRecreateDatabase(IEndPointConfiguration ep)
        {
            if (ep.EndPointType != EndPointType.DBMS)
                return;

            await DatabaseUtilities.DropDatabase(ep);
            await DatabaseUtilities.ApplyMigrations(ep);
        }

        protected async Task CreateTestArtifacts()
        {
            EndPoints = EndPointUtilities.LoadEndPoints("EndPoints.json");
            if (EndPoints.Any(x => x.ProviderName == DataBaseProviderName.MySQL))
            {
                EndPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(EndPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
                EndPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(EndPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
            }
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.AutofacModule());
            RegistrationHelper registrationHelper = new RegistrationHelper(builder);
            registrationHelper.RegisterEndPoints(EndPoints);
            registrationHelper.RegisterModule(new Zamagon.Services.BackOffice.AdaptiveClientModule());
            registrationHelper.RegisterModule(new Zamagon.Services.Common.AdaptiveClientModule());
            registrationHelper.RegisterModule(new Zamagon.Services.StoreFront.AdaptiveClientModule());
            Container = builder.Build();
            BOServiceClient = Container.Resolve<IAdaptiveClient<IBOServiceManifest>>();
            SFServiceClient = Container.Resolve<IAdaptiveClient<ISFServiceManifest>>();
            DatabaseUtilities = Container.Resolve<IDatabaseUtilities>();
        }
    }
}
