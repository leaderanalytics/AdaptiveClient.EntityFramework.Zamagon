namespace Zamagon.Services.StoreFront.WebAPI;

public class ProductsService : BaseHTTPService, IProductsService
{
    public ProductsService(Func<IEndPointConfiguration> endPointFactory) : base(endPointFactory)
    {
    }

    public virtual async Task<List<Product>> GetProducts() => await httpClient.GetFromJsonAsync<List<Product>>("products");
}
