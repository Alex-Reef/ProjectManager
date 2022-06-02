using System;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ProjectManager.Controllers;
using ProjectManager.View.Controls;
using ProjectManager.Models;

namespace ProjectManager
{
    public partial class DashboardPage : Page
    {
        private Model Model { get; set; }
        private Controller Controller { get; set; }
        private Project project { get; set; } 
        public DashboardPage(Controller controller, Model Model)
        {
            InitializeComponent();

            this.Model = Model;
            this.Controller = controller;
            this.project = Model.CurentProject;
            var tasks = project.Tasks[0];
            var list = tasks.Skip(Math.Max(0, tasks.Count() - 5));
            foreach(var item in list)
                ListBox.Children.Add(new TaskListItem(Model, Controller, item));

            Username.Text = Model.CurentUser.UserName;
            Email.Text = Model.CurentUser.Email;

            if (File.Exists(Model.CurentUser.ImagePath))
            {
                UserImage.Visibility = System.Windows.Visibility.Visible;
                UserImageIcon.Visibility = System.Windows.Visibility.Collapsed;
                UserImage.Background = new ImageBrush(new BitmapImage(new Uri(Model.CurentUser.ImagePath, UriKind.Absolute)));
            }
            else
            {
                UserImageIcon.Visibility=System.Windows.Visibility.Visible;
                UserImage.Visibility = System.Windows.Visibility.Collapsed;
            }

            UsingMarkers.Text = project.Markers.Count.ToString();
            MarkersCount.Text = Model.MaxMarkers.ToString();
            MarkerPorgress.Value = (project.Markers.Count / Model.MaxMarkers) * 100;

            int countComplete = 0;
            int count = 0;
            foreach (var column in project.Tasks)
            {
                foreach(var item in column)
                {
                    count += item.Subtasks.Count;
                    foreach(var subtasks in item.Subtasks)
                    {
                        if(subtasks.Complete)
                            countComplete++;
                    }
                }
            }
            SubtaskComplete.Text = countComplete.ToString();
            SubtaskTotal.Text = count.ToString();
            if (count > 0)
                SubtaskProgress.Value = (countComplete / count) * 100;
            else
                SubtaskProgress.Value = 0;

            // ----
            count = 0;
            foreach (var column in project.Tasks)
            {
                count += column.Count;
            }

            TasksNow.Text = count.ToString();
            MaxTasks.Text = Model.MaxTasks.ToString();
            if (count > 0)
                TasksCount.Value = (count / Model.MaxTasks) * 100;
            else
                TasksCount.Value = 0;
        }
    }
}
