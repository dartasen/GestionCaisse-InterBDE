using System.Windows;
using GestionCaisse_MVVM.Model.Services;
using GestionCaisse_MVVM.ViewModel;
using MahApps.Metro.Controls;

namespace GestionCaisse_MVVM.View
{
    /// <summary>
    /// Interaction logic for RollingBackView.xaml
    /// </summary>
    public partial class RollingBackView : MetroWindow
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
                if ((ComboBox.Items[i] as User).Nom.Equals(user.Nom))
                {
                    ComboBox.SelectedIndex = i;
                }
            }
        }
    }

}
