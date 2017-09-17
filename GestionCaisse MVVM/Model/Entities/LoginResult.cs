using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCaisse_MVVM.Model.Entities
{
    public class LoginResult
    {
        public LoginResult(ConnectionResult connectionResult, User user)
        {
            ConnectionResult = connectionResult;
            User = user;
        }

        public User User { get; }
        public ConnectionResult ConnectionResult { get; }
    }
}
