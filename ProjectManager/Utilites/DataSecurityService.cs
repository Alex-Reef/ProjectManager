using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProjectManager.Utilites
{
    public static class DataSecurityService
    {
        public static string CreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidLogin(Model model, string login)
        {
            if (model.GetUsers().Find(x => x.Login == login) == null)
                return true;
            else
                return false;
        }
    }
}
