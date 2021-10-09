using System;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public static class Criptografia
    {
       public static string TxtCript(string normalTxt) {
           MD5 hash = MD5.Create();

           byte[] byteNonCript = Encoding.Default.GetBytes(normalTxt);
           byte[] bytesCript = hash.ComputeHash(byteNonCript);

           StringBuilder SB = new StringBuilder();

           foreach(byte b in bytesCript){
               string DebugB = b.ToString("x2");
               SB.Append(DebugB);
           }

           return SB.ToString();
       } 
    }
}