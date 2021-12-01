namespace Zamagon.Services.BackOffice.MSSQL;

public class TimeCardsService : BaseService, ITimeCardsService
{
    public TimeCardsService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
    {

    }

    public virtual async Task<List<TimeCard>> GetTimeCards() => await db.TimeCards.ToListAsync();
}
