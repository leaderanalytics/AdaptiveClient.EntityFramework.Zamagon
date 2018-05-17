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
    public class ProductsService : BaseService, IProductsService
    {
        public ProductsService(Db db, ISFServiceManifest serviceManifest) : base(db, serviceManifest)
        {

        }

        public virtual async Task<List<Product>> GetProducts()
        {
            return await db.Products.ToListAsync();
        }
    }
}
