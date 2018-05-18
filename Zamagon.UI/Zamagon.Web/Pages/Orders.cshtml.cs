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
    public class OrdersModel : BasePageModel
    {
        public List<Order> Orders { get; set; }
        private IAdaptiveClient<ISFServiceManifest> serviceClient;

        public OrdersModel(IAdaptiveClient<ISFServiceManifest> serviceClient)
        {
            this.serviceClient = serviceClient;
        }


        public override async Task OnGetAsync()
        {
            await base.OnGetAsync();
            IEndPointConfiguration ep = GetEndPoints().FirstOrDefault(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataSource);
            string epName = ep?.Name ?? null;
            Orders = await serviceClient.CallAsync(async x => await x.OrdersService.GetOrders(), epName);
        }


        public override async Task OnPostAsync()
        {
            await base.OnPostAsync();
        }
    }
}