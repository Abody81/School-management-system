using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SMS.Business.UserServices
{
    internal class CryptographyHelper
    {
        internal static bool _IsPasswordMatch(string NewPassword, string storedHash, string storedSalt)
        {
            string inputHash = _ComputeHash(NewPassword, storedSalt);
            return inputHash == storedHash;
        }

        internal static string _ComputeHash(string input, string Salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input + Salt));

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        internal static string _GenerateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(24);

            string saltString = Convert.ToBase64String(salt);

            return saltString;
        }
    }
}
