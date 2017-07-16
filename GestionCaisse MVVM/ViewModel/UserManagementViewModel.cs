using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCaisse_MVVM.Model.Services;

namespace GestionCaisse_MVVM.ViewModel
{
    public class UserManagementViewModel
    {
        public UserManagementViewModel()
        {
            _users = UserService.GetUsers();
        }

        #region Properties
        private List<UserService.UserQueryResult> _users;

        public List<UserService.UserQueryResult> Users
        {
            get { return _users; }
            set { _users = value; }
        }
        #endregion
    }
}
