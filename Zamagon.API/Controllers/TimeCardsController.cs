﻿using System;
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
    [Route("api/BackOffice/TimeCards")]
    public class TimeCardsController : Controller
    {
        private IAdaptiveClient<ITimeCardsService> serviceClient;

        public TimeCardsController(IAdaptiveClient<ITimeCardsService> serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        [HttpGet]
        public async Task<List<TimeCard>> GetTimeCards()
        {
            return await serviceClient.CallAsync(async x => await x.GetTimeCards());
        }
    }
}