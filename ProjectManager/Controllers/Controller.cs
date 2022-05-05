using System;
using System.Windows.Controls;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class Controller
    {
        private Model model { get; set; }

        public MarkerController markerController { get; set; }
        public ProjectController projectController { get; set; }
        public TaskController taskController { get; set; }
        public SettingsController settingsController { get; set; }
        public UserController userController { get; set; }
        public NotificationsController notificationsController { get; set; }

        public Controller() { }

        public Controller(Model model)
        {
            this.model = model;
            markerController = new MarkerController(model);
            taskController = new TaskController(model);
            projectController = new ProjectController(model);
            settingsController = new SettingsController(model);
            userController = new UserController(model);
            notificationsController = new NotificationsController(model);
        }

        public Page OpenPage(string PageName)
        {
            return PagesNavigator.Open(PageName, model, this);
        }

        public Tuple<int, int> GetTaskPosition(Model model, Task task)
        {
            int PositionID = -1, CategoryID = -1;
            var list = model.GetCurentProject().Tasks;
            for (int i = 0; i < 3; i++)
            {
                PositionID = list[i].FindIndex(s => s.UniqleID == task.UniqleID);
                if (PositionID != -1)
                {
                    CategoryID = i;
                    break;
                }
            }

            return new Tuple<int, int>(PositionID, CategoryID);
        }
    }
}