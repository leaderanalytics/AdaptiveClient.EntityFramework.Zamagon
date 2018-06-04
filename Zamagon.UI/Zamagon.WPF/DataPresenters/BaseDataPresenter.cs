using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.BackOffice;
using Zamagon.Domain.StoreFront;

namespace Zamagon.WPF.DataPresenters
{
    public abstract class BaseDataPresenter : UserControl, INotifyPropertyChanged
    {
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(BaseDataPresenter), new PropertyMetadata(false, IsSelectedChanged));


        public IEnumerable<IEndPointConfiguration> EndPoints
        {
            get { return (IEnumerable<IEndPointConfiguration>)GetValue(EndPointsProperty); }
            set { SetValue(EndPointsProperty, value); }
        }

        public static readonly DependencyProperty EndPointsProperty =
            DependencyProperty.Register("EndPoints", typeof(IEnumerable<IEndPointConfiguration>), typeof(BaseDataPresenter), new PropertyMetadata(null));


        public static void IsSelectedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            BaseDataPresenter control = sender as BaseDataPresenter;

            if (control.IsSelected)
                control.CreateUI();
        }

        public IAdaptiveClient<IBOServiceManifest> BackOfficeServiceClient { get; set; }
        public IAdaptiveClient<ISFServiceManifest> StoreFrontServiceClient { get; set; }

        public virtual void CreateUI()
        {
            Autofac.IContainer container = App.CreateContainer(EndPoints);

            using (var scope = container.BeginLifetimeScope())
            {
                
            }
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
