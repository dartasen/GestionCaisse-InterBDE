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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    /// Interaction logic for SynthesisUserControl.xaml
    /// </summary>
    public partial class SynthesisUserControl : UserControl
    {
        public SynthesisUserControl()
        {
            InitializeComponent();
            DataContext = new SynthesisViewModel();
        }
    }
}