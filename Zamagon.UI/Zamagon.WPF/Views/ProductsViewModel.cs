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
    public class ProductsViewModel : BaseViewModel<Product, ISFServiceManifest>
    {
        public ProductsViewModel(IAdaptiveClient<ISFServiceManifest> serviceClient) :base(API_Name.StoreFront)
        {
            Banner = "Products";
        }

        protected override async Task<List<Product>> FetchData() => await ServiceClient.TryAsync(x => x.ProductsService.GetProducts());
    }
}
