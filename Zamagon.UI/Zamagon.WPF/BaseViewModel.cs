using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autofac;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.StoreFront;
using Zamagon.Domain.BackOffice;


namespace Zamagon.WPF
{
    public abstract class BaseViewModel<T> : INotifyPropertyChanged where T:class
    {
        private ObservableCollection<T> _Entities;
        public ObservableCollection<T> Entities
        {
            get => _Entities;
            set
            {
                if (_Entities != value)
                {
                    _Entities = value;
                    RaisePropertyChanged();
                }
            }
        }

        private T _CurrentEntity;
        public T CurrentEntity
        {
            get => _CurrentEntity;
            set
            {
                if (_CurrentEntity != value)
                {
                    _CurrentEntity = value;
                    RaisePropertyChanged();
                }
            }
        }

        


        protected Autofac.IContainer Container;
        protected IAdaptiveClient<ISFServiceManifest>  StoreFrontServiceClient { get; private set; }
        protected IAdaptiveClient<IBOServiceManifest> BackOfficeServiceClient { get; private set; }

        public BaseViewModel(IAdaptiveClient<ISFServiceManifest> storeFrontClient, IAdaptiveClient<IBOServiceManifest> backOfficeClient)
        {
            StoreFrontServiceClient = storeFrontClient;
            BackOfficeServiceClient = backOfficeClient;
            Entities = new ObservableCollection<T>();
        }

        protected void CreateContainer(IEnumerable<IEndPointConfiguration> endPoints)
        {
            Container = App.CreateContainer(endPoints);
            BackOfficeServiceClient = Container.Resolve<IAdaptiveClient<IBOServiceManifest>>();
            StoreFrontServiceClient = Container.Resolve<IAdaptiveClient<ISFServiceManifest>>();
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
