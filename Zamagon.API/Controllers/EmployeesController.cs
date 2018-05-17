using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.BackOffice;
using Zamagon.Model;

namespace Zamagon.API.Controllers
{
    [Produces("application/json")]
    [Route("api/BackOffice/Employees")]
    public class EmployeesController : Controller
    {
        private IAdaptiveClient<IEmployeesService> serviceClient;

        public EmployeesController(IAdaptiveClient<IEmployeesService> serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        [HttpGet]
        public async Task<List<Employee>> GetOrders()
        {
            return await serviceClient.CallAsync(async x => await x.GetEmployees());
        }
    }
}