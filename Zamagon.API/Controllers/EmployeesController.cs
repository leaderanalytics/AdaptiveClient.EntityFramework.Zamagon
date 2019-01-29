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
    public class EmployeesController : Controller
    {
        private IAdaptiveClient<IEmployeesService> serviceClient;

        public EmployeesController(IAdaptiveClient<IEmployeesService> serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        [HttpGet]
        [Route("api/BackOffice/Employees")]
        public async Task<List<Employee>> GetEmployees()
        {
            return await serviceClient.CallAsync(x => x.GetEmployees());
        }


        [HttpGet]
        [Route("api/BackOffice")]
        public async Task Get()
        {

        }
    }
}