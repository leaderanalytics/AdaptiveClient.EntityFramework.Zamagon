namespace Zamagon.Services.BackOffice.Database;

public class Db : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<TimeCard> TimeCards { get; set; }

    public Db(Func<IDbContextOptions> dbContextOptionsFactory) : base(dbContextOptionsFactory().Options)
    {

    }

    public Db(DbContextOptions options) : base(options)
    {
    }
}
