using System.Windows;
using System.Windows.Controls;
using ProjectManager.Controllers;

namespace ProjectManager
{
    public partial class SettingsPage : Page
    {
        private Controller Controller { get; set; }
        private Model Model { get; set; }
        public SettingsPage(Controller controller, Model model)
        {
            InitializeComponent();
            this.Controller = controller;
            this.Model = model;
            ThemeSelect.SelectedIndex = model.GetAppSettings().Theme;

            var user = model.GetCurentUser();
            FirstNameBox.Text = user.UserName.Split(' ')[0];
            LastNameBox.Text = user.UserName.Split(' ')[1];
            LoginBox.Text = user.Login;
            EmailBox.Text = user.Email;
        }

        private void ThemeSelect_Selected(object sender, RoutedEventArgs e)
        {
            Controller.settingsController.SetTheme(ThemeSelect.SelectedIndex);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = Model.GetCurentUser();
            user.UserName = FirstNameBox.Text + " " + LastNameBox.Text;
            user.Email = EmailBox.Text;
            user.Login = LoginBox.Text;
            Controller.userController.Update(user);
        }

        private void ChangePassBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = Model.GetCurentUser();
            if(OldPassBox.Text == user.Password)
            {
                user.Password = NewPassBox.Text;
                Controller.userController.Update(user);
            }    
        }
    }
}
