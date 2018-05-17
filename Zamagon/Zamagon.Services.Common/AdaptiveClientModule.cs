using System;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;

namespace Zamagon.Services.Common
{
    public class AdaptiveClientModule : IAdaptiveClientModule
    {
        public void Register(RegistrationHelper registrationHelper)
        {
            registrationHelper


            // EndPoint Validator
            .RegisterEndPointValidator<InProcessEndPointValidator>(EndPointType.DBMS, DataBaseProviderName.MSSQL)
            .RegisterEndPointValidator<InProcessEndPointValidator>(EndPointType.DBMS, DataBaseProviderName.MySQL)


            // DbContextOptions
            .RegisterDbContextOptions<DbContextOptions_MSSQL>(DataBaseProviderName.MSSQL)
            .RegisterDbContextOptions<DbContextOptions_MySQL>(DataBaseProviderName.MySQL);
        }
    }
}
