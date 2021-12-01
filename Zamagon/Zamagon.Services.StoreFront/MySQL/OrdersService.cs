namespace Zamagon.Services.StoreFront.MySQL;

public class OrdersService : MSSQL.OrdersService
{
    public OrdersService(Db db, ISFServiceManifest serviceManifest) : base(db, serviceManifest)
    {

    }

    // Derive from MSSQL.OrdersService because much of the code for MSSQL is the
    // same for MySQL.

    // MySQL specific code here...

    public override async Task<List<Order>> GetOrders() => await db.Orders.ToListAsync();
}
