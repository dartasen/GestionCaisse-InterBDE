using GestionCaisse_MVVM.Exceptions;
using GestionCaisse_MVVM.Model.Entities;
using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace GestionCaisse_MVVM.Model.Services
{
    public class LoginService
    {
        #region Singleton
        private static LoginService _instance;

        public static LoginService Instance
        {
            get
            {
                if (_instance == null) _instance = new LoginService();
                return _instance;
            }
        }

        private LoginService() { }
        #endregion

        private LoginContext _loginContext = new LoginContext();

        public LoginContext GetLoginContext()
        {
            return _loginContext;
        }

        public User Login(string username, SecureString password)
        {
            IntPtr passwordBSTR = default(IntPtr);
            string insecurePassword = "";

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

        private User IsUserAuthorizedToLogIn(string username, string plainTextPassword)
        {
            if (username == null || plainTextPassword == null) return null;
            string convertedPassword = CalculateMD5Hash(plainTextPassword);

            try
            {
                using (DBConnection context = new DBConnection())
                {
                    return ((Func<User>)(() =>
                    {
                        User user = context.Users.FirstOrDefault(x => x.Name.Equals(username) && x.PersonnalPassword.Equals(convertedPassword));
                        if (user == null) return null;
                        return !user.IsActive ? null : user;
                    }))();
                }
            }
            catch (EntityException ex)
            {
                throw new ConnectionFailedException(ex.Message, ex);
            }
        }

        private string CalculateMD5Hash(string input)
        {
            var md5 = MD5.Create();

            var inputBytes = Encoding.ASCII.GetBytes(input);

            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();

            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
