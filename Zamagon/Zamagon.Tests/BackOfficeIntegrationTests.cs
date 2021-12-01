namespace Zamagon.Tests;

[TestFixture("MSSQL")]
[TestFixture("MySQL")]
[TestFixture("WebAPI")]
class BackOfficeIntegrationTests : BaseTest
{
    public BackOfficeIntegrationTests(string databaseProviderName) : base(databaseProviderName)
    {

    }

    [SetUp]
    public async Task Setup()
    {
        await CreateTestArtifacts();
    }

    [Test]
    public async Task Employee_count_equals_two()
    {
        IEndPointConfiguration ep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.BackOffice);
        await DropAndRecreateDatabase(ep);
        List<Employee> employees = await BOServiceClient.CallAsync(async x => await x.EmployeesService.GetEmployees(), ep.Name);
        Assert.AreEqual(2, employees.Count);
    }

    [Test]
    public async Task TimeCard_count_equals_four()
    {
        IEndPointConfiguration ep = EndPoints.First(x => x.ProviderName == CurrentDatabaseProviderName && x.API_Name == API_Name.BackOffice);
        await DropAndRecreateDatabase(ep);
        List<TimeCard> timeCards = await BOServiceClient.CallAsync(async x => await x.TimeCardsService.GetTimeCards(), ep.Name);
        Assert.AreEqual(4, timeCards.Count);
    }
}
