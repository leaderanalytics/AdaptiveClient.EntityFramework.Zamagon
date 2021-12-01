namespace Zamagon.WPF;

public class MainWindowViewModel : PropertyChangedBase
{
    private int _SelectedTabIndex;
    public int SelectedTabIndex
    {
        get => _SelectedTabIndex;
        set
        {
            if (_SelectedTabIndex != value)
            {
                _SelectedTabIndex = value;
                RaisePropertyChanged();
            }
        }
    }


    public Task Initalization;

    public MainWindowViewModel()
    {
        Initalization = Initalize();
    }

    public async Task Initalize()
    {

    }
}
