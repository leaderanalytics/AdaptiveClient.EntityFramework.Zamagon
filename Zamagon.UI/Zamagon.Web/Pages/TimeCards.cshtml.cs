using System;
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
    public class TimeCardsModel : BasePageModel
    {
        public List<TimeCard> TimeCards { get; set; }
        private IAdaptiveClient<IBOServiceManifest> serviceClient;

        public TimeCardsModel(IAdaptiveClient<IBOServiceManifest> serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        public override async Task OnGetAsync()
        {
            await base.OnGetAsync();
            await GetTimeCards();
        }

        public override async Task OnPostAsync()
        {
            await base.OnPostAsync();
            await GetTimeCards();
        }

        private async Task GetTimeCards()
        {
            CurrentEndPoint = GetEndPoints().FirstOrDefault(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataSource);

            if (CurrentEndPoint != null)
                TimeCards = await serviceClient.CallAsync(async x => await x.TimeCardsService.GetTimeCards(), CurrentEndPoint.Name);
        }
    }
}