using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Services;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows.Input;

namespace GestionCaisse_MVVM.ViewModel
{
    public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

        private LoginService _loginService = LoginService.Instance;

        #region Commands
        private ICommand _checkAndTryToLogin;
        private ICommand _quit;

        public ICommand CheckAndTryToLogin
        {
            get { return _checkAndTryToLogin; }
        }

        public ICommand Quit
        {
            get { return _quit; }
        }
        #endregion

        #region Properties
        private string _username;
        private SecureString _password;

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        public SecureString Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        #endregion

        public LoginViewModel()
        {
            var dialogService = new DialogService();

            _checkAndTryToLogin = new RelayCommand(() =>
            {
                try
                {
                    User user = _loginService.Login(_username, _password);

                    if (user != null)
                    {
                        _loginService.GetLoginContext().User = user;
                        _loginService.GetLoginContext().BuyingBDE = BDEService.GetBDEs().Where(x => x.idBDE == user.IdBDE).FirstOrDefault();
                        Password = null; //Makes the password box empty
                        dialogService.ShowMainWindow();
                    }
                    else
                    {
                        dialogService.ShowInformationWindow("Utilisateur ou mot de passe incorrect !",
                            "Erreur de connexion",
                            System.Windows.MessageBoxButton.OK,
                            System.Windows.MessageBoxImage.Error);
                    }
                }
                catch (ConnectionFailedException ex)
                {
                    dialogService.ShowInformationWindow("Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                        "Connexion impossible !", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }, o => true);

            _quit = new RelayCommand(() => Close(), o => true);
        }
    }
}
