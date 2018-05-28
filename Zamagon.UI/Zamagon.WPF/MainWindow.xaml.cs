using System;
using System.Collections.Generic;
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

namespace Zamagon.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Task Initalization { get; set; }

        public MainWindow(MainWindowViewModel vm)
        {
            InitializeComponent();
            Initalization = Initalize(vm);
        }

        private async Task Initalize(MainWindowViewModel vm)
        {
            await vm.Initalization;
            DataContext = vm;
        }
    }
}
