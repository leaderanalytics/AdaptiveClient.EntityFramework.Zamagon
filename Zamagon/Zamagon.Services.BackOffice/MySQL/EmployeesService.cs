namespace Zamagon.Services.BackOffice.MySQL;

public class EmployeesService : MSSQL.EmployeesService
{
    public EmployeesService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
    {

    }
}
