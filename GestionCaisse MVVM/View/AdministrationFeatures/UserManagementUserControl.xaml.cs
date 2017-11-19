using System.Windows.Controls;
using GestionCaisse_MVVM.ViewModel.AdministrationFeatures;

namespace GestionCaisse_MVVM.View.AdministrationFeatures
{
    /// <summary>
    /// Interaction logic for UserManagementUserControl.xaml
    /// </summary>
    public partial class UserManagementUserControl : UserControl
    {
        public UserManagementUserControl()
        {
            InitializeComponent();

            DataContext = new UserManagementViewModel();
        }
    }
}
