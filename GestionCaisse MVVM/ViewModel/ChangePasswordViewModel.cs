using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class ChangePasswordViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private User user;

        public ChangePasswordViewModel(User user)
        {
            this.user = user;

            DialogService dialogService = new DialogService();

            CloseWindow = new RelayCommand(() => Close(), o => true);

            ChangePassword = new RelayCommand(() =>
            {
                if (string.IsNullOrEmpty(Password))
                {
                    dialogService.ShowInformationWindow("Le mot de passe ne peut pas être nul !", "Changement impossible ! !",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    UserService.ChangeUserPassword(user, Password);
                }
                catch
                {
                    return;
                }

                dialogService.ShowInformationWindow("Mot de passe changé !", "Modifications enregistrées",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);

                Close();
            }, o => true);
        }

        #region Properties
        public string Username => user.Name;

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands
        public ICommand CloseWindow { get; }
        public ICommand ChangePassword { get; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
