using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ProjectManager.Controllers;
using ProjectManager.Models;
using ProjectManager.Utilites;

namespace ProjectManager.View.Forms
{
    public partial class RegistrationForm : Window
    {
        private Model model { get; set; }
        private Controller controller { get; set; }
        private string UserImagePath { get; set; }

        public RegistrationForm(Model model, Controller controller)
        {
            InitializeComponent();
            this.model = model;
            this.controller = controller;
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            string error = null;
            if (LoginBox.Text.Length < 3 || NameBox.Text.Length < 3 || SurnameBox.Text.Length < 3
                || EmailBox.Text.Length < 3 || PasswordBox.Password.Length < 5 || UserImagePath == null)
            {
                error = Application.Current.Resources["FillingFields"].ToString();
                valid = false;
            }
            if(!DataSecurityService.IsValidEmail(EmailBox.Text) && valid)
            {
                error = Application.Current.Resources["InvalidEmail"].ToString();
                valid = false;
            }
            if (!DataSecurityService.IsValidLogin(model, LoginBox.Text) && valid)
            {
                error = Application.Current.Resources["LoginAlready"].ToString();
                valid = false;
            }

            if(valid)
            {
                User user = new User()
                {
                    Login = LoginBox.Text,
                    UserName = NameBox.Text + " " + SurnameBox.Text,
                    Email = EmailBox.Text,
                    Password = DataSecurityService.CreateMD5(PasswordBox.Password),
                    UniqleID = KeyGenerator.Generate(10),
                    ImagePath = UserImagePath
                };
                controller.userController.Create(user);
                LoginForm loginForm = new LoginForm(model, controller);
                loginForm.Show();
                this.Hide();
            }
            else
            {
                ErrorBox.Text = error;
                Dialog.IsOpen = true;
            }
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginForm loginForm = new LoginForm(model, controller);
            loginForm.Show();
            this.Hide();
        }

        private void UserImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (dialog.ShowDialog() == true)
            {
                UserImagePath = dialog.FileName;
                UserImage.Background = new ImageBrush(new BitmapImage(new System.Uri(UserImagePath)));
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
