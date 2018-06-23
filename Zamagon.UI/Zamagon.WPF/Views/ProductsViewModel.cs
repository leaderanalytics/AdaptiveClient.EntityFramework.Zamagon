using System;
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
    public class ProductsViewModel : BaseViewModel<Product>
    {
        public ProductsViewModel(IAdaptiveClient<ISFServiceManifest> storeFrontClient, IAdaptiveClient<IBOServiceManifest> backOfficeClient) :base(storeFrontClient, backOfficeClient)
        {
            Banner = "Products";
            EndPoints = new ObservableCollection<IEndPointConfiguration>(LoadEndPoints(API_Name.StoreFront));
        }

        public override async Task GetData(object arg)
        {
            await base.GetData(API_Name.StoreFront);
            StoreFrontServiceClient = Container.Resolve<IAdaptiveClient<ISFServiceManifest>>();
            List<Product> products = await StoreFrontServiceClient.TryAsync(async x => await x.ProductsService.GetProducts());
            products.ForEach(x => Entities.Add(x));
            LogMessages.Add($"{products.Count} rows retrieved from {StoreFrontServiceClient.CurrentEndPoint.Name}.");
        }
    }
}
