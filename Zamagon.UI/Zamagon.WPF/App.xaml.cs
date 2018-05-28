using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFramework;
using Zamagon.Domain;

namespace Zamagon.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IEnumerable<IEndPointConfiguration> endPoints = ReadEndPointsFromDisk();
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacModule());
            builder.RegisterModule(new LeaderAnalytics.AdaptiveClient.EntityFramework.AutofacModule());
            RegistrationHelper registrationHelper = new RegistrationHelper(builder);
            

            registrationHelper
                .RegisterEndPoints(endPoints)
                .RegisterModule(new Zamagon.Services.Common.AdaptiveClientModule())
                .RegisterModule(new Zamagon.Services.BackOffice.AdaptiveClientModule())
                .RegisterModule(new Zamagon.Services.StoreFront.AdaptiveClientModule());


            var container = builder.Build();

            // Create all databases or apply migrations
            foreach (IEndPointConfiguration ep in endPoints.Where(x => x.EndPointType == EndPointType.DBMS))
            {
                //
                // Always resolve a new instance of databaseUtilities for each endPoint!
                //
                using (ILifetimeScope scope = container.BeginLifetimeScope())
                {
                    IDatabaseUtilities databaseUtilities = scope.Resolve<IDatabaseUtilities>();
                    Task.Run(() => databaseUtilities.CreateOrUpdateDatabase(ep)).Wait();
                }
            }

            MainWindow mainWindow = container.Resolve<MainWindow>();
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
    }
}
