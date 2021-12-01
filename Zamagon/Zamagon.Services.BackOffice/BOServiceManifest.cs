namespace Zamagon.Services.BackOffice;

public class BOServiceManifest : ServiceManifestFactory, IBOServiceManifest
{
    public IEmployeesService EmployeesService { get => Create<IEmployeesService>(); }
    public ITimeCardsService TimeCardsService { get => Create<ITimeCardsService>(); }
}
