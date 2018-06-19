using System;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Autofac;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;
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

        private ObservableCollection<IEndPointConfiguration> _EndPoints;
        public ObservableCollection<IEndPointConfiguration> EndPoints
        {
            get => _EndPoints;
            set
            {
                if (_EndPoints != value)
                {
                    _EndPoints = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _SelectedEndPointName;
        public string SelectedEndPointName
        {
            get => _SelectedEndPointName;
            set
            {
                if (_SelectedEndPointName != value)
                {
                    _SelectedEndPointName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<string> _LogMessages;
        public ObservableCollection<string> LogMessages
        {
            get => _LogMessages;
            set
            {
                if (_LogMessages != value)
                {
                    _LogMessages = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand GetDataCommand { get; set; }
        protected Autofac.IContainer Container;
        protected IAdaptiveClient<ISFServiceManifest>  StoreFrontServiceClient { get;  set; }
        protected IAdaptiveClient<IBOServiceManifest> BackOfficeServiceClient { get; set; }

        public BaseViewModel(IAdaptiveClient<ISFServiceManifest> storeFrontClient, IAdaptiveClient<IBOServiceManifest> backOfficeClient)
        {
            StoreFrontServiceClient = storeFrontClient;
            BackOfficeServiceClient = backOfficeClient;
            Entities = new ObservableCollection<T>();
            EndPoints = new ObservableCollection<IEndPointConfiguration>();
            LogMessages = new ObservableCollection<string>();
            GetDataCommand = new CommandHandler(GetData, x => true);
        }

        protected void CreateContainer(IEnumerable<IEndPointConfiguration> endPoints, string apiName)
        {
            Container = App.CreateContainer(endPoints, apiName, x => LogMessages.Add(x));
        }

        public virtual async Task GetData(object arg)
        {
            Entities.Clear();
            LogMessages.Clear();
            CreateContainer(EndPoints, arg.ToString());
        }

        protected IEnumerable<IEndPointConfiguration> LoadEndPoints(string apiName)
        {
          IEnumerable<IEndPointConfiguration> endPoints = App.ReadEndPointsFromDisk().Where(x => x.API_Name == apiName).Select(x => new PresentationEndPoint(x)).ToList();

            foreach (IEndPointConfiguration ep in endPoints.Skip(1))
                ep.IsActive = false;

            return endPoints;
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
