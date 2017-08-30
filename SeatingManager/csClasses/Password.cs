﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace SeatingManager.csClasses
{
    class Password
    {
        public static byte[] Hash(string value, string salt)
        {
            return Hash(Encoding.UTF8.GetBytes(value), Encoding.UTF8.GetBytes(salt));
        }

        public static byte[] Hash(byte[] value, byte[] salt)
        {
            byte[] saltedValue = value.Concat(salt).ToArray();

            return new SHA256Managed().ComputeHash(saltedValue);
        }

        private static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public static bool ConfirmPassword(string username, string password)
        {
            var context = new SeatingManager.SeatingManagerDBEntities();
            string recordedPassword = (from u in context.users
                                       where u.firstName == username
                                       select u.password).ToString();
            string salt = (from u in context.users
                           where u.firstName == username
                           select u.salt).ToString();
            byte[] passwordHash = Hash(password, salt);
            byte[] recordedHash = Encoding.UTF8.GetBytes(recordedPassword);
            return recordedHash.SequenceEqual(passwordHash);
        }
    }
}