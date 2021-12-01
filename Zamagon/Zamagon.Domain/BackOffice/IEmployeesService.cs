namespace Zamagon.Domain.BackOffice;

public interface IEmployeesService : IDisposable
{
    Task<List<Employee>> GetEmployees();
}
