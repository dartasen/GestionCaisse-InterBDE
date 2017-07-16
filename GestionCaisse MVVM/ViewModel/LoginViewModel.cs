using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly LoginService _loginService = LoginService.Instance;

        #region Commands

        public ICommand CheckAndTryToLogin { get; }

        public ICommand Quit { get; }

        #endregion

        #region Properties

        private string _username;
        private SecureString _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string WindowName
        {
            get => $"Connexion à l'application (v.{AppInformations.Version})";
        }

        #endregion

        public LoginViewModel()
        {          
            var dialogService = new DialogService();

            CheckAndTryToLogin = new RelayCommand(() =>
            {
                try
                {
                    var user = _loginService.Login(_username, _password);

                    if (user != null)
                    {
                        _loginService.GetLoginContext().User = user;
                        _loginService.GetLoginContext().BuyingBDE = BDEService.GetBDEs()
                            .Where(x => x.idBDE == user.IdBDE).FirstOrDefault();
                        Password = null; //Makes the password box empty
                        dialogService.ShowMainWindow();
                    }
                    else
                    {
                        dialogService.ShowInformationWindow("Utilisateur ou mot de passe incorrect !",
                            "Erreur de connexion",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
                catch (ConnectionFailedException ex)
                {
                    dialogService.ShowInformationWindow(
                        "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                        "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, o => true);

            Quit = new RelayCommand(() => Close(), o => true);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string p = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }
    }
}