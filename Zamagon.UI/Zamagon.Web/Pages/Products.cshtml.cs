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
    public class ProductsModel : BasePageModel<Product, ISFServiceManifest>
    {
        public ProductsModel(IAdaptiveClient<ISFServiceManifest> serviceClient) : base(serviceClient, API_Name.StoreFront) { }

        protected override async Task GetData() => Data = await ServiceClient.CallAsync(async x => await x.ProductsService.GetProducts(), CurrentEndPoint.Name);
    }
}