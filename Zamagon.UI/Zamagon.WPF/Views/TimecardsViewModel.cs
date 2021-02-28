using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;
using Zamagon.Domain.BackOffice;
using Zamagon.Domain.StoreFront;
using Zamagon.Model;

namespace Zamagon.WPF.Views
{
    public class TimecardsViewModel : BaseViewModel<TimeCard, IBOServiceManifest>
    {
        public TimecardsViewModel(IAdaptiveClient<IBOServiceManifest> serviceClient) :base(API_Name.BackOffice)
        {
            Banner = "TimeCards";
        }

        protected override async Task<List<TimeCard>> FetchData() => await ServiceClient.TryAsync(x => x.TimeCardsService.GetTimeCards());
    }
}
