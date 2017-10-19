using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class UserManagementViewModel : INotifyPropertyChanged
    {
        public UserManagementViewModel()
        {
            _users = UserService.GetUsers();

            DialogService dialogService = new DialogService();

            ActivateDeactivateUser = new RelayCommand(() =>
            {
                if (SelectedUser == null)
                {
                    dialogService.ShowInformationWindow("Vous devez sélectionner un utilisateur !",
                        "Opération impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (SelectedUser.IdUser.Equals(LoginService.Instance.GetLoginContext().User.IdUser))
                {
                    DialogService _dialogService = new DialogService();
                    _dialogService.ShowInformationWindow("Vous ne pouvez-pas désactiver votre propre compte",
                        "Opération interdite !", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                UserService.ToggleUserConnectionRights(SelectedUser);
                _users = UserService.GetUsers();
                OnPropertyChanged(nameof(Users));
            }, o => true);

            ToggleIsUserAdmin = new RelayCommand(() =>
            {
                if (SelectedUser == null)
                {
                    dialogService.ShowInformationWindow("Vous devez sélectionner un utilisateur !",
                        "Opération impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (SelectedUser.IdUser.Equals(LoginService.Instance.GetLoginContext().User.IdUser))
                {
                    DialogService _dialogService = new DialogService();
                    _dialogService.ShowInformationWindow("Vous ne pouvez-pas vous révoquer votre droit administrateur vous-même",
                        "Opération interdite !", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                UserService.ToggleIsAdminUser(SelectedUser.IdUser);
                _users = UserService.GetUsers();
                OnPropertyChanged(nameof(Users));
            }, o => true);

            OpenAddUserView = new RelayCommand(() =>
            {
                dialogService.ShowAddUserView();
                _users = UserService.GetUsers();
                OnPropertyChanged(nameof(Users));
            } ,o => true);

            DeleteUser = new RelayCommand(() =>
            {
                try
                {
                    if (SelectedUser == null)
                    {
                        dialogService.ShowInformationWindow("Vous devez sélectionner un utilisateur !",
                            "Opération impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (SelectedUser.IdUser.Equals(LoginService.Instance.GetLoginContext().User.IdUser))
                    {
                        DialogService _dialogService = new DialogService();
                        _dialogService.ShowInformationWindow("Vous ne pouvez-pas vous supprimer vous-même",
                            "Opération interdite !", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    MessageBoxResult result = dialogService.ShowInformationWindow("Voulez-vous vraiment supprimer cet utilisateur ? Une désactivation est souvent préférable : elle permet de conserver l'historique de vente !!!",
                        "Confirmation de l'opération",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        UserService.DeleteUser(SelectedUser.IdUser);
                        _users = UserService.GetUsers();
                        OnPropertyChanged(nameof(Users));
                    }
                }
                catch (Exception e)
                {
                    dialogService.ShowInformationWindow("Erreur :\n" + e, "Suppression impossible !", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }, o => true);

            ChangePassword = new RelayCommand(() =>
            {
                try
                {
                    if (SelectedUser == null)
                    {
                        dialogService.ShowInformationWindow("Vous devez sélectionner un utilisateur !",
                            "Opération impossible !", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    dialogService.ShowChangePasswordView(SelectedUser);
                }
                catch (Exception e)
                {
                    dialogService.ShowInformationWindow("Erreur :\n" + e, "Modification impossible !", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }, o => true);
        }

        #region Properties
        private List<UserService.UserQueryResult> _users;

        public List<UserService.UserQueryResult> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        private UserService.UserQueryResult _selectedUser;

        public UserService.UserQueryResult SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands
        public ICommand ToggleIsUserAdmin { get; }
        public ICommand ActivateDeactivateUser { get; }
        public ICommand OpenAddUserView { get; }
        public ICommand DeleteUser { get; }
        public ICommand ChangePassword { get; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
