namespace Zamagon.Services.StoreFront;

public class SFServiceManifest : ServiceManifestFactory, ISFServiceManifest
{
    public IOrdersService OrdersService { get => Create<IOrdersService>(); }
    public IProductsService ProductsService { get => Create<IProductsService>(); }
}
