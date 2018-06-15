﻿using System;
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
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Model;

namespace Zamagon.WPF.Views
{
    public partial class HomeView : BaseView
    {
        public HomeView()
        {
            InitializeComponent();
        }

        public override void IsSelected_Changed(IEnumerable<IEndPointConfiguration> endPoints)
        {
            base.IsSelected_Changed(endPoints);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string name = ((CheckBox)sender).Tag.ToString();
        }
    }
}
