using System.Windows.Media;
using System.Windows;
using System.Text;
using System.Security.Cryptography;
using System;

namespace ProjectManager
{
    public partial class CreateTaskForm : Window
    {
        private string taskName { get; set; }
        private string descTask { get; set; }
        private string markerTask { get; set; }
        private string markerID { get; set; }
        private string uID { get; set; }
        private string Date { get; set; }
        private Model model { get; set; }

        public CreateTaskForm(Model model)
        {
            InitializeComponent();
            this.model = model;
            var list = model.GetMarkers();
            foreach (var item in list)
                markerBox.Items.Add(item.Text);
        }

        public string GetTaskName()
        {
            return taskName;
        }

        public string GetDesc()
        {
            return descTask;
        }

        public string GetMarkerText()
        {
            return markerTask;
        }

        public string GetMarkerColor()
        {
            return markerID;
        }

        public string GetID()
        {
            return uID;
        }

        public string GetDate()
        {
            return Date;
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
            markerTask = markerBox.SelectedItem.ToString();
            uID = RandomString(10);
            markerID = model.GetMarkers()[markerBox.SelectedIndex].UniqleID;
            this.Visibility = Visibility.Hidden;
        }
    }
}