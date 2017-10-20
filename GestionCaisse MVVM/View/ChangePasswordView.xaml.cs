using System.Windows;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
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
