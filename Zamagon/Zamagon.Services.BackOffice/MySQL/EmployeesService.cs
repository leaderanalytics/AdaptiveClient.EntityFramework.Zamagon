using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.BackOffice;
using Zamagon.Model;
using Zamagon.Services.BackOffice.Database;

namespace Zamagon.Services.BackOffice.MySQL
{
    public class EmployeesService : MSSQL.EmployeesService
    {
        public EmployeesService(Db db, IBOServiceManifest serviceManifest) : base(db, serviceManifest)
        {

        }
    }
}
