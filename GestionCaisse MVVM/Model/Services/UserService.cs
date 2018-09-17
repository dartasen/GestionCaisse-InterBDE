using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Windows;
using GestionCaisse_MVVM.Exceptions;

namespace GestionCaisse_MVVM.Model.Services
{
    public class UserService
    {
        public static List<QueryUser> GetUsers()
        {
            try
            {
                using (var context = new DBConnection())
                {
                    var query =
                        from user in context.User
                        join bde in context.BDE on user.IdBDE equals bde.Id
                        orderby user.IdUtilisateur
                        select new QueryUser()
                        {
                            IdUtilisateur = user.IdUtilisateur,
                            Nom = user.Nom,
                            CodePersonnel = user.CodePersonnel,
                            CodeBadge = user.CodeBadge,
                            BDEName = bde.Nom,
                            IsAdmin = user.IsAdmin,
                            IsActive = user.IsActive
                        };

                    return query.ToList();
                }
            }
            catch (Exception ex)
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
                    context.User.FirstOrDefault(x => x.IdUtilisateur == user.IdUtilisateur).IsActive = !user.IsActive;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
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
                        Nom = name,
                        CodePersonnel = clearTextPassword,
                        IdBDE = bde.Id,
                        IsActive = isActive,
                        IsAdmin = isAdmin,
                    };

                    context.User.Add(userToAdd);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static void ChangeUserPassword(User user, string plaintextPassword)
        {
            try
            {
                using (var context = new DBConnection())
                {
                    string hashedPassword = LoginService.CalculateMd5Hash(plaintextPassword);
                    context.User.FirstOrDefault(x => x.IdUtilisateur == user.IdUtilisateur).CodePersonnel = hashedPassword;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
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
                    context.User.Remove(context.User.FirstOrDefault(x => x.IdUtilisateur == userId));
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
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
                    var user = context.User.FirstOrDefault(x => x.IdUtilisateur == userId);
                    user.IsAdmin = !user.IsAdmin;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
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
                    List<User> users = context.User.ToList();

                    foreach (var user in users)
                    {
                        var query = context.History.Count(x => x.IdUtilisateur == user.IdUtilisateur
                                                               && x.DateVente.Month == month
                                                               && x.DateVente.Year == DateTime.Now.Year);

                        scores.Add(new UserRankQueryResult()
                        {
                            Username = user.Nom,
                            Quantity = query
                        });
                    }
                    return scores.OrderByDescending(x => x.Quantity);
                }
            }
            catch (Exception ex)
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

                    var query = context.History.Where(x => x.IdUtilisateur == user.IdUtilisateur
                                                           && x.DateVente.Day == DateTime.Now.Day
                                                           && x.DateVente.Month == DateTime.Now.Month
                                                           && x.DateVente.Year == DateTime.Now.Year)
                        .GroupBy(x => x.DateVente);

                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public class UserRankQueryResult
        {
            public string Username { get; set; }
            public int Quantity { get; set; }
        }
    }
}