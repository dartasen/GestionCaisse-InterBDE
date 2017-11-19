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
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
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
