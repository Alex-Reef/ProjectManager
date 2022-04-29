using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using ProjectManager.Controllers;
using ProjectManager.Models;
using ProjectManager.View.Controls;

namespace ProjectManager
{
    public partial class TaskPage : Page
    {
        private List<List<Task>> Tasks { get; set; }
        private List<StackPanel> Panels { get; set; }
        private List<HeaderColumn> Headers { get; set; }
        private CreateTaskForm createTaskForm;
        private Model model { get; set; }
        private Controller controller { get; set; }

        public TaskPage(Model Model, Controller controller)
        {
            InitializeComponent();

            model = Model;
            this.controller = controller;

            Tasks = model.GetCurentProject().Tasks;
            Panels = new List<StackPanel>();
            Headers = new List<HeaderColumn>();

            UserChip.Content = model.GetCurentUser().UserName;
            ProjectNameLabel.Content = model.GetCurentProject().Name;
            ltsUserName.Content = model.GetCurentUser().UserName.Split(' ')[0];

            var list = model.GetCurentProject().Headers;
            for (int i = 0; i < list.Count; i++)
            {
                Panels.Add(new StackPanel() { Width = 280, Margin = new Thickness(0, 0, 40, 0) });
                Headers.Add(new HeaderColumn(list[i], 0));
                ColumnPanel.Children.Add(Panels[i]);
                HeaderPanel.Children.Add(Headers[i]);
            }

            Update();
        }

        private int LoadTask(List<Task> taskList, StackPanel panel, HeaderColumn headers)
        {
            Button task;
            foreach (Task ts in taskList)
            {
                task = CreateTask(ts).Item1;
                panel.Children.Add(task);
            }
            headers.SetCount(taskList.Count);
            return 0;
        }

        private void addTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            if (model.GetCurentProject().Markers.Count != 0)
            {
                createTaskForm = new CreateTaskForm(model);
                createTaskForm.ShowDialog();

                Task task = new Task()
                {
                    Name = createTaskForm.GetTaskName(),
                    Description = createTaskForm.GetDesc(),
                    UniqleID = createTaskForm.GetID(),
                    MarkersID = createTaskForm.GetMarkers(),
                    EndTime = createTaskForm.GetDate()
                };

                Button taskPanel = CreateTask(task).Item1;
                controller.taskController.Create(task);
                Panels[0].Children.Add(taskPanel);

                MySnackbar.IsActive = true;
                MySnackbar.MouseDown += MySnackbar_MouseDown;
                Update();
            }
            else
            {
                Message.Show("Спочатку створіть маркер!", Message.MessageIcon.Info, Message.MessageButton.OK);
                OpenMarkerSettings();
            }
        }

        private void MySnackbar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MySnackbar.IsActive = false;
        }

        private void deleteTaskClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DeleteTask((Task)btn.Tag);
        }

        private void OpenMarkerSettings()
        {
            CreateMarkerForm createMarkerForm = new CreateMarkerForm(model, controller);
            createMarkerForm.ShowDialog();
            if (createMarkerForm.DialogResult == true)
                Update();
        }
        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenMarkerSettings();
        }

        private void openTaskClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            OpenTask((Task)btn.Tag);
        }

        private void OpenTask(Task task)
        {
            TaskForm taskEdit = new TaskForm(task, model, controller);
            taskEdit.ShowDialog();
            Update();
        }

        private void Update()
        {
            var list = model.GetCurentProject().Headers;
            for (int i = 0; i < list.Count; i++)
                Panels[i].Children.Clear();

            Tasks = model.GetCurentProject().Tasks;

            for (int i = 0; i < list.Count; i++)
                LoadTask(Tasks[i], Panels[i], Headers[i]);

            int count = 0;
            for(int i = 0; i < Tasks.Count; i++)
            {
                for(int j = 0; j < Tasks[i].Count; j++)
                    count++;
            }
            CountTaskLabel.Content = count.ToString();
        }

        private void DeleteTask(Task task)
        {
            controller.taskController.Delete(task);
            Update();
        }

        private Tuple<Button, Task> CreateTask(Task ts)
        {
            Task task = ts;

            if (ts.Description.Length > 70)
                ts.Description = ts.Description.Substring(0, 70) + "...";

            TaskBlock taskBlock = new TaskBlock(task, model);

            Button button = new Button()
            {
                Background = new SolidColorBrush(Colors.Transparent),
                Width = 280,
                Height = 150,
                Margin = new Thickness(0, 20, 0, 0),
                BorderThickness = new Thickness(0),
                Content = taskBlock,
                Tag = task,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Style = this.FindResource("clearBtn") as Style
            };
           
            button.Click += openTaskClick;

            Tuple<Button, Task> tuple = new Tuple<Button, Task>(button, task);
            return tuple;
        }
    }
}
