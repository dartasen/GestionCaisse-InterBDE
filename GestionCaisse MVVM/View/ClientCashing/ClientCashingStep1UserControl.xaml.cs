using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GestionCaisse_MVVM.ViewModel.ClientCashing;

namespace GestionCaisse_MVVM.View.ClientCashing
{
    /// <summary>
    /// Interaction logic for ClientCashingStep1UserControl.xaml
    /// </summary>
    public partial class ClientCashingStep1UserControl : UserControl
    {
        public ClientCashingStep1UserControl(Action<int> checkPasskey)
        {
            InitializeComponent();

            DataContext = new ClientCashingStep1ViewModel(checkPasskey);
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                (DataContext as ClientCashingStep1ViewModel).CheckPasskey.Execute(null);
            }
        }

        private void ClientCashingStep1UserControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox.Focus();
            TextBox.Text = string.Empty;
        }
    }
}
