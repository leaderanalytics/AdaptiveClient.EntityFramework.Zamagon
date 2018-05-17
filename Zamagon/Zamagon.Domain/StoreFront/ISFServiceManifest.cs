using System;
using System.Collections.Generic;
using System.Text;

namespace Zamagon.Domain.StoreFront
{
    public interface ISFServiceManifest : IDisposable
    {
        IOrdersService OrdersService { get; }
        IProductsService ProductsService { get; }
    }
}
