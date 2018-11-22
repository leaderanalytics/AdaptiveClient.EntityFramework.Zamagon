using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.StoreFront;
using Zamagon.Model;

namespace Zamagon.API.Controllers
{
    [Produces("application/json")]
    public class OrdersController : Controller
    {
        private IAdaptiveClient<IOrdersService> serviceClient;

        public OrdersController(IAdaptiveClient<IOrdersService> serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        [HttpGet]
        [Route("api/StoreFront/Orders")]
        public async Task<List<Order>> GetOrders()
        {
            return await serviceClient.CallAsync(async x => await x.GetOrders());
        }


        [HttpGet]
        [Route("api/StoreFront")]
        public async Task Get()
        {

        }
    }
}