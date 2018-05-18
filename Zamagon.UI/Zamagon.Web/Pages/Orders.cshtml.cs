using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LeaderAnalytics.AdaptiveClient;
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
            Orders = await serviceClient.CallAsync(async x => await x.OrdersService.GetOrders());
        }


        public override async Task OnPostAsync()
        {
            await base.OnPostAsync();
        }
    }
}