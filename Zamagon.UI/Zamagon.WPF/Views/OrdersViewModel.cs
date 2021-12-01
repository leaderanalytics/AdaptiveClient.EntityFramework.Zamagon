namespace Zamagon.WPF.Views;

public class OrdersViewModel : BaseViewModel<Order, ISFServiceManifest>
{
    public OrdersViewModel(IAdaptiveClient<ISFServiceManifest> serviceClient) : base(API_Name.StoreFront)
    {
        Banner = "Orders";
    }

    protected override async Task<List<Order>> FetchData() => await ServiceClient.TryAsync(x => x.OrdersService.GetOrders());
}
