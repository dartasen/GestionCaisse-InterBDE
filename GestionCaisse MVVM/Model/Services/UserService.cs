using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public static IOrderedEnumerable<UserRankQueryResult> RankUsersBySellsForAMonth(int month)
        {
            try
            {
                using (var context = new DBConnection())
                { 
                    var scores = new List<UserRankQueryResult>();
                    List<User> users = context.Users.ToList();

                    foreach (var user in users)
                    {
                        var query = context.History.Count(x => x.IdUser == user.IdUser
                                                               && x.SaleDate.Month == month
                                                               && x.SaleDate.Year == DateTime.Now.Year);
                        scores.Add(new UserRankQueryResult()
                        {
                            Username = user.Name,
                            Quantity = query
                        });
                    }
                    return scores.OrderByDescending(x => x.Quantity);
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static int SellsMadeByUserToday(User user)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    var query = context.History.Count(x => x.IdUser == user.IdUser 
                                                           && x.SaleDate.Day == DateTime.Now.Day 
                                                           && x.SaleDate.Month == DateTime.Now.Month 
                                                           && x.SaleDate.Year == DateTime.Now.Year);
                    return query;
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
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
