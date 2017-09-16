using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class UserManagementViewModel : INotifyPropertyChanged
    {
        public UserManagementViewModel()
        {
            _users = UserService.GetUsers();

            ActivateDeactivateUser = new RelayCommand(() =>
            {
                if (SelectedUser == null) return;
                UserService.ToggleUserConnectionRights(SelectedUser);
                _users = UserService.GetUsers();
                OnPropertyChanged(nameof(Users));
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
        public ICommand ActivateDeactivateUser { get; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string p = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
