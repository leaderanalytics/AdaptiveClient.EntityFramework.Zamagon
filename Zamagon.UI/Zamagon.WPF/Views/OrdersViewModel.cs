using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;
using Zamagon.Domain.BackOffice;
using Zamagon.Domain.StoreFront;
using Zamagon.Model;

namespace Zamagon.WPF.Views
{
    public class OrdersViewModel : BaseViewModel<Order>
    {
        public OrdersViewModel(IAdaptiveClient<ISFServiceManifest> storeFrontClient, IAdaptiveClient<IBOServiceManifest> backOfficeClient) :base(storeFrontClient, backOfficeClient)
        {
            Banner = "Orders";
            EndPoints = new ObservableCollection<IEndPointConfiguration>(LoadEndPoints(API_Name.StoreFront));
        }

        public override async Task GetData(object arg)
        {
            await base.GetData(API_Name.StoreFront);
            Stopwatch sw = Stopwatch.StartNew();
            StoreFrontServiceClient = Container.Resolve<IAdaptiveClient<ISFServiceManifest>>();
            List<Order> orders = await StoreFrontServiceClient.TryAsync(x => x.OrdersService.GetOrders());
            sw.Stop();
            orders.ForEach(x => Entities.Add(x));
            LogMessages.Add($"{orders.Count} rows retrieved from {StoreFrontServiceClient.CurrentEndPoint.Name}.");
            LogMessages.Add($"Data acquistion time was {sw.ElapsedMilliseconds} miliseconds.");
        }
    }
}
