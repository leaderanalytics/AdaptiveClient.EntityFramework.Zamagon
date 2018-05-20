using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;
using Zamagon.Domain.StoreFront;
using Zamagon.Model;

namespace Zamagon.Web.Pages
{
    public class ProductsModel : BasePageModel
    {
        public List<Product> Products { get; set; }
        private IAdaptiveClient<ISFServiceManifest> serviceClient;

        public ProductsModel(IAdaptiveClient<ISFServiceManifest> serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        public override async Task OnGetAsync()
        {
            await base.OnGetAsync();
            await GetProducts();
        }

        public override async Task OnPostAsync()
        {
            await base.OnPostAsync();
            await GetProducts();
        }

        private async Task GetProducts()
        {
            CurrentEndPoint = GetEndPoints().FirstOrDefault(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataSource);

            if (CurrentEndPoint != null)
                Products = await serviceClient.CallAsync(async x => await x.ProductsService.GetProducts(), CurrentEndPoint.Name);
        }
    }
}