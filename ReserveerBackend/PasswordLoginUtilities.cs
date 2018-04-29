using ReserveerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ReserveerBackend
{
    public static class PasswordLoginUtilities
    {
        const int iterations = 10000;
        const int saltsize = 16;
        const int hashsize = 20;

        public static UserPasswordLogin GenerateNewLogin(string username, string password)
        {
            byte[] salt = new byte[saltsize];
            new RNGCryptoServiceProvider().GetBytes(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(hashsize);

            var userpasslogin = new UserPasswordLogin();
            userpasslogin.HashedPassword = hash;
            userpasslogin.Salt = salt;
            userpasslogin.Username = username;
            return userpasslogin;
        }

        public static bool CheckLogin(string Password, UserPasswordLogin passworddata)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(Password, passworddata.Salt, iterations);
            var computedhash = pbkdf2.GetBytes(hashsize);

            return Enumerable.SequenceEqual(computedhash, passworddata.HashedPassword);
        }
    }
}
