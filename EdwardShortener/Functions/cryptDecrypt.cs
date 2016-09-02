using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardShortener.Functions
{
    public class cryptDecrypt
    {

        private static string pass = "66DA80D6-E8CD-48A3-AEBE-A31BDEF3BAFC";
        private static string salt = "66DA80D6-E8CD-48A3-AEBE-A31BDEF3BAFC";
        private static string algorithm = "SHA1";
        

        public static string cryptPass (string userPass)
        {
             return RijndaelEncryptDecrypt.EncryptDecryptUtils.Encrypt(userPass, pass, salt, algorithm);
        }

        public static string decryptPass (string cryptedPass)
        {
            return RijndaelEncryptDecrypt.EncryptDecryptUtils.Decrypt(cryptedPass, pass, salt, algorithm);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        


        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

    }
}