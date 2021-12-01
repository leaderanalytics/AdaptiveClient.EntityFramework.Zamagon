namespace Zamagon.WPF.Views;

public class EmployeesViewModel : BaseViewModel<Employee, IBOServiceManifest>
{
    public EmployeesViewModel(IAdaptiveClient<IBOServiceManifest> serviceClient) : base(API_Name.BackOffice)
    {
        Banner = "Employees";
    }

    protected override async Task<List<Employee>> FetchData() => await ServiceClient.TryAsync(x => x.EmployeesService.GetEmployees());
}
