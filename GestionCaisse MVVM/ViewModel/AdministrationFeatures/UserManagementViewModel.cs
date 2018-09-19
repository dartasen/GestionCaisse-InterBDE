using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel.AdministrationFeatures
{
    public class UserManagementViewModel : INotifyPropertyChanged
    {
        public UserManagementViewModel()
        {
            Users = UserService.GetUsers();

            var dialogService = new DialogService();

            ActivateDeactivateUser = new RelayCommand(() =>
            {
                if (SelectedUser == null)
                {
                    dialogService.ShowInformationModern("Vous devez sélectionner un utilisateur !",
                        "Opération impossible !");
                    return;
                }

                if (SelectedUser.IdUtilisateur.Equals(LoginService.Instance.GetLoginContext().User.IdUtilisateur))
                {
                    dialogService.ShowInformationModern("Vous ne pouvez-pas désactiver votre propre compte",
                        "Opération interdite !");
                    return;
                }

                UserService.ToggleUserConnectionRights(SelectedUser);
                Users = UserService.GetUsers();

                OnPropertyChanged(nameof(Users));
            }, o => true);

            ToggleIsUserAdmin = new RelayCommand(() =>
            {
                if (SelectedUser == null)
                {
                    dialogService.ShowInformationModern("Vous devez sélectionner un utilisateur !",
                        "Opération impossible !");
                    return;
                }

                if (SelectedUser.IdUtilisateur.Equals(LoginService.Instance.GetLoginContext().User.IdUtilisateur))
                {
                    dialogService.ShowInformationModern(
                        "Vous ne pouvez-pas vous révoquer votre droit administrateur vous-même",
                        "Opération interdite !");
                    return;
                }

                UserService.ToggleIsAdminUser(SelectedUser.IdUtilisateur);
                Users = UserService.GetUsers();

                OnPropertyChanged(nameof(Users));
            }, o => true);

            OpenAddUserView = new RelayCommand(() =>
            {
                dialogService.ShowAddUserView();
                Users = UserService.GetUsers();

                OnPropertyChanged(nameof(Users));
            }, o => true);

            DeleteUser = new RelayCommand(() =>
            {
                try
                {
                    if (SelectedUser == null)
                    {
                        dialogService.ShowInformationModern("Vous devez sélectionner un utilisateur !",
                            "Opération impossible !");
                        return;
                    }

                    if (SelectedUser.IdUtilisateur.Equals(LoginService.Instance.GetLoginContext().User.IdUtilisateur))
                    {
                        dialogService.ShowInformationModern("Vous ne pouvez-pas vous supprimer vous-même",
                            "Opération interdite !");
                        return;
                    }

                    var result = dialogService.ShowInformationWindow(
                        "Voulez-vous vraiment supprimer cet utilisateur ? Une désactivation est souvent préférable : elle permet de conserver l'historique de vente !!!",
                        "Confirmation de l'opération",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result.Equals(MessageBoxResult.Yes))
                    {
                        UserService.DeleteUser(SelectedUser.IdUtilisateur);
                        Users = UserService.GetUsers();
                        OnPropertyChanged(nameof(Users));
                    }
                }
                catch (Exception e)
                {
                    dialogService.ShowInformationModern("Erreur :\n" + e, "Suppression impossible !");
                }
            }, o => true);

            ChangePassword = new RelayCommand(() =>
            {
                try
                {
                    if (SelectedUser == null)
                    {
                        dialogService.ShowInformationModern("Vous devez sélectionner un utilisateur !",
                            "Opération impossible !");
                        return;
                    }

                    dialogService.ShowChangePasswordView(SelectedUser);
                }
                catch (Exception e)
                {
                    dialogService.ShowInformationModern("Erreur :\n" + e, "Modification impossible !");
                }
            }, o => true);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

        #region Properties

        public List<QueryUser> Users { get; set; }

        private QueryUser _selectedUser;

        public QueryUser SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand ToggleIsUserAdmin { get; }
        public ICommand ActivateDeactivateUser { get; }
        public ICommand OpenAddUserView { get; }
        public ICommand DeleteUser { get; }
        public ICommand ChangePassword { get; }

        #endregion
    }
}