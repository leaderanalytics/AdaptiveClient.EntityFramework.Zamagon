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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Zamagon.WPF
{
    public class TabHeader : Control, INotifyPropertyChanged
    {
        public string IconName
        {
            get { return (string)GetValue(IconNameProperty); }
            set { SetValue(IconNameProperty, value); }
        }

        public static readonly DependencyProperty IconNameProperty =
            DependencyProperty.Register("IconName", typeof(string), typeof(TabHeader), new PropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TabHeader), new PropertyMetadata(null));


        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(TabHeader), new PropertyMetadata(false, IsHighlightedCallback));



        public bool IsHighlighted
        {
            get { return (bool)GetValue(IsHighlightedProperty); }
            set { SetValue(IsHighlightedProperty, value); }
        }

        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(TabHeader), new FrameworkPropertyMetadata(false, IsHighlightedCallback, CoerceHighlightedCallback ));


        static TabHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabHeader), new FrameworkPropertyMetadata(typeof(TabHeader)));
        }

        public TabHeader()
        {
            this.MouseEnter += TabHeader_MouseEnter;
            this.MouseLeave += TabHeader_MouseLeave;
            
        }

        private void TabHeader_MouseLeave(object sender, MouseEventArgs e)
        {
            IsHighlighted = IsMouseOver && ! IsSelected;
        }

        private void TabHeader_MouseEnter(object sender, MouseEventArgs e)
        {
            IsHighlighted = IsMouseOver && !IsSelected;
        }

        static void IsHighlightedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TabHeader tabHeader = sender as TabHeader;
            tabHeader.IsHighlighted = (tabHeader.IsMouseOver && !tabHeader.IsSelected);
            tabHeader.RaisePropertyChanged("IsHighlighted");
            tabHeader.RaisePropertyChanged("IsSelected");

        }

        static object CoerceHighlightedCallback(DependencyObject sender, object val)
        {
            TabHeader tabHeader = sender as TabHeader;
            return (tabHeader.IsMouseOver && !tabHeader.IsSelected);
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
