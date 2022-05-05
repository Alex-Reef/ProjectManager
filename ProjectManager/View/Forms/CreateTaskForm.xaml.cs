using System.Windows;
using System.Collections.Generic;
using ProjectManager.View.Controls;
using ProjectManager.Models;
using ProjectManager.Utilites;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace ProjectManager
{
    public partial class CreateTaskForm : Window
    {
        private string taskName { get; set; }
        private string descTask { get; set; }
        private List<string> markersID { get; set; }
        private string uID { get; set; }
        private string Date { get; set; }
        private string ImagePath { get; set; }
        private bool SelectImage { get; set; }

        private Model model { get; set; }

        public CreateTaskForm(Model model)
        {
            InitializeComponent();
            this.model = model;
            markersID = new List<string>();
            SelectImage = false;

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

        private void addTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            taskName = taskNameBox.Text;
            descTask = descTaskBox.Text;
            uID = KeyGenerator.Generate(10);
            Date = Calendar.SelectedDate.Value.ToString("d MMMM");
            DialogResult = true;
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

        private void SelectImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (dialog.ShowDialog() == true)
            {
                ImagePath = dialog.FileName;
                ImageBox.Source = new BitmapImage(new System.Uri(ImagePath));
            }
        }

        private void SelectTextBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectImage = false;
            ImageBlock.Visibility = Visibility.Collapsed;
            TextBlock.Visibility = Visibility.Visible;
        }

        private void ClearImage_Click(object sender, RoutedEventArgs e)
        {
            ImagePath = null;
            ImageBox.Source = null;
        }

        private void OpenImageBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectImage = true;
            ImageBlock.Visibility = Visibility.Visible;
            TextBlock.Visibility = Visibility.Collapsed;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Visibility = Visibility.Hidden;
        }

        private void OK_SelectDate_Click(object sender, RoutedEventArgs e)
        {
            DateBlock.Text = Calendar.SelectedDate.Value.ToString("d MMMM");
        }
    }
}