namespace Zamagon.Services.StoreFront.WebAPI;

public class OrdersService : BaseHTTPService, IOrdersService
{
    public OrdersService(Func<IEndPointConfiguration> endPointFactory) : base(endPointFactory)
    {

    }

    public virtual async Task<List<Order>> GetOrders() => await httpClient.GetFromJsonAsync<List<Order>>("orders");
}
