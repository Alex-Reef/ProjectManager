using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using ProjectManager.Controllers;

namespace ProjectManager
{
    public partial class SettingsPage : Page
    {
        private Controller Controller { get; set; }
        public SettingsPage(Controller controller, Model model)
        {
            InitializeComponent();
            this.Controller = controller;
            ThemeSelect.SelectedIndex = model.GetAppSettings().Theme;
        }

        private void ThemeSelect_Selected(object sender, RoutedEventArgs e)
        {
            Controller.settingsController.SetTheme(ThemeSelect.SelectedIndex);
        }
    }
}
