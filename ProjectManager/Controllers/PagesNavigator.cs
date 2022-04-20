using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    static class PagesNavigator
    {
        private static List<Tuple<string, Page>> pages;

        public static Page Open(string name, Model model, Controller controller)
        {
            pages = new List<Tuple<string, Page>>()
            {
                new Tuple<string, Page>("Tasks", new TaskPage(model, controller)),
                new Tuple<string, Page>("Settings", new SettingsPage(controller))
            };

            return pages.FirstOrDefault(x => x.Item1 == name).Item2;
        }
    }
}
