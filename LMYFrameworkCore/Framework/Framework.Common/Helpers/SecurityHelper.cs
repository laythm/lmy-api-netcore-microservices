using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Common
{
    public static class SecurityHelper
    {
        private const string _alg = "HmacSHA256";
        private const string seperator = "~";
        private const string _salt = "ytAXTluFAI";

        public static string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, _salt });
            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                return Convert.ToBase64String(hmac.Hash);
            }
        }

        public static string GenerateToken(string username, string password, string ip, string userAgent, long ticks)
        {
            string ticksEncrypted = EncryptionHelper.Encrypt(ticks.ToString());

            string hash = string.Join(seperator, new string[] { username, ip, userAgent, ticksEncrypted });
            string hashLeft = "";
            string hashRight = "";
            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(seperator, new string[] { username, ticksEncrypted });
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(seperator, hashLeft, hashRight)));
        }

        public static string GetUserIDFromToken(string token)
        {
            try
            {
                var parts = GetTokenParts(token);

                string userid = parts[1];

                return userid;

            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public static bool IsTokenValid(string token, string ip, string userAgent, int expirationMinutes, string password)
        {
            bool result = false;
            try
            {
                var parts = GetTokenParts(token);
                if (parts.Length == 3)
                {
                    // Get the hash message, userid, and timestamp.
                    string hash = parts[0];
                    string userid = parts[1];
                    string ticks = parts[2];
                    long ticksDecrypted = long.Parse(EncryptionHelper.Decrypt(ticks.ToString()));

                    DateTime timeStamp = new DateTime(ticksDecrypted);
                    // Ensure the timestamp is valid.
                    bool expired = Math.Abs((DateTime.Now - timeStamp).TotalMinutes) > expirationMinutes;
                    if (!expired)
                    {
                        // Hash the message with the key to generate a token.
                        string computedToken = GenerateToken(userid, password, ip, userAgent, ticksDecrypted);
                        // Compare the computed token with the one supplied and ensure they match.
                        result = (token == computedToken);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        private static string[] GetTokenParts(string token)
        {
            // Base64 decode the string, obtaining the token:userid:timeStamp.
            string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            // Split the parts.
            string[] parts = key.Split(new char[] { char.Parse(seperator) });
            return parts;
        }
    }
}
