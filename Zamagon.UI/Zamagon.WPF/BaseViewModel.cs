namespace Zamagon.WPF;

public abstract class BaseViewModel<TModel, TManifest> : PropertyChangedBase where TManifest : class where TModel : class
{
    private ObservableCollection<TModel> _Entities;
    public ObservableCollection<TModel> Entities
    {
        get => _Entities;
        set
        {
            if (_Entities != value)
            {
                _Entities = value;
                RaisePropertyChanged();
            }
        }
    }

    private TModel _CurrentEntity;
    public TModel CurrentEntity
    {
        get => _CurrentEntity;
        set
        {
            if (_CurrentEntity != value)
            {
                _CurrentEntity = value;
                RaisePropertyChanged();
            }
        }
    }

    private ObservableCollection<IEndPointConfiguration> _EndPoints;
    public ObservableCollection<IEndPointConfiguration> EndPoints
    {
        get => _EndPoints;
        set
        {
            if (_EndPoints != value)
            {
                _EndPoints = value;
                RaisePropertyChanged();
            }
        }
    }

    private string _SelectedEndPointName;
    public string SelectedEndPointName
    {
        get => _SelectedEndPointName;
        set
        {
            if (_SelectedEndPointName != value)
            {
                _SelectedEndPointName = value;
                RaisePropertyChanged();
            }
        }
    }

    private ObservableCollection<string> _LogMessages;
    public ObservableCollection<string> LogMessages
    {
        get => _LogMessages;
        set
        {
            if (_LogMessages != value)
            {
                _LogMessages = value;
                RaisePropertyChanged();
            }
        }
    }

    private string _Banner;
    public string Banner
    {
        get => _Banner;
        set
        {
            if (_Banner != value)
            {
                _Banner = value;
                RaisePropertyChanged();
            }
        }
    }

    public ICommand GetDataCommand { get; set; }
    protected IAdaptiveClient<TManifest> ServiceClient { get; set; }
    protected readonly string APIName;

    public BaseViewModel(string apiName)
    {
        // In most applications IAdaptiveClient<TManifest>> will be injected in the constructor.
        APIName = apiName;
        Entities = new ObservableCollection<TModel>();
        EndPoints = new ObservableCollection<IEndPointConfiguration>(LoadEndPoints());
        LogMessages = new ObservableCollection<string>();
        GetDataCommand = new CommandHandler(GetData, x => true);
    }

    public async Task GetData(object arg)
    {
        Entities.Clear();
        LogMessages.Clear();
        // In this demo we create the container numerous times - this is not standard behavior for most apps and is not required to use AdaptiveClient.
        Autofac.IContainer container = App.CreateContainer(EndPoints, APIName, x => LogMessages.Add(x));
        ServiceClient = container.Resolve<IAdaptiveClient<TManifest>>();
        Stopwatch sw = Stopwatch.StartNew();
        List<TModel> data = await FetchData();
        sw.Stop();
        data.ForEach(x => Entities.Add(x));
        LogMessages.Add($"{data.Count} rows retrieved from {ServiceClient.CurrentEndPoint.Name}.");
        LogMessages.Add($"Data acquisition time was {sw.ElapsedMilliseconds} milliseconds.");
    }

    protected IEnumerable<IEndPointConfiguration> LoadEndPoints()
    {
        IEnumerable<IEndPointConfiguration> endPoints = App.ReadEndPointsFromDisk().Where(x => x.API_Name == APIName).Select(x => new PresentationEndPoint(x)).ToList();

        foreach (IEndPointConfiguration ep in endPoints.Skip(1))
            ep.IsActive = false;

        return endPoints;
    }

    protected abstract Task<List<TModel>> FetchData();
}
