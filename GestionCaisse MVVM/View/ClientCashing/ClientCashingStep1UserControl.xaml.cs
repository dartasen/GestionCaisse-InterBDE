using System;
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

            TextBox.Focus();
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                ((ClientCashingStep1ViewModel)DataContext).CheckPasskey.Execute(null);
            }
        }
    }
}
