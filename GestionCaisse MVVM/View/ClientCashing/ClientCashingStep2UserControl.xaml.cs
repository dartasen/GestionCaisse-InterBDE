using System;
using System.Windows.Controls;
using GestionCaisse_MVVM.ViewModel.ClientCashing;

namespace GestionCaisse_MVVM.View.ClientCashing
{
    /// <summary>
    /// Interaction logic for ClientCashingStep2UserControl.xaml
    /// </summary>
    public partial class ClientCashingStep2UserControl : UserControl
    {
        public ClientCashingStep2UserControl(Client client, Action goBackward, Action closeWindow)
        {
            InitializeComponent();

            DataContext = new ClientCashingStep2ViewModel(client, goBackward, closeWindow);
        }
    }
}
