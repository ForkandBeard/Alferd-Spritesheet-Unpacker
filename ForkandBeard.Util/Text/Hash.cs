using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ForkandBeard.Util.Text
{
    public class Hash
    {
        public static string HashString(string text)
        {
            MD5 m = MD5.Create();
            StringBuilder sb = new StringBuilder();

            byte[] hash = m.ComputeHash(System.Text.Encoding.ASCII.GetBytes(text));            
            foreach(byte b in hash)
            {
                sb.Append(b.ToString("x2")); 
            }
            return sb.ToString();
        }
    }
}
