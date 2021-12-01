namespace Zamagon.Domain.StoreFront;

public interface IOrdersService : IDisposable
{
    Task<List<Order>> GetOrders();
}
