using System;
using System.Collections.Generic;
using System.Text;
using Zamagon.Domain.StoreFront;
using LeaderAnalytics.AdaptiveClient.Utilities;

namespace Zamagon.Services.StoreFront
{
    public class SFServiceManifest : ServiceManifestFactory, ISFServiceManifest
    {
        public IOrdersService OrdersService { get => Create<IOrdersService>(); }
        public IProductsService ProductsService { get => Create<IProductsService>(); }
    }
}
