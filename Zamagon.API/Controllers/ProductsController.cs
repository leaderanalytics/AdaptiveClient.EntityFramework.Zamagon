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
    [Route("api/StoreFront/Products")]
    public class ProductsController : Controller
    {
        private IAdaptiveClient<IProductsService> serviceClient;

        public ProductsController(IAdaptiveClient<IProductsService> serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        [HttpGet]
        public async Task<List<Product>> GetProducts()
        {
            return await serviceClient.CallAsync(x => x.GetProducts());
        }
    }
}