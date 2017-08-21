using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                            IsActive = user.IsActive
                        };

                    return query.ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static IEnumerable<UserRankQueryResult> RankUsersBySellsForCurrentMonth()
        {
            DateTime beginningOfTheMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            try
            {
                using (var context = new DBConnection())
                { 
                    var query = 
                        context.History
                        .GroupBy(x => x.IdUser)
                        .Select(y => new UserRankQueryResult()
                        {
                            Username = context.Users.FirstOrDefault(name => name.IdUser == (context.Users.FirstOrDefault().IdUser)).Name,
                            Quantity = y.Count()
                        });
                    return query.ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
            return null;
        }

        public class UserRankQueryResult
        {
            public string Username { get; set; }
            public int Quantity { get; set; }
        }

        public class UserQueryResult : User
        {
            public string BDEName { get; set; }

            public string FormatedIsActive => IsActive ? "O" : "X";
            public string FormatedIsAdmin => IsAdmin ? "O" : "X";
        }
    }
}
