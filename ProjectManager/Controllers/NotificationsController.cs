using ProjectManager.Models;
using System;
using System.Windows;

namespace ProjectManager.Controllers
{
    public class NotificationsController : IController<Notification>
    {
        private Model Model { get; set; }

        public NotificationsController(Model model)
        {
            this.Model = model;
        }

        public void Check() {
            var list = Model.CurentProject.Tasks;
            foreach(var tasklist in list)
            {
                foreach(var task in tasklist)
                {
                    var date = DateTime.Parse(task.EndTime);
                    if (date <= DateTime.Now)
                        Create(new Notification(
                            Application.Current.Resources["Deadline"].ToString(),
                            Application.Current.Resources["DeadlineMessage"].ToString(), 
                            Utilites.KeyGenerator.Generate(10))
                        );
                }
            }
        }

        public void Create(Notification value) {
            Model.Notifications.Add(value);
        }

        public void Update(Notification value) {
            int index = Model.Notifications.FindIndex(x => x.UniqleID == value.UniqleID);
            Model.Notifications[index] = value;
        }

        public void Delete(Notification value) {
            Model.Notifications.RemoveAll(x => x.UniqleID == value.UniqleID);
        }
    }
}
