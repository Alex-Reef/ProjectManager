using System.Windows.Controls;
using ProjectManager.Controllers;
using ProjectManager.View.Controls;
using ProjectManager.Models;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjectManager
{
    public partial class ArchivePage : Page
    {
        private Model Model { get; set; }
        private Controller Controller { get; set; }
        private Task SelectTask { get; set; }

        public ArchivePage(Controller controller, Model Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.Controller = controller;

            Update();
        }

        private void Update()
        {
            ArchivePanel.Children.Clear();
            foreach (var task in Model.CurentProject.DeletedTasks)
                ArchivePanel.Children.Add(CreateTask(task));
        }

        private Button CreateTask(Task task)
        {
            TaskBlock taskBlock = new TaskBlock(task, Model);
            Button button = new Button()
            {
                Background = new SolidColorBrush(Colors.Transparent),
                Margin = new Thickness(10, 20, 0, 0),
                BorderThickness = new Thickness(0),
                Content = taskBlock,
                Tag = task,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Style = this.FindResource("clearBtn") as Style
            };

            button.Click += openTaskClick;
            return button;
        }

        private void openTaskClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            OpenTask((Task)btn.Tag);
        }

        private void OpenTask(Task task)
        {
            SelectTask = task;
            LinearGradientBrush colors = new LinearGradientBrush();
            colors.StartPoint = new Point(0, 0);
            colors.EndPoint = new Point(1, 1);

            GradientStop gray = new GradientStop();
            gray.Color = Colors.LightGray;
            gray.Offset = 0.0;
            colors.GradientStops.Add(gray);

            GradientStop light = new GradientStop();
            light.Color = Color.FromArgb(255, 232, 232, 232);
            light.Offset = 0.0;
            colors.GradientStops.Add(light);

            gray.Offset = 0.9;
            colors.GradientStops.Add(gray);

            TaskImage.Fill = colors;

            if (task.ImagePath != null)
            {
                TaskImage.Fill = new ImageBrush(new BitmapImage(new System.Uri(task.ImagePath, System.UriKind.Absolute)));
                TaskDescr.Text = "No Description";
            }
            else
                TaskDescr.Text = task.Description;

            TaskTitle.Text = task.Name;
            TaskDeadline.Text = task.EndTime;

            MarkerStack.Children.Clear();
            foreach(var item in task.MarkersID)
                MarkerStack.Children.Add(new MarkerBlock(Model.CurentProject.Markers.Find(x=>x.UniqleID == item)));
        }

        private void Retrieve_Click(object sender, RoutedEventArgs e)
        {
            Controller.archiveController.Delete(SelectTask);
            Controller.taskController.Create(SelectTask);
            Update();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Controller.archiveController.Delete(SelectTask);
            Update();
        }
    }
}
