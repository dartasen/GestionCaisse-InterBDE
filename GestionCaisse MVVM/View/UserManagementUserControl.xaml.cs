using System.Windows.Controls;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
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
