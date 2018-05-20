using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.StoreFront;
using Zamagon.Model;
using Zamagon.Services.StoreFront.Database;
using Zamagon.Services.Common;

namespace Zamagon.Services.StoreFront.WebAPI
{
    public class ProductsService : BaseHTTPService, IProductsService
    {
        public ProductsService(Func<IEndPointConfiguration> endPointFactory) : base(endPointFactory)
        {
        }

        public virtual async Task<List<Product>> GetProducts()
        {
            string json = await httpClient.GetStringAsync("products");
            return JsonConvert.DeserializeObject<List<Product>>(json);
        }
    }
}
