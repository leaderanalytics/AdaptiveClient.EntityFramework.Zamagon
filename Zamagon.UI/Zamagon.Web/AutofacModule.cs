using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;
using System.Reflection;
using System.IO;

namespace Zamagon.Web
{
    public class AutofacModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Autofac & AdaptiveClient
            IEnumerable<IEndPointConfiguration> endPoints = ReadEndPointsFromDisk();
            builder.RegisterModule(new LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.AutofacModule());
            RegistrationHelper registrationHelper = new RegistrationHelper(builder);

            registrationHelper
                .RegisterEndPoints(endPoints)
                .RegisterModule(new Zamagon.Services.Common.AdaptiveClientModule())
                .RegisterModule(new Zamagon.Services.BackOffice.AdaptiveClientModule())
                .RegisterModule(new Zamagon.Services.StoreFront.AdaptiveClientModule());

        }


        public static IEnumerable<IEndPointConfiguration> ReadEndPointsFromDisk()
        {
            // Please see ConnectionstringUtility class to add your login credentials for MySQL

            List<IEndPointConfiguration> endPoints = EndPointUtilities.LoadEndPoints(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json")).ToList();
            IEndPointConfiguration backOffice = endPoints.FirstOrDefault(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL);
            IEndPointConfiguration frontOffice = endPoints.FirstOrDefault(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL);

            if (backOffice != null)
                backOffice.ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);

            if (frontOffice != null)
                frontOffice.ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);

            return endPoints;
        }
    }
}
