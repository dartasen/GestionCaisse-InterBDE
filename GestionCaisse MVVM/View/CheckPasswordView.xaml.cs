using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.ViewModel;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    /// Interaction logic for CheckPasswordView.xaml
    /// </summary>
    public partial class CheckPasswordView : Window
    {
        public CheckPasswordView(string windowToOpenAction)
        {
            InitializeComponent();

            var vm = new CheckPasswordViewModel(windowToOpenAction)
            {
                Close = () => Close(),
                Hide = () => Hide()
            };

            DataContext = vm;

            PasswordBox.Focus();
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                ((CheckPasswordViewModel)DataContext).CheckUserPassword.Execute(null);

            if (e.Key.Equals(Key.Escape))
                Close();
        }
    }
}
