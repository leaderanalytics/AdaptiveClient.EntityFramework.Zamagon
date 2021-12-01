namespace Zamagon.Services.BackOffice.WebAPI;

public class EmployeesService : BaseHTTPService, IEmployeesService
{
    public EmployeesService(Func<IEndPointConfiguration> endPointFactory) : base(endPointFactory)
    {

    }

    public virtual async Task<List<Employee>> GetEmployees() => await httpClient.GetFromJsonAsync<List<Employee>>("employees");
}
