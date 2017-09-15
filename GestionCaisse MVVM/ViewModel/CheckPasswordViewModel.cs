using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class CheckPasswordViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public CheckPasswordViewModel(string windowToOpenAction)
        {
            CheckUserPassword = new RelayCommand(() =>
            {
                string username = LoginService.Instance.GetLoginContext().User.Name;

                var dialogService = new DialogService();

                if (LoginService.Instance.Login(username, Password) != null)
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
                    dialogService.ShowInformationWindow("Mot de passe incorrect !",
                        "Erreur de connexion",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
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
