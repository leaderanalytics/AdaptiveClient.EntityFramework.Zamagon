using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Zamagon.Domain.BackOffice;
using Zamagon.Model;
using Zamagon.Services.BackOffice.Database;

namespace Zamagon.Services.BackOffice.MSSQL
{
    public class TimeCardsService : BaseService, ITimeCardsService
    {
        public TimeCardsService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
        {

        }

        public virtual async Task<List<TimeCard>> GetTimeCards()
        {
            return await db.TimeCards.ToListAsync();
        }
    }
}
