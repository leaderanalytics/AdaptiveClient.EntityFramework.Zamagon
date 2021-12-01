namespace Zamagon.Services.BackOffice.MySQL;

public class TimeCardsService : MSSQL.TimeCardsService
{
    public TimeCardsService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
    {

    }
}
