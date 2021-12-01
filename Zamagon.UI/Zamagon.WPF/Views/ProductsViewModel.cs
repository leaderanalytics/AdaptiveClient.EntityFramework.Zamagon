namespace Zamagon.WPF.Views;

public class ProductsViewModel : BaseViewModel<Product, ISFServiceManifest>
{
    public ProductsViewModel(IAdaptiveClient<ISFServiceManifest> serviceClient) : base(API_Name.StoreFront)
    {
        Banner = "Products";
    }

    protected override async Task<List<Product>> FetchData() => await ServiceClient.TryAsync(x => x.ProductsService.GetProducts());
}
