namespace Zamagon.Services.StoreFront.Database;

public class Db_MySQL : Db, IMigrationContext
{
    public Db_MySQL(DbContextOptions options) : base(options)
    {

    }
}
