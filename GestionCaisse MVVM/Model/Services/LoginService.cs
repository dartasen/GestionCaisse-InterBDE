using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Entities;

namespace GestionCaisse_MVVM.Model.Services
{
    public class LoginService
    {
        private readonly LoginContext _loginContext = new LoginContext();
        public static Action ShowLoginWindow;

        public LoginContext GetLoginContext()
        {
            return _loginContext;
        }

        public LoginResult Login(string username, SecureString password)
        {
            if (string.IsNullOrEmpty(username) || password == null)
            {
                return new LoginResult(ConnectionResult.NotFound, null);
            }

            var passwordBSTR = default(IntPtr);
            var insecurePassword = "";

            try
            {
                passwordBSTR = Marshal.SecureStringToBSTR(password);
                insecurePassword = Marshal.PtrToStringBSTR(passwordBSTR);
            }
            catch
            {
                insecurePassword = "";
            }

            return IsUserAuthorizedToLogIn(username, insecurePassword);
        }

        private LoginResult IsUserAuthorizedToLogIn(string username, string plainTextPassword)
        {
            string convertedPassword = CalculateMd5Hash(plainTextPassword, true);

            try
            {
                using (var context = new DBConnection())
                {
                    User user = context.User.FirstOrDefault(x => x.Nom.Equals(username) && x.CodePersonnel.Equals(convertedPassword));

                    if (user == null)
                    {
                        return new LoginResult(ConnectionResult.NotFound, null);
                    }

                    return !user.IsActive ? new LoginResult(ConnectionResult.Disabled, user) : new LoginResult(ConnectionResult.Authorized, user);
                }
            }
            catch (Exception ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        public static string CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();

            var inputBytes = Encoding.ASCII.GetBytes(input);

            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();

            foreach (var t in hash)
                sb.Append(t.ToString("X2"));

            return sb.ToString();
        }

        public static string CalculateMd5Hash(string input, bool test)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("X2"));
            }

            return hash.ToString();
        }

        public bool IsTimerActive;

        #region Singleton

        private static LoginService _instance;

        public static LoginService Instance => _instance ?? (_instance = new LoginService());

        private LoginService()
        {
        }

        #endregion
    }
}