using System.Windows;
using ProjectManager.Controllers;
using ProjectManager.Models;

namespace ProjectManager.View.Forms
{
    public partial class LoginForm : Window
    {
        private Model model { get; set; }
        private Controller controller { get; set; }

        public LoginForm()
        {
            InitializeComponent();
            model = new Model();
            controller = new Controller(model);
            controller.settingsController.SetTheme(model.GetAppSettings().Theme);
            controller.settingsController.SetLanguage(model.GetAppSettings().Language);
        }

        public LoginForm(Model model, Controller controller)
        {
            InitializeComponent();
            this.model = model;
            this.controller = controller;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectProjectForm spf;
            User user = controller.userController.Authorization(LoginBox.Text, PasswordBox.Password);
            if (user != null)
            {
                spf = new SelectProjectForm(controller, model);
                spf.Show();
                this.Hide();
            }
            else
                Dialog.IsOpen = true;
        }

        private void CreateAccBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm regForm = new RegistrationForm(model, controller);
            regForm.Show();
            this.Hide();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
