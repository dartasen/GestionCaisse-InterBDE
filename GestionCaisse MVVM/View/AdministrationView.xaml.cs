﻿using System;
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
using System.Windows.Shapes;
using GestionCaisse_MVVM.Model.Services;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    /// Interaction logic for AdministrationView.xaml
    /// </summary>
    public partial class AdministrationView : Window
    {
        public AdministrationView()
        {
            InitializeComponent();

            var vm = new AdministrationViewModel();
            DataContext = vm;
        }

        private void AdministrationView_OnClosed(object sender, EventArgs e)
        {
            LoginService.Instance.IsTimerActive = true;
        }
    }
}
