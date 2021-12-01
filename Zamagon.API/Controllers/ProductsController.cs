namespace Zamagon.API.Controllers;

[Produces("application/json")]
[Route("api/StoreFront/Products")]
public class ProductsController : ControllerBase
{
    private IAdaptiveClient<IProductsService> serviceClient;

    public ProductsController(IAdaptiveClient<IProductsService> serviceClient)
    {
        this.serviceClient = serviceClient;
    }

    [HttpGet]
    public async Task<List<Product>> GetProducts()
    {
        return await serviceClient.CallAsync(x => x.GetProducts());
    }
}
