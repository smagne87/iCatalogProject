using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace iCatalogBB
{
    internal class CryptorEngine
    {
        public static string Encrypt(string ToEncrypt, bool useHasing)
        {
            byte[] key = Encoding.ASCII.GetBytes("icatalogDR0wSS@P6660juht");
            byte[] iv = Encoding.ASCII.GetBytes("iCatalog");
            byte[] data = Encoding.ASCII.GetBytes(ToEncrypt);
            byte[] enc = new byte[0];
            TripleDES tdes = TripleDES.Create();
            tdes.IV = iv;
            tdes.Key = key;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.Zeros;
            ICryptoTransform ict = tdes.CreateEncryptor();
            enc = ict.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(enc);
        }

        public static string Decrypt(string cypherString, bool useHasing)
        {
            byte[] key = Encoding.ASCII.GetBytes("icatalogDR0wSS@P6660juht");
            byte[] iv = Encoding.ASCII.GetBytes("iCatalog");
            byte[] data = Convert.FromBase64String(cypherString);
            byte[] enc = new byte[0];
            TripleDES tdes = TripleDES.Create();
            tdes.IV = iv;
            tdes.Key = key;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.Zeros;
            ICryptoTransform ict = tdes.CreateDecryptor();
            enc = ict.TransformFinalBlock(data, 0, data.Length);
            return UTF8Encoding.UTF8.GetString(enc, 0, enc.Length);
        }
    }
}
