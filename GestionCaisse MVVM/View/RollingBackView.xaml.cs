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
    /// Interaction logic for RollingBackView.xaml
    /// </summary>
    public partial class RollingBackView : Window
    {
        public RollingBackView()
        {
            InitializeComponent();

            var vm = new RollingBackViewModel
            {
                Close = () => Close(),
                Hide = () => Hide()
            };

            DataContext = vm;
        }

        private void RollingBackView_OnLoaded(object sender, RoutedEventArgs e)
        {
            /* 
             * PS : Visual workaround
             * You cannot set quickly the CurrentUser with a object of a foreign
             * collection from the ViewModel. The match doesn't work.
             * Instead, you need to make it manually here to get the visual OK with
             * the value in your VM when the dialog is fully loaded.
             */
            
            User user = LoginService.Instance.GetLoginContext().User;

            for (int i = 0; i < ComboBox.Items.Count; i++)
            {
                if (((UserService.UserQueryResult)ComboBox.Items[i]).Name == user.Name)
                    ComboBox.SelectedIndex = i;
            }
        }
    }

}
