namespace Zamagon.Domain.BackOffice;

public interface ITimeCardsService : IDisposable
{
    Task<List<TimeCard>> GetTimeCards();
}
