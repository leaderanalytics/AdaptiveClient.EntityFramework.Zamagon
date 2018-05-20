using System;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.Utilities;
using Zamagon.Domain;

namespace Zamagon.Services.Common
{
    public class AdaptiveClientModule : IAdaptiveClientModule
    {
        public void Register(RegistrationHelper registrationHelper)
        {
            registrationHelper


            // EndPoint Validator
            .RegisterEndPointValidator<MSSQL_EndPointValidator>(EndPointType.DBMS, DataBaseProviderName.MSSQL)
            .RegisterEndPointValidator<MySQL_EndPointValidator>(EndPointType.DBMS, DataBaseProviderName.MySQL)
            .RegisterEndPointValidator<Http_EndPointValidator>(EndPointType.HTTP, DataBaseProviderName.WebAPI)


            // DbContextOptions
            .RegisterDbContextOptions<DbContextOptions_MSSQL>(DataBaseProviderName.MSSQL)
            .RegisterDbContextOptions<DbContextOptions_MySQL>(DataBaseProviderName.MySQL);
        }
    }
}
