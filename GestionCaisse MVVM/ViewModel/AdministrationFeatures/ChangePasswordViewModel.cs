using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel.AdministrationFeatures
{
    public class ChangePasswordViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly User user;

        public ChangePasswordViewModel(User user)
        {
            this.user = user;

            DialogService dialogService = new DialogService();

            CloseWindow = new RelayCommand(() => Close(), o => true);

            ChangePassword = new RelayCommand(() =>
            {
                if (string.IsNullOrEmpty(Password))
                {
                    dialogService.ShowInformationModern("Merci de renseigner un mot de passe correct !", "Changement impossible");

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

                dialogService.ShowInformationModern("Le mot de passe a bien été modifié !", "Modifications enregistrées");

                Close();
            }, o => true);
        }

        #region Properties
        public string Username => user.Nom;

        private string _password;

        public string Password
        {
            get => _password;
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
