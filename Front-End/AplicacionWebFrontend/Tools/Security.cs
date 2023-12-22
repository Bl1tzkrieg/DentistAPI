using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AplicacionWebFrontend.Tools
{
    public class Security
    {
        public static string Encryptar(string cadena)
        {
            return ConvertirSha256(cadena);
        }

        private static string ConvertirSha256(string cadena)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] resultado = hash.ComputeHash(encoding.GetBytes(cadena));

                foreach (byte b in resultado)
                {
                    sb.Append(b.ToString("X2"));
                }
            }

            return sb.ToString();
        }
    }
}