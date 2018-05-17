using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Zamagon.Model;

namespace Zamagon.Domain.StoreFront
{
    public interface IOrdersService
    {
        Task<List<Order>> GetOrders();
    }
}
