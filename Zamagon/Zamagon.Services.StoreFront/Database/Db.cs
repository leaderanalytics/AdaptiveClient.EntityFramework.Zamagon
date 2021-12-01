namespace Zamagon.Services.StoreFront.Database;

public class Db : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    public Db(Func<IDbContextOptions> dbContextOptionsFactory) : base(dbContextOptionsFactory().Options)
    {
        // this constructor is required.  dbContextOptionsFactory is registered by AdaptiveClient.EntityFramework
    }

    public Db(DbContextOptions options) : base(options)
    {
        // this constructor is required.
    }
}
