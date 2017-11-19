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
using GestionCaisse_MVVM.ViewModel.AdministrationFeatures;

namespace GestionCaisse_MVVM.View.AdministrationFeatures
{
    /// <summary>
    /// Interaction logic for ClientManagementUserControl.xaml
    /// </summary>
    public partial class ClientManagementUserControl : UserControl
    {
        public ClientManagementUserControl()
        {
            InitializeComponent();

            DataContext = new ClientManagementViewModel();
        }
    }
}
