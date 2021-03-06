﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;
using Zamagon.Domain.StoreFront;
using Zamagon.Domain.BackOffice;


namespace Zamagon.WPF
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private int _SelectedTabIndex;
        public int SelectedTabIndex
        {
            get => _SelectedTabIndex;
            set
            {
                if (_SelectedTabIndex != value)
                {
                    _SelectedTabIndex = value;
                    RaisePropertyChanged();
                }
            }
        }
        

        public Task Initalization;

        public MainWindowViewModel()
        {
            Initalization = Initalize();
        }

        public async Task Initalize()
        {
        
        }
    }
}
