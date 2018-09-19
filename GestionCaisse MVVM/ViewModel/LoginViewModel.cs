using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Entities;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private LoginService _loginService { get; } = LoginService.Instance;

        public event PropertyChangedEventHandler PropertyChanged;

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

        private string _randomedSentence;

        public string RandomedSentence
        {
            get => _randomedSentence;
            set { _randomedSentence = value; OnPropertyChanged(); }
        }

        public string WindowName => $"Connexion à l'application (v.{AppInformations.Version})";

        #endregion

        public LoginViewModel()
        {
            var dialogService = new DialogService();
            RandomedSentence = GetRandomASentence();

            LoginService.ShowLoginWindow = () => { Show(); };

            CheckAndTryToLogin = new RelayCommand(() =>
            {
                try
                {
                    var connection = _loginService.Login(_username, _password);

                    if (connection.ConnectionResult.Equals(ConnectionResult.Authorized))
                    {
                        _loginService.GetLoginContext().User = connection.User;
                        _loginService.GetLoginContext().BuyingBDE = BDEService
                            .GetBDEs().FirstOrDefault(x => x.Id == connection.User.IdBDE);
                        Hide();
                        dialogService.ShowMainWindow();
                    }
                    else if (connection.ConnectionResult.Equals(ConnectionResult.Disabled))
                    {
                        dialogService.ShowInformationModern("Votre compte a été désactivé ! Contactez un administrateur !",
                            "Erreur de connexion" );
                    }
                    else
                    {
                        dialogService.ShowInformationModern("Utilisateur ou mot de passe incorrect !",
                            "Erreur de connexion");
                    }
                }
                catch (Exception ex)
                {
                    dialogService.ShowInformationModern("Problème de connexion à la base de données ! " + ex.InnerException.Message,
                            "Erreur de connexion");
                }

                RandomedSentence = GetRandomASentence();

            }, o => true);

            Quit = new RelayCommand(() => Close(), o => true);
        }

        private string GetRandomASentence()
        {
            try
            {
                string[] allLines = File.ReadAllLines(@"Assets\extra\sentences.txt");

                int randomedNumber = new Random().Next(0, allLines.Length);

                return allLines[randomedNumber];
            }
            catch (IOException)
            {
                return @"Fichier 'Assets\extra\sentences.txt' illisible\introuvable !";
            }
        }

        private void OnPropertyChanged([CallerMemberName] string p = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }
    }
}