using System.Windows.Controls;
using System.Windows.Media;
using ProjectManager.Models;
using System;

namespace ProjectManager.View.Controls
{
    public partial class TaskBlock : UserControl
    {
        public TaskBlock(Task task, Model model)
        {
            InitializeComponent();
            TaskName.Text = task.Name;
            TaskDescription.Text = task.Description;
            DateTime date = new DateTime();
            date = DateTime.Parse(task.EndTime.ToString());
            TaskDate.Content = date.ToString("d MMMM");
            foreach(var marker in task.MarkersID)
            {
                MarkerBlock markerBlock = new MarkerBlock(model.GetMarkers().Find(x => x.UniqleID == marker));
                MarkerPanel.Children.Add(markerBlock);
            }
        }
    }
}
