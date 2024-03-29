﻿namespace Zamagon.Tests;

[TestFixture("MSSQL")]
[TestFixture("MySQL")]
[TestFixture("WebAPI")]
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
        await DropAndRecreateDatabase(ep);
        List<Order> orders = await SFServiceClient.CallAsync(async x => await x.OrdersService.GetOrders(), ep.Name);
        Assert.AreEqual(2, orders.Count);
    }

    [Test]
    public async Task Product_count_equals_two()
    {
        IEndPointConfiguration ep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.StoreFront);
        await DropAndRecreateDatabase(ep);
        List<Product> products = await SFServiceClient.CallAsync(async x => await x.ProductsService.GetProducts(), ep.Name);
        Assert.AreEqual(2, products.Count);
    }
}
