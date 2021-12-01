using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Zamagon.Domain;
using System.Reflection;
using System.IO;

namespace Zamagon.MigrationContexts;

public class Constants
{
    public static readonly string EndPointsFile;
    static Constants() => EndPointsFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json");
}

public class BackOffice_MSSQLContextFactory : IDesignTimeDbContextFactory<Zamagon.Services.BackOffice.Database.Db_MSSQL>
{
    public Zamagon.Services.BackOffice.Database.Db_MSSQL CreateDbContext(string[] args)
    {
        string connectionString = ConnectionstringUtility.GetConnectionString(Constants.EndPointsFile, API_Name.BackOffice, DataBaseProviderName.MSSQL);
        DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
        dbOptions.UseSqlServer(connectionString);
        Zamagon.Services.BackOffice.Database.Db_MSSQL db = new Zamagon.Services.BackOffice.Database.Db_MSSQL(dbOptions.Options);
        return db;
    }
}

public class BackOffice_MySQLContextFactory : IDesignTimeDbContextFactory<Zamagon.Services.BackOffice.Database.Db_MySQL>
{
    public Zamagon.Services.BackOffice.Database.Db_MySQL CreateDbContext(string[] args)
    {
        string connectionString = ConnectionstringUtility.BuildConnectionString(ConnectionstringUtility.GetConnectionString(Constants.EndPointsFile, API_Name.BackOffice, DataBaseProviderName.MySQL));
        DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
        dbOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        Zamagon.Services.BackOffice.Database.Db_MySQL db = new Zamagon.Services.BackOffice.Database.Db_MySQL(dbOptions.Options);
        return db;
    }
}

public class StoreFront_MSSQLContextFactory : IDesignTimeDbContextFactory<Zamagon.Services.StoreFront.Database.Db_MSSQL>
{
    public Zamagon.Services.StoreFront.Database.Db_MSSQL CreateDbContext(string[] args)
    {
        string connectionString = ConnectionstringUtility.GetConnectionString(Constants.EndPointsFile, API_Name.StoreFront, DataBaseProviderName.MSSQL);
        DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
        dbOptions.UseSqlServer(connectionString);
        Zamagon.Services.StoreFront.Database.Db_MSSQL db = new Zamagon.Services.StoreFront.Database.Db_MSSQL(dbOptions.Options);
        return db;
    }
}

public class StoreFront_MySQLContextFactory : IDesignTimeDbContextFactory<Zamagon.Services.StoreFront.Database.Db_MySQL>
{
    public Zamagon.Services.StoreFront.Database.Db_MySQL CreateDbContext(string[] args)
    {
        string connectionString = ConnectionstringUtility.BuildConnectionString(ConnectionstringUtility.GetConnectionString(Constants.EndPointsFile, API_Name.StoreFront, DataBaseProviderName.MySQL));
        DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
        dbOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        Zamagon.Services.StoreFront.Database.Db_MySQL db = new Zamagon.Services.StoreFront.Database.Db_MySQL(dbOptions.Options);
        return db;
    }
}
