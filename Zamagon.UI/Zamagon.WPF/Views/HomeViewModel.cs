﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.BackOffice;
using Zamagon.Domain.StoreFront;
using Zamagon.Model;

namespace Zamagon.WPF.Views
{
    public class HomeViewModel : BaseViewModel<Order>
    {
        public HomeViewModel(IAdaptiveClient<ISFServiceManifest> storeFrontClient, IAdaptiveClient<IBOServiceManifest> backOfficeClient) :base(storeFrontClient, backOfficeClient)
        {
        }

        public void HandleEndPointActivation(string name)
        {

        }
    }
}
