using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;
using Zamagon.Services.StoreFront.Database;
using Zamagon.Model;

namespace Zamagon.Services.StoreFront
{
    public class SFDatabaseInitializer : IDatabaseInitializer
    {
        private Db db;


        public SFDatabaseInitializer(Db db)
        {
            this.db = db;
        }

        public async Task Seed(string migrationName)
        {
            Product phone = new Product { Description = "Phone", Price = 700 };
            Product cup = new Product { Description = "Coffee cup", Price = 15 };

            Order o1 = new Order { ClientID = 1, OrderDate = DateTime.Now.AddDays(-3), Product = phone };
            Order o2 = new Order { ClientID = 2, OrderDate = DateTime.Now.AddDays(-4), Product = cup };

            db.Entry(phone).State = EntityState.Added;
            db.Entry(cup).State = EntityState.Added;
            db.Entry(o1).State = EntityState.Added;
            db.Entry(o2).State = EntityState.Added;

            await db.SaveChangesAsync();
        }
    }
}
