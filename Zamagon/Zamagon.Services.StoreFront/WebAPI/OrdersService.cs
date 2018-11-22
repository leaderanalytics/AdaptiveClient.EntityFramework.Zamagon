using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.StoreFront;
using Zamagon.Model;
using Zamagon.Services.StoreFront.Database;
using Zamagon.Services.Common;

namespace Zamagon.Services.StoreFront.WebAPI
{
    public class OrdersService : BaseHTTPService , IOrdersService
    {
        public OrdersService(Func<IEndPointConfiguration> endPointFactory) : base(endPointFactory)
        {

        }

        public virtual async Task<List<Order>> GetOrders()
        {
            string json = await httpClient.GetStringAsync("orders");
            return JsonConvert.DeserializeObject<List<Order>>(json);
        }
    }
}
