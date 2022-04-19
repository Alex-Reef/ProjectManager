using System;
using System.Windows.Controls;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class Controller
    {
        private Model model { get; set; }

        public Controller() { }

        public Controller(Model model)
        {
            this.model = model;
        }

        public int SaveProject(string name, string desc)
        {
            model.SaveProject();
            return 0;
        }

        public int CreateProject(string name)
        {
            model.CreateProject(name);
            return 0;
        }

        public Page OpenPage(string PageName)
        {
            return PagesNavigator.Open(PageName, model, this);
        }

        public Tuple<int, int> GetTaskPosition(Model model, Task task)
        {
            int PositionID = -1, CategoryID = -1;
            var list = model.GetTasks();
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