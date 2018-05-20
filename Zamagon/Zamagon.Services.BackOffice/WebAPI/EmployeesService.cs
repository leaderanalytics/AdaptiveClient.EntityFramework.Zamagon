using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.BackOffice;
using Zamagon.Model;
using Zamagon.Services.BackOffice.Database;
using Zamagon.Services.Common;

namespace Zamagon.Services.BackOffice.WebAPI
{
    public class EmployeesService : BaseHTTPService, IEmployeesService
    {
        public EmployeesService(Func<IEndPointConfiguration> endPointFactory) : base(endPointFactory)
        {

        }

        public virtual async Task<List<Employee>> GetEmployees()
        {
            string json = await httpClient.GetStringAsync("employees");
            return JsonConvert.DeserializeObject<List<Employee>>(json);
        }
    }
}
