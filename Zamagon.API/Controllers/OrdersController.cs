namespace Zamagon.API.Controllers;

[Produces("application/json")]
public class OrdersController : ControllerBase
{
    private IAdaptiveClient<IOrdersService> serviceClient;

    public OrdersController(IAdaptiveClient<IOrdersService> serviceClient)
    {
        this.serviceClient = serviceClient;
    }

    [HttpGet]
    [Route("api/StoreFront/Orders")]
    public async Task<List<Order>> GetOrders()
    {
        return await serviceClient.CallAsync(x => x.GetOrders());
    }


    [HttpGet]
    [Route("api/StoreFront")]
    public async Task Get()
    {

    }
}
