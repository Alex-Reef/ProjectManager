using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using ProjectManager.Controllers;

namespace ProjectManager
{
    public partial class SettingsPage : Page
    {
        private Controller Controller { get; set; }
        public SettingsPage(Controller controller)
        {
            InitializeComponent();
            this.Controller = controller;
        }

        private void LangSettings_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ThemeSettings_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ThemeSelect_Checked(object sender, RoutedEventArgs e)
        {
            Controller.SetTheme();
        }
    }
}
