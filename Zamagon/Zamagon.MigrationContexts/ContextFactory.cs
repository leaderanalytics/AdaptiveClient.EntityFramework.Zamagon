using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using Zamagon.Domain;
using Zamagon.Services.BackOffice.Database;
using Zamagon.Services.StoreFront.Database;

namespace Zamagon.MigrationContexts
{
    public class BackOffice_MSSQLContextFactory : IDesignTimeDbContextFactory<Zamagon.Services.BackOffice.Database.Db_MSSQL>
    {
        public Zamagon.Services.BackOffice.Database.Db_MSSQL CreateDbContext(string[] args)
        {
            string connectionString = ConnectionstringUtility.GetConnectionString("bin\\debug\\netcoreapp2.0\\EndPoints.json", API_Name.BackOffice, DataBaseProviderName.MSSQL);
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
            string connectionString = ConnectionstringUtility.BuildConnectionString(ConnectionstringUtility.GetConnectionString("bin\\debug\\netcoreapp2.0\\EndPoints.json", API_Name.BackOffice, DataBaseProviderName.MySQL));
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseMySql(connectionString);
            Zamagon.Services.BackOffice.Database.Db_MySQL db = new Zamagon.Services.BackOffice.Database.Db_MySQL(dbOptions.Options);
            return db;
        }
    }

    public class StoreFront_MSSQLContextFactory : IDesignTimeDbContextFactory<Zamagon.Services.StoreFront.Database.Db_MSSQL>
    {
        public Zamagon.Services.StoreFront.Database.Db_MSSQL CreateDbContext(string[] args)
        {
            string connectionString = ConnectionstringUtility.GetConnectionString("bin\\debug\\netcoreapp2.0\\EndPoints.json", API_Name.StoreFront, DataBaseProviderName.MSSQL);
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
            string connectionString = ConnectionstringUtility.BuildConnectionString(ConnectionstringUtility.GetConnectionString("bin\\debug\\netcoreapp2.0\\EndPoints.json", API_Name.StoreFront, DataBaseProviderName.MySQL));
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder();
            dbOptions.UseMySql(connectionString);
            Zamagon.Services.StoreFront.Database.Db_MySQL db = new Zamagon.Services.StoreFront.Database.Db_MySQL(dbOptions.Options);
            return db;
        }
    }
}
