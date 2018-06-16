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
using LeaderAnalytics.AdaptiveClient;

namespace Zamagon.WPF
{
    /// <summary>
    /// Interaction logic for EndPointListBox.xaml
    /// </summary>
    public partial class EndPointListBox : UserControl
    {

        public IEnumerable<IEndPointConfiguration> EndPoints
        {
            get { return (IEnumerable<IEndPointConfiguration>)GetValue(EndPointsProperty); }
            set { SetValue(EndPointsProperty, value); }
        }

        public static readonly DependencyProperty EndPointsProperty =
            DependencyProperty.Register("EndPoints", typeof(IEnumerable<IEndPointConfiguration>), typeof(EndPointListBox), new PropertyMetadata(null));


        public string SelectedEndPointName
        {
            get { return (string)GetValue(SelectedEndPointNameProperty); }
            set { SetValue(SelectedEndPointNameProperty, value); }
        }

        public static readonly DependencyProperty SelectedEndPointNameProperty =
            DependencyProperty.Register("SelectedEndPointName", typeof(string), typeof(EndPointListBox), new FrameworkPropertyMetadata(null,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private bool isProcessing;

        public EndPointListBox()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (isProcessing)
                return;     // prevent recursive calls when we changed IsActive in this handler

            isProcessing = true;
            CheckBox checkBox = (CheckBox)sender;

            if (!checkBox.IsChecked.HasValue || !checkBox.IsChecked.Value)
                checkBox.IsChecked = true;
            else
            {

                SelectedEndPointName = checkBox.Tag.ToString();

                foreach (IEndPointConfiguration ep in EndPoints.Where(x => x.Name != SelectedEndPointName))
                    ep.IsActive = false;
            }
            isProcessing = false;
        }
       
    }
}
