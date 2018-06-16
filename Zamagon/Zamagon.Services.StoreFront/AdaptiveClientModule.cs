using System;
using System.Collections.Generic;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;
using Zamagon.Domain.StoreFront;

namespace Zamagon.Services.StoreFront
{
    public class AdaptiveClientModule : IAdaptiveClientModule
    {
        public void Register(RegistrationHelper registrationHelper)
        {
            registrationHelper

            // --- StoreFront Services ---

            // MSSQL
            .RegisterService<StoreFront.MSSQL.OrdersService, IOrdersService>(EndPointType.DBMS, API_Name.StoreFront, DataBaseProviderName.MSSQL)
            .RegisterService<StoreFront.MSSQL.ProductsService, IProductsService>(EndPointType.DBMS, API_Name.StoreFront, DataBaseProviderName.MSSQL)
            
            // MySQL
            .RegisterService<StoreFront.MySQL.OrdersService, IOrdersService>(EndPointType.DBMS, API_Name.StoreFront, DataBaseProviderName.MySQL)
            .RegisterService<StoreFront.MySQL.ProductsService, IProductsService>(EndPointType.DBMS, API_Name.StoreFront, DataBaseProviderName.MySQL)

            // WebAPI
            .RegisterService<StoreFront.WebAPI.OrdersService, IOrdersService>(EndPointType.HTTP, API_Name.StoreFront, DataBaseProviderName.WebAPI)
            .RegisterService<StoreFront.WebAPI.ProductsService, IProductsService>(EndPointType.HTTP, API_Name.StoreFront, DataBaseProviderName.WebAPI)

            // DbContexts
            .RegisterDbContext<Database.Db>(API_Name.StoreFront)

            // Migration Contexts
            .RegisterMigrationContext<Database.Db_MSSQL>(API_Name.StoreFront, DataBaseProviderName.MSSQL)
            .RegisterMigrationContext<Database.Db_MySQL>(API_Name.StoreFront, DataBaseProviderName.MySQL)

            // Database Initializers
            .RegisterDatabaseInitializer<SFDatabaseInitializer>(API_Name.StoreFront, DataBaseProviderName.MSSQL)
            .RegisterDatabaseInitializer<SFDatabaseInitializer>(API_Name.StoreFront, DataBaseProviderName.MySQL) 

            // Service Manifests
            .RegisterServiceManifest<SFServiceManifest, ISFServiceManifest>(EndPointType.DBMS, API_Name.StoreFront, DataBaseProviderName.MSSQL)
            .RegisterServiceManifest<SFServiceManifest, ISFServiceManifest>(EndPointType.DBMS, API_Name.StoreFront, DataBaseProviderName.MySQL)
            .RegisterServiceManifest<SFServiceManifest, ISFServiceManifest>(EndPointType.HTTP, API_Name.StoreFront, DataBaseProviderName.WebAPI);
        }
    }
}
