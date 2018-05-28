using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.StoreFront;
using Zamagon.Domain.BackOffice;


namespace Zamagon.WPF
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public IAdaptiveClient<ISFServiceManifest>  StoreFrontClient { get; private set; }
        public IAdaptiveClient<IBOServiceManifest> BackOfficeClient { get; private set; }

        public BaseViewModel(IAdaptiveClient<ISFServiceManifest> storeFrontClient, IAdaptiveClient<IBOServiceManifest> backOfficeClient)
        {
            StoreFrontClient = storeFrontClient;
            BackOfficeClient = backOfficeClient;
        }


        #region ProperyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
