using System.Windows.Controls;
using System.Windows.Media;
using ProjectManager.Models;
using System;

namespace ProjectManager.View.Controls
{
    public partial class TaskBlock : UserControl
    {
        public TaskBlock(Task task, string markerText, Brush markerColor)
        {
            InitializeComponent();
            TaskName.Content = task.Name;
            TaskDescription.Text = task.Description;
            MarkerText.Content = markerText;
            MarkerCard.Background = markerColor;
            DateTime date = new DateTime();
            date = DateTime.Parse(task.EndTime.ToString());
            TaskDate.Content = date.ToString("d MMMM");
            MarkerCard.Width = markerText.Length + 55;
        }
    }
}
