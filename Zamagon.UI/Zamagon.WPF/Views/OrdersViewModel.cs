using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.BackOffice;
using Zamagon.Domain.StoreFront;
using Zamagon.Model;

namespace Zamagon.WPF.Views
{
    public class OrdersViewModel : BaseViewModel<Order>
    {
        public OrdersViewModel(IAdaptiveClient<ISFServiceManifest> storeFrontClient, IAdaptiveClient<IBOServiceManifest> backOfficeClient) :base(storeFrontClient, backOfficeClient)
        {

        }

        public async Task CreateUI()
        {
            List<Order> orders = await StoreFrontServiceClient.TryAsync(async x => await x.OrdersService.GetOrders());
            Entities.Clear();
            orders.ForEach(x => Entities.Add(x));
        }
    }
}
