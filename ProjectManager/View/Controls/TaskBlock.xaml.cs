using System.Windows.Controls;
using ProjectManager.Models;
using System;
using System.Windows.Media.Imaging;

namespace ProjectManager.View.Controls
{
    public partial class TaskBlock : UserControl
    {
        public TaskBlock(Task task, Model model)
        {
            InitializeComponent();
            TaskName.Text = task.Name;
            if (task.ImagePath == null)
            {
                TaskDescription.Text = task.Description;
                ImageBox.Visibility = System.Windows.Visibility.Collapsed;
                TaskDescription.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                ImageBox.Source = new BitmapImage(new Uri(task.ImagePath));
                TaskDescription.Visibility = System.Windows.Visibility.Collapsed;
                ImageBox.Visibility = System.Windows.Visibility.Visible;
            }

            TaskSubtaskCount.Content = task.Subtasks.Count.ToString();

            TaskDate.Content = task.EndTime.ToString();
            foreach (var marker in task.MarkersID)
            {
                MarkerBlock markerBlock = new MarkerBlock(model.CurentProject.Markers.Find(x => x.UniqleID == marker));
                MarkerPanel.Children.Add(markerBlock);
            }
        }
    }
}
