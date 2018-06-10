using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Autofac;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain.BackOffice;
using Zamagon.Domain.StoreFront;

namespace Zamagon.WPF.Views
{
    public abstract class BaseView : UserControl
    {
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(BaseView), new PropertyMetadata(false, IsSelectedChanged));


        public IEnumerable<IEndPointConfiguration> EndPoints
        {
            get { return (IEnumerable<IEndPointConfiguration>)GetValue(EndPointsProperty); }
            set { SetValue(EndPointsProperty, value); }
        }

        public static readonly DependencyProperty EndPointsProperty =
            DependencyProperty.Register("EndPoints", typeof(IEnumerable<IEndPointConfiguration>), typeof(BaseView), new PropertyMetadata(null));


        public static void IsSelectedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            BaseView control = sender as BaseView;

            if (control.IsSelected)
                control.CreateUI();
        }

        public virtual void CreateUI()
        {
        
        }
    }
}
