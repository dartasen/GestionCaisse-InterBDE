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

        public static void ToggleUserConnectionRights(User user)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    context.Users.FirstOrDefault(x => x.IdUser == user.IdUser).IsActive = !user.IsActive;
                    context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static bool AddUser(string name, string clearTextPassword, BDE bde, bool isActive, bool isAdmin)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    var userToAdd = new User()
                    {
                        Name = name,
                        PersonnalPassword = clearTextPassword,
                        IdBDE = bde.idBDE,
                        IsActive = isActive,
                        IsAdmin = isAdmin,
                    };

                    context.Users.Add(userToAdd);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static void DeleteUser(int userId)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    context.Users.Remove(context.Users.FirstOrDefault(x => x.IdUser == userId));
                    context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static void ToggleIsAdminUser(int userId)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    var user = context.Users.FirstOrDefault(x => x.IdUser == userId);
                    user.IsAdmin = !user.IsAdmin;
                    context.SaveChanges();
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
                    var query = context.History.Where(x => x.IdUser == user.IdUser
                                                           && x.SaleDate.Day == DateTime.Now.Day
                                                           && x.SaleDate.Month == DateTime.Now.Month
                                                           && x.SaleDate.Year == DateTime.Now.Year)
                        .GroupBy(x => x.SaleDate);

                    return query.Count();
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