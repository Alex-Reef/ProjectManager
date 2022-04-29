using System.Windows;
using ProjectManager.Controllers;
using ProjectManager.Models;
using ProjectManager.Utilites;

namespace ProjectManager.View.Forms
{
    public partial class RegistrationForm : Window
    {
        private Model model { get; set; }
        private Controller controller { get; set; }

        public RegistrationForm(Model model, Controller controller)
        {
            InitializeComponent();
            this.model = model;
            this.controller = controller;
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = new User() { 
                Login = LoginBox.Text,
                UserName = NameBox.Text + " " + SurnameBox.Text,
                Email = EmailBox.Text,
                Password = PasswordBox.Password,
                UniqleID = KeyGenerator.Generate(10)
            };
            controller.userController.Create(user);
            LoginForm loginForm = new LoginForm(model, controller);
            loginForm.Show();
            this.Hide();
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginForm loginForm = new LoginForm(model, controller);
            loginForm.Show();
            this.Hide();
        }

        private void DialogHost_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {

        }
    }
}
