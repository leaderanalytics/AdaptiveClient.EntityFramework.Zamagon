namespace Zamagon.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public Task Initalization { get; set; }

    public MainWindow(MainWindowViewModel vm)
    {
        InitializeComponent();
        Initalization = Initalize(vm);
    }

    private async Task Initalize(MainWindowViewModel vm)
    {
        await vm.Initalization;
        DataContext = vm;
    }
}
