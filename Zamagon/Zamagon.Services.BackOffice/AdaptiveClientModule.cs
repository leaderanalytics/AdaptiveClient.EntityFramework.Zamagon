﻿namespace Zamagon.Services.BackOffice;

public class AdaptiveClientModule : IAdaptiveClientModule
{
    public void Register(RegistrationHelper registrationHelper)
    {
        // --- BackOffice Services ---


        registrationHelper

        // MSSQL
        .RegisterService<MSSQL.EmployeesService, IEmployeesService>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MSSQL)
        .RegisterService<MSSQL.TimeCardsService, ITimeCardsService>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MSSQL)

        // MySQL
        .RegisterService<BackOffice.MySQL.EmployeesService, IEmployeesService>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MySQL)
        .RegisterService<BackOffice.MySQL.TimeCardsService, ITimeCardsService>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MySQL)

        // WebAPI
        .RegisterService<BackOffice.WebAPI.EmployeesService, IEmployeesService>(EndPointType.HTTP, API_Name.BackOffice, DataBaseProviderName.WebAPI)
        .RegisterService<BackOffice.WebAPI.TimeCardsService, ITimeCardsService>(EndPointType.HTTP, API_Name.BackOffice, DataBaseProviderName.WebAPI)

        // DbContexts
        .RegisterDbContext<Database.Db>(API_Name.BackOffice)

        // Migration Contexts
        .RegisterMigrationContext<Database.Db_MSSQL>(API_Name.BackOffice, DataBaseProviderName.MSSQL)
        .RegisterMigrationContext<Database.Db_MySQL>(API_Name.BackOffice, DataBaseProviderName.MySQL)

        // Database Initializers
        .RegisterDatabaseInitializer<BODatabaseInitializer>(API_Name.BackOffice, DataBaseProviderName.MSSQL)
        .RegisterDatabaseInitializer<BODatabaseInitializer>(API_Name.BackOffice, DataBaseProviderName.MySQL) // register same class for both providers.  If we had stored procs, etc. that were different we could just create a new class.

        // Service Manifests
        .RegisterServiceManifest<BOServiceManifest, IBOServiceManifest>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MSSQL)
        .RegisterServiceManifest<BOServiceManifest, IBOServiceManifest>(EndPointType.DBMS, API_Name.BackOffice, DataBaseProviderName.MySQL)
        .RegisterServiceManifest<BOServiceManifest, IBOServiceManifest>(EndPointType.HTTP, API_Name.BackOffice, DataBaseProviderName.WebAPI);
    }
}
