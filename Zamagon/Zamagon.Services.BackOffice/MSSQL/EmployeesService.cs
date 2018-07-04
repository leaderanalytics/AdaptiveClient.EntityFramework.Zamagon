using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.BackOffice;
using Zamagon.Model;
using Zamagon.Services.BackOffice.Database;

namespace Zamagon.Services.BackOffice.MSSQL
{
    public class EmployeesService : BaseService, IEmployeesService
    {
        public EmployeesService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
        {

        }

        public virtual async Task<List<Employee>> GetEmployees()
        {
            // We can access any service that is defined on the Manifest here also:
            List<TimeCard> dummyList = await ServiceManifest.TimeCardsService.GetTimeCards();


            return await db.Employees.ToListAsync();
        }
    }
}
