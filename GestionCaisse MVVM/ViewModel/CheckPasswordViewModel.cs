using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Entities;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class CheckPasswordViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public CheckPasswordViewModel(string windowToOpenAction)
        {
            CheckUserPassword = new RelayCommand(() =>
            {
                string username = LoginService.Instance.GetLoginContext().User.Nom;

                var dialogService = new DialogService();

                if (LoginService.Instance.Login(username, Password).ConnectionResult.Equals(ConnectionResult.Authorized))
                {
                    Hide();
                    switch (windowToOpenAction)
                    {
                        case "rollingback":
                            dialogService.ShowRollingBackWindow();
                            break;
                        case "administration":
                            dialogService.ShowAdministrationWindow();
                            break;
                    }
                    Close();
                }
                else
                {
                    dialogService.ShowInformationModern("Mot de passe incorrect !", "Erreur de connexion");
                    Password = null;
                }

            }, o => true);
        }

        #region Properties

        private SecureString _password;

        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand CheckUserPassword { get; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
