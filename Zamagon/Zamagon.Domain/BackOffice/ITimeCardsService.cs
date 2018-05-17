using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Zamagon.Model;

namespace Zamagon.Domain.BackOffice
{
    public interface ITimeCardsService
    {
        Task<List<TimeCard>> GetTimeCards();
    }
}
