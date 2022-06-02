using System.Windows.Controls;
using ProjectManager.Controllers;
using ProjectManager.Models;

namespace ProjectManager.View.Controls
{
    public partial class TaskListItem : UserControl
    {
        private Model Model { get; set; }
        private Controller Controller { get; set; }
        private Task Task { get; set; }

        public TaskListItem(Model model, Controller controller, Task task)
        {
            InitializeComponent();
            this.Controller = controller;
            this.Model = model;
            this.Task = task;

            Title.Text = Task.Name;
            foreach(var item in Task.MarkersID)
            {
                var index = Model.CurentProject.Markers.FindIndex(x => x.UniqleID == item);
                MarkerBox.Children.Add(new MarkerBlock(Model.CurentProject.Markers[index]));
            }
            EndTime.Text = Task.EndTime;
        }
    }
}
