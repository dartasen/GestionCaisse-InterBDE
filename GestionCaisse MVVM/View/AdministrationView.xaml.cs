using System;
using System.Windows;
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
