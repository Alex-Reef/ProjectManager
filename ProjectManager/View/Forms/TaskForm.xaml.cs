using System.Collections.Generic;
using System.Windows;
using ProjectManager.Controllers;
using ProjectManager.Models;
using ProjectManager.View.Controls;
using ProjectManager.Utilites;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System;
using System.IO;

namespace ProjectManager
{
    public partial class TaskForm : Window
    {
        private Task task { get; set; }
        private Model model { get; set; }
        private Controller controller { get; set; }
        private List<string> markersID { get; set; }
        public string ImagePath { get; set; }

        public TaskForm(Task ts, Model md, Controller contr)
        {
            InitializeComponent();
            model = md;
            controller = contr;

            foreach(var col in model.CurentProject.Tasks)
            {
                task = col.Find(x => x.UniqleID == ts.UniqleID);
                if (task != null)
                    break;
            }

            markersID = new List<string>();

            TaskNameBox.Text = task.Name;
            DescriptionBox.Text = task.Description;

            if(File.Exists(task.ImagePath))
                ImageBox.Source = new BitmapImage(new Uri(task.ImagePath));

            var list = model.CurentProject.Markers;
            foreach (var item in list)
            {
                MarkerBox.Items.Add(item.Text);
            }

            var lst = ts.MarkersID;
            foreach (var item in lst)
            {
                markersID.Add(item);
                MarkerBlock markerBlock = new MarkerBlock(model.CurentProject.Markers.Find(x => x.UniqleID == item));
                markerBlock.MouseDown += MarkerBlock_MouseDown;
                markerPanel.Children.Add(markerBlock);
            }

            if (task.Subtasks.Count > 0)
                EmptyPanel.Visibility = Visibility.Collapsed;
            else
                EmptyPanel.Visibility = Visibility.Visible;

            foreach (var item in task.Subtasks)
                SubtaskPanel.Children.Add(new SubtaskListItem(item, task, controller, this));

            foreach(var item in model.CurentProject.Headers)
                categoryBox.Items.Add(item);

            categoryBox.SelectedIndex = controller.GetTaskPosition(task);
            DataPicker.Text = task.EndTime;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            controller.taskController.Delete(task);
            this.Visibility = Visibility.Hidden;
        }

        private void SaveTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            task.Name = TaskNameBox.Text;
            task.Description = DescriptionBox.Text;
            task.EndTime = DataPicker.Text.ToString();
            task.MarkersID = markersID;
            task.ImagePath = ImagePath;
            controller.taskController.Move(task, categoryBox.SelectedIndex);
            controller.taskController.Update(task);
            this.Visibility = Visibility.Hidden;
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            Popup.IsOpen = false;
        }

        private void OpenPopup_Click(object sender, RoutedEventArgs e)
        {
            Popup.IsOpen = true;
        }

        private void MarkerBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AddMarker(MarkerBox.SelectedIndex);
        }

        public void AddMarker(int ID)
        {
            markersID.Add(model.CurentProject.Markers[ID].UniqleID);
            MarkerBlock markerBlock = new MarkerBlock(model.CurentProject.Markers[ID]);
            markerBlock.MouseDown += MarkerBlock_MouseDown;
            markerPanel.Children.Add(markerBlock);
        }

        private void MarkerBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MarkerBlock btn = sender as MarkerBlock;
            RemoveMarker((Marker)btn.Tag);
        }

        public void RemoveMarker(Marker marker)
        {
            markersID.RemoveAt(model.CurentProject.Markers.FindIndex(x => x.UniqleID == marker.UniqleID));
            markerPanel.Children.RemoveAt(model.CurentProject.Markers.FindIndex(x => x.UniqleID == marker.UniqleID));
        }

        private void AddSubtask_Click(object sender, RoutedEventArgs e)
        {
            InputDialog inputDialog = new InputDialog("");
            if (inputDialog.DialogResult == true)
            {
                controller.taskController.Subtask_Create(new Subtask
                { 
                    Name = inputDialog.GetString(), 
                    Complete = false, 
                    UniqleID = KeyGenerator.Generate(10) 
                }, task);
            }
            Update();
        }

        public void Update()
        {
            SubtaskPanel.Children.Clear();
            foreach (var item in task.Subtasks)
                SubtaskPanel.Children.Add(new SubtaskListItem(item, task, controller, this));

            if(task.Subtasks.Count > 0)
                EmptyPanel.Visibility = Visibility.Collapsed;
            else
                EmptyPanel.Visibility = Visibility.Visible;

            ImageBox.Source = null;
            if (File.Exists(ImagePath))
                ImageBox.Source = new BitmapImage(new Uri(ImagePath));
        }

        private void SelectImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (dialog.ShowDialog() == true)
            {
                ImagePath = dialog.FileName;
            }

            Update();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void ClearImage_Click(object sender, RoutedEventArgs e)
        {
            ImagePath = null;
            Update();
        }
    }
}