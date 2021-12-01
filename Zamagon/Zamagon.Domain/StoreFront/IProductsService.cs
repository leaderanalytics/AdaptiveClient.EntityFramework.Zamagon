namespace Zamagon.Domain.StoreFront;

public interface IProductsService : IDisposable
{
    Task<List<Product>> GetProducts();
}
