using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.BackOffice;
using Zamagon.Model;

namespace Zamagon.API.Controllers
{
    [Produces("application/json")]
    public class TimeCardsController : ControllerBase
    {
        private IAdaptiveClient<ITimeCardsService> serviceClient;

        public TimeCardsController(IAdaptiveClient<ITimeCardsService> serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        [HttpGet]
        [Route("api/BackOffice/TimeCards")]
        public async Task<List<TimeCard>> GetTimeCards()
        {
            return await serviceClient.CallAsync(x => x.GetTimeCards());
        }
    }
}