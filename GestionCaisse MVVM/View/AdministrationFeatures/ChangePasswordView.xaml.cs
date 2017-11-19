using System.Windows;
using GestionCaisse_MVVM.ViewModel.AdministrationFeatures;

namespace GestionCaisse_MVVM.View.AdministrationFeatures
{
    /// <summary>
    /// Interaction logic for ChangePasswordView.xaml
    /// </summary>
    public partial class ChangePasswordView : Window
    {
        public ChangePasswordView(User user)
        {
            InitializeComponent();

            DataContext = new ChangePasswordViewModel(user)
            {
                Close = () => Close()
            };

            PasswordTextBox.Focus();
        }
    }
}
