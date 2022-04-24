using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ProjectManager.Models;
using ProjectManager.Controllers;

namespace ProjectManager
{
    public partial class CreateMarkerForm : Window
    {
        private Model model;
        private Controller controller { get; set; }
        private Marker marker { get; set; }
        public CreateMarkerForm(Model model, Controller controller)
        {
            InitializeComponent();
            this.model = model;
            this.controller = controller;
            if(model.GetCurentProject().Markers.Count != 0)
                marker = model.GetCurentProject().Markers[0];
            else
                NewMarker();
            marker = model.GetCurentProject().Markers[0];
            SetMarker(marker);
            LoadMarkers();
        }

        private void SetMarker(Marker mark)
        {
            marker = mark;
            MarkLabel.Content = mark.Text;
            MarkerName.Text = mark.Text;
            HexBox.Text = "#" + mark.Color.R.ToString("X2") + mark.Color.G.ToString("X2") + mark.Color.B.ToString("X2");
            ColorPicker.Color = mark.Color;
        }

        private void LoadMarkers()
        {
            markerBox.Children.Clear();
            var list = model.GetCurentProject().Markers;
            foreach (var item in list)
            {
                MaterialDesignThemes.Wpf.Card card = new MaterialDesignThemes.Wpf.Card()
                {
                    Margin = new Thickness(5, 10, 5, 5),
                    Background = new SolidColorBrush(item.Color),
                    Tag = item
                };

                Label marker = new Label()
                {
                    Content = item.Text + " (#" + item.Color.R.ToString("X2") + item.Color.G.ToString("X2") + item.Color.B.ToString("X2") + ")",
                    Foreground = new SolidColorBrush(Colors.White),
                };

                card.Content = marker;
                card.MouseDown += Card_MouseDown;
                markerBox.Children.Add(card);
            }
            SetMarker(list[0]);
        }

        private void Card_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MaterialDesignThemes.Wpf.Card btn = sender as MaterialDesignThemes.Wpf.Card;
            SetMarker((Marker)btn.Tag);
        }

        private void AddMarkerBtn_Click(object sender, RoutedEventArgs e)
        {
            NewMarker();
        }

        private void NewMarker()
        {
            Marker marker = new Marker()
            {
                Color = Colors.DodgerBlue,
                Text = "New Marker",
                UniqleID = RandomString(10)
            };
            controller.markerController.Create(marker);
            LoadMarkers();
        }

        // Generator of Random String for marker ID
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

        private void ColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            HexBox.Text = "#" + ColorPicker.Color.R.ToString("X2") + ColorPicker.Color.G.ToString("X2") + ColorPicker.Color.B.ToString("X2");
        }

        private void HexBox_TextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            ColorPicker.Color = (Color)ColorConverter.ConvertFromString(HexBox.Text);
        }

        private void SaveMarkerBtn_Click(object sender, RoutedEventArgs e)
        {
            marker.Color = ColorPicker.Color;
            marker.Text = MarkerName.Text;
            controller.markerController.Update(marker);
            LoadMarkers();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void DeveleMarkerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (model.GetCurentProject().Markers.Count > 1)
            {
                controller.markerController.Delete(marker);
                LoadMarkers();
            }
        }
    }
}