﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;
using Zamagon.Domain.BackOffice;
using Zamagon.Model;

namespace Zamagon.Web.Pages
{
    public class TimeCardsModel : BasePageModel<TimeCard, IBOServiceManifest>
    {
        public TimeCardsModel(IAdaptiveClient<IBOServiceManifest> serviceClient) : base(serviceClient, API_Name.BackOffice) { }
        
        protected override async Task GetData() => Data = await ServiceClient.CallAsync(async x => await x.TimeCardsService.GetTimeCards(), CurrentEndPoint.Name);
    }
}