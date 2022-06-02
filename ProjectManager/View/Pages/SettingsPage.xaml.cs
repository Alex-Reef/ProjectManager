using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ProjectManager.Controllers;
using ProjectManager.Utilites;

namespace ProjectManager
{
    public partial class SettingsPage : Page
    {
        private Controller Controller { get; set; }
        private Model Model { get; set; }

        private string ImagePath { get; set; }

        public SettingsPage(Controller controller, Model model)
        {
            InitializeComponent();
            this.Controller = controller;
            this.Model = model;
            ThemeSelect.SelectedIndex = model.AppSettings.Theme;
            LangSelect.SelectedIndex = model.AppSettings.Language;

            var user = model.CurentUser;
            FirstNameBox.Text = user.UserName.Split(' ')[0];
            LastNameBox.Text = user.UserName.Split(' ')[1];
            LoginBox.Text = user.Login;
            EmailBox.Text = user.Email;
            ImagePath = user.ImagePath;
            SelectImageBtn.Background = new ImageBrush(new BitmapImage(new System.Uri(user.ImagePath, System.UriKind.Relative)));
        }

        private void ThemeSelect_Selected(object sender, RoutedEventArgs e)
        {
            Controller.settingsController.SetTheme(ThemeSelect.SelectedIndex);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            string error = null;
            if (LoginBox.Text.Length < 3 || FirstNameBox.Text.Length < 3 || LastNameBox.Text.Length < 3
                || EmailBox.Text.Length < 3 || ImagePath == null)
            {
                error = Application.Current.Resources["FillingFields"].ToString();
                valid = false;
            }
            if (!DataSecurityService.IsValidEmail(EmailBox.Text) && valid)
            {
                error = Application.Current.Resources["InvalidEmail"].ToString();
                valid = false;
            }
            if (!DataSecurityService.IsValidLogin(Model, LoginBox.Text) && valid)
            {
                error = Application.Current.Resources["LoginAlready"].ToString();
                valid = false;
            }

            if (valid)
            {
                var user = Model.CurentUser;
                user.UserName = FirstNameBox.Text + " " + LastNameBox.Text;
                user.ImagePath = ImagePath;
                user.Email = EmailBox.Text;
                user.Login = LoginBox.Text;
                Controller.userController.Update(user);
            }
            else
            {
                MessageBlock.Text = error;
                Dialog.IsOpen = true;
            }
        }

        private void ChangePassBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = Model.CurentUser;
            if(DataSecurityService.CreateMD5(OldPassBox.Text) == user.Password)
            {
                user.Password = DataSecurityService.CreateMD5(NewPassBox.Text);
                Controller.userController.Update(user);
            } 
            else
            {
                MessageBlock.Text = Application.Current.Resources["WrongPassword"].ToString();
                Dialog.IsOpen = true;
            }
        }

        private void LangSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Controller.settingsController.SetLanguage(LangSelect.SelectedIndex);
        }

        private void SelectImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (dialog.ShowDialog() == true)
            {
                ImagePath = dialog.FileName;
                SelectImageBtn.Background = new ImageBrush(new BitmapImage(new System.Uri(ImagePath)));
            }
        }
    }
}
