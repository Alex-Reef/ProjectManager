using Newtonsoft.Json;
using ProjectManager.Models;
using System;
using System.IO;

namespace ProjectManager.Controllers
{
    public class UserController : IController<User>
    {
        private Model Model { get; set; }
        public UserController(Model model) {
            Model = model;
        }

        public void Create(User user) {
            Model.GetUsers().Add(user);
            Model.SaveUserData();
        }

        public void Update(User user) {
            var list = Model.GetUsers();
            var index = list.FindIndex(x => x.UniqleID == user.UniqleID);
            list[index] = user;
            Model.SaveUserData();
        }
        public void Delete(User user) {
            var list = Model.GetUsers();
            list.RemoveAll(x => x.UniqleID == user.UniqleID);
            Model.SaveUserData();
        }
    }
}
