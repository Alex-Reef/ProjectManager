using ProjectManager.Models;
using ProjectManager.Utilites;

namespace ProjectManager.Controllers
{
    public class UserController : IController<User>
    {
        private Model Model { get; set; }
        public UserController(Model model) {
            Model = model;
        }

        public void Create(User user) {
            Model.Users.Add(user);
            Model.SaveUserData();
        }

        public void Update(User user) {
            var list = Model.Users;
            var index = list.FindIndex(x => x.UniqleID == user.UniqleID);
            list[index] = user;
            Model.SaveUserData();
        }
        public void Delete(User user) {
            var list = Model.Users;
            list.RemoveAll(x => x.UniqleID == user.UniqleID);
            Model.SaveUserData();
        }

        public User Authorization(string login, string password)
        {
            var list = Model.Users;
            foreach (var user in list)
            {
                if (user.Login == login && user.Password == DataSecurityService.CreateMD5(password))
                {
                    Model.SetUser(user);
                    return user;
                }
            }
            return null;
        }
    }
}
