using ProjectManager.Models;
using ProjectManager.Utilites;

namespace ProjectManager.Controllers
{
    public class UserController
    {
        public UserController() { }

        public void DeleteUser(string UserID)
        {

        }

        public void CreateUser(string Username, string Password, string Email)
        {
            Model model = new Model();
            model.CreateUser(Username, Password, Email, KeyGenerator.Generate(10));
        }

        public User Authentication(string Username, string Password)
        {
            Model model = new Model();
            foreach (var user in model.GetUsersList())
            {
                if (user.UserName == Username && user.Password == Password)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
