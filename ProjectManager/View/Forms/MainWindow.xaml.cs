using System.Windows;
using System.Windows.Input;
using ProjectManager.Models;
using ProjectManager.Controllers;

namespace ProjectManager
{
    public partial class MainWindow : Window
    {
        private Model model { get; set; }
        private Controller controller { get; set; }

        public MainWindow(Model Model, Controller Presenter, Project project)
        {
            InitializeComponent();

            model = Model;
            controller = Presenter;
            model.SetProject(project);
            model.CurentProject.LastOpened = project.LastOpened;

            Menu.SelectedIndex = 0;
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

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void dashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = controller.OpenPage("Dashboard");
        }

        private void archiveBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = controller.OpenPage("Archive");
        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = controller.OpenPage("Settings");
        }

        private void tasksBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = controller.OpenPage("Tasks");
        }
    }
}