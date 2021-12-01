namespace Zamagon.Services.StoreFront.MSSQL;

public class OrdersService : BaseService, IOrdersService
{
    public OrdersService(Db db, ISFServiceManifest serviceManifest) : base(db, serviceManifest)
    {

    }

    // MSSQL specific code here...
    public virtual async Task<List<Order>> GetOrders() => await db.Orders.ToListAsync();

}
