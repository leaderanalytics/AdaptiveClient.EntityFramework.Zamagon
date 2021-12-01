namespace Zamagon.API.Controllers;

[Produces("application/json")]
public class EmployeesController : ControllerBase
{
    private IAdaptiveClient<IEmployeesService> serviceClient;

    public EmployeesController(IAdaptiveClient<IEmployeesService> serviceClient)
    {
        this.serviceClient = serviceClient;
    }

    [HttpGet]
    [Route("api/BackOffice/Employees")]
    public async Task<List<Employee>> GetEmployees()
    {
        return await serviceClient.CallAsync(x => x.GetEmployees());
    }


    [HttpGet]
    [Route("api/BackOffice")]
    public async Task Get()
    {

    }
}
