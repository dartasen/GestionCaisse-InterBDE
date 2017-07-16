using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCaisse_MVVM.Exceptions;

namespace GestionCaisse_MVVM.Model.Services
{
    public class UserService
    {
        public static List<UserQueryResult> GetUsers()
        {
            try
            {
                using (var context = new DBConnection())
                {
                    var query =
                        from user in context.Users
                        join bde in context.BDEs on user.IdBDE equals bde.idBDE
                        orderby user.IdUser
                        select new UserQueryResult()
                        {
                            IdUser = user.IdUser,
                            Name = user.Name,
                            PersonnalPassword = user.PersonnalPassword,
                            BadgeID = user.BadgeID,
                            BDEName = bde.Name,
                            IsAdmin = user.IsAdmin,
                            IsActive =  user.IsActive
                        };

                    return query.ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public class UserQueryResult : User
        {
            public string BDEName { get; set; }

            public string FormatedIsActive => IsActive ? "O" : "X";
            public string FormatedIsAdmin => IsAdmin ? "O" : "X";
        }
    }
}
