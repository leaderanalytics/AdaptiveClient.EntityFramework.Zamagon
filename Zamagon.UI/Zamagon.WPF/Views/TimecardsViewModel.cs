namespace Zamagon.WPF.Views;

public class TimecardsViewModel : BaseViewModel<TimeCard, IBOServiceManifest>
{
    public TimecardsViewModel(IAdaptiveClient<IBOServiceManifest> serviceClient) : base(API_Name.BackOffice)
    {
        Banner = "TimeCards";
    }

    protected override async Task<List<TimeCard>> FetchData() => await ServiceClient.TryAsync(x => x.TimeCardsService.GetTimeCards());
}
