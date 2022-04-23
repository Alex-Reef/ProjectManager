using System.Collections.Generic;

namespace ProjectManager.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User(string Username, string Password, string Email, string ID)
        {
            this.UserName = Username;
            this.UserID = ID;
            this.Password = Password;
            this.Email = Email;
        }
    }
}
