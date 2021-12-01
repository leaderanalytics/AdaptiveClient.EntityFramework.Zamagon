namespace Zamagon.WPF;

public class PresentationEndPoint : INotifyPropertyChanged, IEndPointConfiguration
{
    public string Name { get; set; }
    public string API_Name { get; set; }
    public int Preference { get; set; }
    public string EndPointType { get; set; }
    public string ConnectionString { get; set; }
    public string ProviderName { get; set; }
    public Dictionary<string, string> Parameters { get; set; }

    private bool _IsActive;
    public bool IsActive
    {
        get => _IsActive;
        set
        {
            if (_IsActive != value)
            {
                _IsActive = value;
                RaisePropertyChanged();
            }
        }
    }


    public PresentationEndPoint() { }

    public PresentationEndPoint(IEndPointConfiguration ep)
    {
        if (ep == null)
            return;


        Name = ep.Name;
        API_Name = ep.API_Name;
        Preference = ep.Preference;
        EndPointType = ep.EndPointType;
        ConnectionString = ep.ConnectionString;
        ProviderName = ep.ProviderName;
        Parameters = ep.Parameters;
        IsActive = ep.IsActive;
    }

    #region ProperyChanged Implementation
    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
