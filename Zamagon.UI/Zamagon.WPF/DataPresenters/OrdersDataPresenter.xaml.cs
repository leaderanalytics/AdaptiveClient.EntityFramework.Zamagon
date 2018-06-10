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
using Zamagon.Model;

namespace Zamagon.WPF.DataPresenters
{
    /// <summary>
    /// Interaction logic for OrdersDataPresenter.xaml
    /// </summary>
    public partial class OrdersDataPresenter : BaseDataPresenter
    {
        private ObservableCollection<Order> _Orders;
        public ObservableCollection<Order> Orders
        {
            get => _Orders;
            set
            {
                if (_Orders != value)
                {
                    _Orders = value;
                    RaisePropertyChanged();
                }
            }
        }


        public OrdersDataPresenter()
        {
            InitializeComponent();
        }

        public override void CreateUI()
        {
            base.CreateUI();
            List<Order> orders = Task.Run(async () => await StoreFrontServiceClient.TryAsync(async x => await x.OrdersService.GetOrders())).Result;
            Orders = new ObservableCollection<Order>(orders);
        }
    }
}
