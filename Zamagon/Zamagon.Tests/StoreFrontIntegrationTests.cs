using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LeaderAnalytics.AdaptiveClient;
using NUnit.Framework;
using Zamagon.Model;
using Zamagon.Domain;

namespace Zamagon.Tests
{
    [TestFixture("MSSQL")]
    [TestFixture("MySQL")]
    public class StoreFrontIntegratonTests : BaseTest
    {
        public StoreFrontIntegratonTests(string databaseProviderName) : base(databaseProviderName)
        {
        }

        [SetUp]
        public async Task Setup()
        {
            await CreateTestArtifacts();
        }

        [Test]
        public async Task Order_count_equals_two()
        {
            IEndPointConfiguration ep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.StoreFront);
            await DropAndRecreate(ep);
            List<Order> orders = await SFServiceClient.CallAsync(async x => await x.OrdersService.GetOrders());
            Assert.AreEqual(2, orders.Count);
        }

        [Test]
        public async Task Product_count_equals_two()
        {
            IEndPointConfiguration ep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.StoreFront);
            await DropAndRecreate(ep);
            List<Product> products = await SFServiceClient.CallAsync(async x => await x.ProductsService.GetProducts());
            Assert.AreEqual(2, products.Count);
        }
    }
}
