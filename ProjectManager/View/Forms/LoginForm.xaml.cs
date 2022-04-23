using System.Windows;
using System.Windows.Input;
using ProjectManager.Controllers;

namespace ProjectManager.View.Forms
{
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void ThemeSelector_Click(object sender, RoutedEventArgs e)
        {
            Controller controller = new Controller();
            controller.SetTheme(ThemeSelector.IsChecked.Value);
        }


        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Maximized_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void Minimized_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            Model model = new Model();
            Controller controller = new Controller(model);
            //controller.UserController.CreateUser(LoginBox.Text, PasswordBox.Password, "TestEmail");
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            /*Model model = new Model();
            Controller controller = new Controller(model);

            if (controller.UserController.Authentication(LoginBox.Text, PasswordBox.Password) != null)
            {
                model.SetUser(controller.UserController.Authentication(LoginBox.Text, PasswordBox.Password));

                SelectProjectForm selectProjectForm = new SelectProjectForm(model, controller);
                selectProjectForm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("error", "info", MessageBoxButton.OK);*/
        }
    }
}
