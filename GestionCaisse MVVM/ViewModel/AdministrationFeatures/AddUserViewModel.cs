using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel.AdministrationFeatures
{
    public class AddUserViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public AddUserViewModel()
        {

            AddUser = new RelayCommand(() =>
            {
                DialogService dialogService = new DialogService();

                if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password) || SelectedBde == null)
                {
                    dialogService.ShowInformationModern("Vérifier qu'aucun champ n'est vide !", "Formulaire invalide !");
                    return;
                }

                UserService.AddUser(Login, LoginService.CalculateMd5Hash(Password), SelectedBde, IsActive, IsAdmin);
                Close();
            }, o => true);
            CloseWindow = new RelayCommand(() => Close(), o => true);
        }

        #region Properties
        private string _login;

        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(); }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public IEnumerable<BDE> Bdes => BDEService.GetBDEs();

        private BDE _selecteBde;

        public BDE SelectedBde
        {
            get => _selecteBde;
            set { _selecteBde = value; OnPropertyChanged(); }
        }

        private bool _isActive;

        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; OnPropertyChanged(); }
        }

        private bool _isAdmin;

        public bool IsAdmin
        {
            get => _isAdmin;
            set { _isAdmin = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands
        public ICommand AddUser { get; }
        public ICommand CloseWindow { get; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
