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
using System.Windows.Shapes;
using GestionCaisse_MVVM.Model.Services;
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
