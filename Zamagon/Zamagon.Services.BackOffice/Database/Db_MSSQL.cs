namespace Zamagon.Services.BackOffice.Database;

public class Db_MSSQL : Db, IMigrationContext
{
    public Db_MSSQL(DbContextOptions options) : base(options)
    {

    }
}
