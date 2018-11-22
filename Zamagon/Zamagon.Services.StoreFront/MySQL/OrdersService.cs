using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.StoreFront;
using Zamagon.Model;
using Zamagon.Services.StoreFront.Database;

namespace Zamagon.Services.StoreFront.MySQL
{
    public class OrdersService : MSSQL.OrdersService
    {
        public OrdersService(Db db, ISFServiceManifest serviceManifest) : base(db, serviceManifest)
        {

        }

        public override async Task<List<Order>> GetOrders()
        {
            // Derive from MSSQL.OrdersService because much of the code for MSSQL is the
            // same for MySQL.

            // MySQL specific code here...
            return await db.Orders.ToListAsync();
        }

    }
}
