using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using Zamagon.Model;

namespace Zamagon.WPF.Views
{
    public partial class OrdersView : BaseView
    {
        public OrdersView()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).Container.Resolve<OrdersViewModel>();
        }


        public override void CreateUI()
        {
            base.CreateUI();
            Dispatcher.InvokeAsync(() => ((OrdersViewModel)DataContext).CreateUI()).Wait();
        }
    }
}
