namespace Zamagon.Web.Pages;

public abstract class BasePageModel<TModel, TManifest> : PageModel where TManifest : class
{
    protected IAdaptiveClient<TManifest> ServiceClient;
    protected readonly string APIName;


    protected string DataSource
    {
        get => HttpContext.Session.GetString("dataSource");

        set
        {
            HttpContext.Session.SetString("dataSource", value);
            SetCurrentEndPoint();
        }
    }

    public List<TModel> Data { get; set; }

    //public TModel CurrentItem { get; set; } // Not used in this demo but is common

    public IEndPointConfiguration CurrentEndPoint { get; set; }

    public BasePageModel(IAdaptiveClient<TManifest> serviceClient, string apiName)
    {
        ServiceClient = serviceClient;
        APIName = apiName;
    }

    private void SetCurrentEndPoint() => CurrentEndPoint = GetEndPoints().FirstOrDefault(x => x.API_Name == APIName && x.ProviderName == DataSource);

    protected abstract Task GetData();

    public virtual async Task OnGetAsync()
    {
        if (DataSource == null)
            DataSource = DataBaseProviderName.MSSQL;

        if (CurrentEndPoint == null)
            SetCurrentEndPoint();

        await GetData();
    }

    public virtual async Task OnPostAsync()
    {
        DataSource = Request.Form["dataSources"];
        await GetData();
    }

    protected IEnumerable<IEndPointConfiguration> GetEndPoints()
    {
        IEnumerable<IEndPointConfiguration> endPoints = HttpContext.Session.GetObject<IEnumerable<EndPointConfiguration>>("endPoints");

        if (endPoints == null)
        {
            endPoints = AutofacModule.ReadEndPointsFromDisk();
            HttpContext.Session.SetObject("endPoints", endPoints);
        }
        return endPoints;
    }
}
