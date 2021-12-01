namespace Zamagon.Services.BackOffice.WebAPI;

public class TimeCardsService : BaseHTTPService, ITimeCardsService
{
    public TimeCardsService(Func<IEndPointConfiguration> endPointFactory) : base(endPointFactory)
    {

    }

    public virtual async Task<List<TimeCard>> GetTimeCards() => await httpClient.GetFromJsonAsync<List<TimeCard>>("timecards");
}
