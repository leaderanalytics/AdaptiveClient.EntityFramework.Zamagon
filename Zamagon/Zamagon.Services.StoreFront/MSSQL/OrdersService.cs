using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Zamagon.Domain.StoreFront;
using Zamagon.Model;
using Zamagon.Services.StoreFront.Database;

namespace Zamagon.Services.StoreFront.MSSQL
{
    public class OrdersService : BaseService, IOrdersService
    {
        public OrdersService(Db db, ISFServiceManifest serviceManifest) : base(db, serviceManifest)
        {

        }

        public virtual async Task<List<Order>> GetOrders()
        {
            return await db.Orders.ToListAsync();
        }
    }
}
