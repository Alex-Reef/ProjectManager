using System.Windows.Media;
using System.Windows;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System;
using ProjectManager.View.Controls;
using ProjectManager.Models;

namespace ProjectManager
{
    public partial class CreateTaskForm : Window
    {
        private string taskName { get; set; }
        private string descTask { get; set; }
        private List<string> markersID { get; set; }
        private string uID { get; set; }
        private string Date { get; set; }
        private Model model { get; set; }

        public CreateTaskForm(Model model)
        {
            InitializeComponent();
            this.model = model;
            markersID = new List<string>();
            var list = model.GetCurentProject().Markers;
            foreach (var item in list)
            {
                MarkerBox.Items.Add(item.Text);
            }
        }

        public string GetTaskName()
        {
            return taskName;
        }

        public string GetDesc()
        {
            return descTask;
        }

        public string GetID()
        {
            return uID;
        }

        public string GetDate()
        {
            return Date;
        }

        public List<string> GetMarkers()
        {
            return markersID;
        }

        public void AddMarker(int ID)
        {
            markersID.Add(model.GetCurentProject().Markers[ID].UniqleID);
            MarkerBlock markerBlock = new MarkerBlock(model.GetCurentProject().Markers[ID]);
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
            markersID.RemoveAt(model.GetCurentProject().Markers.FindIndex(x=>x.UniqleID == marker.UniqleID));
            markerPanel.Children.RemoveAt(model.GetCurentProject().Markers.FindIndex(x => x.UniqleID == marker.UniqleID));
        }

        private string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        private void addTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            taskName = taskNameBox.Text;
            descTask = descTaskBox.Text;
            uID = RandomString(10);
            Date = DatePicker.SelectedDate.Value.ToString("d MMMM");
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
    }
}