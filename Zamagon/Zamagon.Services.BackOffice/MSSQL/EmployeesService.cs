namespace Zamagon.Services.BackOffice.MSSQL;

public class EmployeesService : BaseService, IEmployeesService
{
    public EmployeesService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
    {
        // We can access any service that is defined on the Manifest from any other service:
        //List<TimeCard> dummyList = await ServiceManifest.TimeCardsService.GetTimeCards();
    }

    public virtual async Task<List<Employee>> GetEmployees() => await db.Employees.ToListAsync();
}
