﻿using System.Collections.Generic;
using System.Windows;
using ProjectManager.Controllers;
using ProjectManager.Models;
using ProjectManager.View.Controls;

namespace ProjectManager
{
    public partial class TaskForm : Window
    {
        private Task task { get; set; }
        private Model model { get; set; }
        private Controller controller { get; set; }
        private List<string> markersID { get; set; }
        public int CategoryID { get; set; }
        public int PositionID { get; set; }

        public TaskForm(Task ts, Model md, Controller contr)
        {
            InitializeComponent();
            model = md;
            controller = contr;
            task = model.GetTaskByKey(ts.UniqleID);
            markersID = new List<string>();
            TaskNameBox.Text = task.Name;
            DescriptionBox.Text = task.Description;
            PositionID = -1;
            CategoryID = -1;

            var list = model.GetMarkers();
            foreach (var item in list)
            {
                MarkerBox.Items.Add(item.Text);
            }

            var lst = ts.MarkersID;
            foreach (var item in lst)
            {
                markersID.Add(item);
                MarkerBlock markerBlock = new MarkerBlock(model.GetMarkers().Find(x => x.UniqleID == item));
                markerBlock.MouseDown += MarkerBlock_MouseDown;
                markerPanel.Children.Add(markerBlock);
            }

            categoryBox.Items.Add("Next Up");
            categoryBox.Items.Add("In Process");
            categoryBox.Items.Add("Complete");

            categoryBox.SelectedIndex = controller.GetTaskPosition(model, task).Item2;
            DataPicker.Text = task.EndTime;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            model.RemoveTask(task);
            this.Visibility = Visibility.Hidden;
        }

        private void SaveTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            task.Name = TaskNameBox.Text;
            task.Description = DescriptionBox.Text;
            task.EndTime = DataPicker.Text.ToString();
            model.UpdateTask(categoryBox.SelectedIndex, task, markersID);
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
            markersID.Add(model.GetMarkers()[ID].UniqleID);
            MarkerBlock markerBlock = new MarkerBlock(model.GetMarkers()[ID]);
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
            markersID.RemoveAt(model.GetMarkers().FindIndex(x => x.UniqleID == marker.UniqleID));
            markerPanel.Children.RemoveAt(model.GetMarkers().FindIndex(x => x.UniqleID == marker.UniqleID));
        }
    }
}