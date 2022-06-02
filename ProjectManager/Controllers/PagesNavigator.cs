using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace ProjectManager.Controllers
{
    static class PagesNavigator
    {
        private static List<Tuple<string, Page>> pages;

        public static Page Open(string name, Model model, Controller controller)
        {
            pages = new List<Tuple<string, Page>>()
            {
                new Tuple<string, Page>("Dashboard", new DashboardPage(controller, model)),
                new Tuple<string, Page>("Tasks", new TaskPage(model, controller)),
                new Tuple<string, Page>("Archive", new ArchivePage(controller, model)),
                new Tuple<string, Page>("Settings", new SettingsPage(controller, model))
            };

            return pages.FirstOrDefault(x => x.Item1 == name).Item2;
        }
    }
}
