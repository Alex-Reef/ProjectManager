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
        public string taskName { get; private set; }
        public string descTask { get; private set; }
        public List<string> markersID { get; private set; }
        public string uID { get; private set; }
        public string Date { get; private set; }
        public string ImagePath { get; private set; }
        private bool SelectImage { get; set; }

        private Model model { get; set; }

        public CreateTaskForm(Model model)
        {
            InitializeComponent();
            this.model = model;
            markersID = new List<string>();
            SelectImage = false;

            var list = model.CurentProject.Markers;
            foreach (var item in list)
            {
                MarkerBox.Items.Add(item.Text);
            }
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
            markersID.RemoveAt(model.CurentProject.Markers.FindIndex(x=>x.UniqleID == marker.UniqleID));
            markerPanel.Children.RemoveAt(model.CurentProject.Markers.FindIndex(x => x.UniqleID == marker.UniqleID));
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