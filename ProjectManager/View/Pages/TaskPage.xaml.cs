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
        private List<TextBlock> Counters { get; set; }
        private CreateTaskForm createTaskForm;
        private Model model { get; set; }
        private Controller controller { get; set; }

        public TaskPage(Model Model, Controller controller)
        {
            InitializeComponent();

            model = Model;
            this.controller = controller;
            Tasks = model.GetTasks();
            Panels = new List<StackPanel>() { nextBlock, processBlock, competeBlock };
            Counters = new List<TextBlock>() { nextCount, procCount, complCount };
            for (int i = 0; i < 3; i++)
                LoadTask(Tasks[i], Panels[i], Counters[i]);
        }

        private int LoadTask(List<Task> taskList, StackPanel panel, TextBlock textBlock)
        {
            Button task;
            foreach (Task ts in taskList)
            {
                task = CreateTask(ts).Item1;
                panel.Children.Add(task);
            }
            textBlock.Text = taskList.Count.ToString();
            return 0;
        }

        private void addTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            if (model.GetMarkers().Count != 0)
            {
                createTaskForm = new CreateTaskForm(model);
                createTaskForm.ShowDialog();

                Task task = new Task()
                {
                    Name = createTaskForm.GetTaskName(),
                    Description = createTaskForm.GetDesc(),
                    UniqleID = createTaskForm.GetID(),
                    MarkersID = createTaskForm.GetMarkers(),
                    EndTime = DateTime.Now.ToString("d MMMM")
                };

                Button taskPanel = CreateTask(task).Item1;
                if (model.AddTask(task, 0) == 0)
                    nextBlock.Children.Add(taskPanel);

                MySnackbar.IsActive = true;
                MySnackbar.MouseDown += MySnackbar_MouseDown;
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
            CreateMarkerForm createMarkerForm = new CreateMarkerForm(model);
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
            for (int i = 0; i < 3; i++)
                Panels[i].Children.Clear();
            Tasks = model.GetTasks();
            for (int i = 0; i < 3; i++)
                LoadTask(Tasks[i], Panels[i], Counters[i]);
        }

        private void DeleteTask(Task task)
        {
            model.RemoveTask(task);
            Update();
        }

        private Tuple<Button, Task> CreateTask(Task ts)
        {
            Task task = new Task()
            {
                Name = ts.Name,
                Description = ts.Description,
                UniqleID = ts.UniqleID,
                MarkersID = ts.MarkersID,
                EndTime = ts.EndTime
            };

            if (ts.Description.Length > 70)
                ts.Description = ts.Description.Substring(0, 70) + "...";

            TaskBlock taskBlock = new TaskBlock(task, model);

            Button button = new Button()
            {
                Background = new SolidColorBrush(Colors.Transparent),
                Width = 260,
                Height = 150,
                Margin = new Thickness(10, 20, 10, 0),
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
