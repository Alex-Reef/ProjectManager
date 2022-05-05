using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectManager.Controllers;
using ProjectManager.Models;

namespace ProjectManager.View.Controls
{
    public partial class SubtaskListItem : UserControl
    {
        private Controller controller { get; set; }
        private Task task { get; set; }
        private Subtask subtask { get; set; }
        private TaskForm Window { get; set; }
        public SubtaskListItem(Subtask subtask, Task task, Controller controller, TaskForm window)
        {
            InitializeComponent();
            this.controller = controller;
            this.subtask = subtask;
            this.task = task;
            this.Window = window;
            SubtaskBlock.Text = subtask.Name;
            if (subtask.Complete)
                CheckBtn.Foreground = (SolidColorBrush)FindResource("Primary");
            else
                CheckBtn.Foreground = new SolidColorBrush(Colors.LightGray);
        }

        private void SubtaskGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            InputDialog inputDialog = new InputDialog("Enter Subtask name");
            inputDialog.ShowDialog();
            if (inputDialog.DialogResult == true)
            {
                SubtaskBlock.Text = inputDialog.GetString();
                subtask.Name = inputDialog.GetString();
                controller.taskController.Subtask_Update(subtask, task);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            controller.taskController.Subtask_Delete(subtask, task);
            Window.Update();
        }

        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            subtask.Complete = !subtask.Complete;
            controller.taskController.Subtask_Update(subtask, task);

            if (subtask.Complete)
                CheckBtn.Foreground = (SolidColorBrush)FindResource("Primary");
            else
                CheckBtn.Foreground = new SolidColorBrush(Colors.LightGray);
        }
    }
}
