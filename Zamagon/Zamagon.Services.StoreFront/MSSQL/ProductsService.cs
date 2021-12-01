namespace Zamagon.Services.StoreFront.MSSQL;

public class ProductsService : BaseService, IProductsService
{
    public ProductsService(Db db, ISFServiceManifest serviceManifest) : base(db, serviceManifest)
    {

    }

    public virtual async Task<List<Product>> GetProducts() => await db.Products.ToListAsync();
}
