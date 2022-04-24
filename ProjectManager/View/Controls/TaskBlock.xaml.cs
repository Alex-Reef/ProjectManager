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
            TaskDate.Content = task.EndTime.ToString();
            foreach(var marker in task.MarkersID)
            {
                MarkerBlock markerBlock = new MarkerBlock(model.GetCurentProject().Markers.Find(x => x.UniqleID == marker));
                MarkerPanel.Children.Add(markerBlock);
            }
        }
    }
}
