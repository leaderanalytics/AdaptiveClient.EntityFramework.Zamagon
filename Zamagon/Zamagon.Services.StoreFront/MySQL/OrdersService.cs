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
    }
}
