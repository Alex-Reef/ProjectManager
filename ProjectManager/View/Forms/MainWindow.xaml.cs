using System.Windows;
using System.Windows.Input;
using ProjectManager.Models;
using ProjectManager.Controllers;

namespace ProjectManager
{
    public partial class MainWindow : Window
    {
        private Model model { get; set; }
        private Controller presenter { get; set; }

        public MainWindow(Model Model, Controller Presenter, Project project)
        {
            InitializeComponent();

            model = Model;
            presenter = Presenter;

            model.SetProject(project);
            TaskPage taskPage = new TaskPage(model, presenter);

            frame.Content = taskPage;
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

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = presenter.OpenPage("Settings");
        }

        private void tasksBtn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = presenter.OpenPage("Tasks");
        }
    }
}