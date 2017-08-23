using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        private Random random;

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
            get { return _randomedSentence; }
            set { _randomedSentence = value; OnPropertyChanged(); }
        }


        public string WindowName
        {
            get => $"Connexion à l'application (v.{AppInformations.Version})";
        }

        #endregion

        public LoginViewModel()
        {
            random = new Random();
            var dialogService = new DialogService();
            _randomedSentence = GetRandomASentence();

            LoginService.ShowLoginWindow = () => { Show(); };
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
                        Hide();
                        dialogService.ShowMainWindow();
                    }
                    else
                    {
                        dialogService.ShowInformationWindow("Utilisateur ou mot de passe incorrect !",
                            "Erreur de connexion",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        Password = null;
                    }
                }
                catch (ConnectionFailedException ex)
                {
                    dialogService.ShowInformationWindow(
                        "Problème de connexion à la base de données !\n" + ex.InnerException.Message,
                        "Connexion impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
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

                int randomedNumber = random.Next(0, allLines.Length);

                return allLines[randomedNumber];
            }
            catch (IOException)
            {
                return @"Fichier 'Assets\extra\sentences.txt' illisible\introuvable !";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string p = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }
    }
}