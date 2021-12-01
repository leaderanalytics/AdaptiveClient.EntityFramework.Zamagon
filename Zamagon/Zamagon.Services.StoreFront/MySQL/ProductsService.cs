namespace Zamagon.Services.StoreFront.MySQL;

public class ProductsService : MSSQL.ProductsService
{
    public ProductsService(Db db, ISFServiceManifest serviceManifest) : base(db, serviceManifest)
    {

    }
}
