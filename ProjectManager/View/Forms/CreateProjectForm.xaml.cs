using System.Windows;
using ProjectManager.Controllers;
using ProjectManager.Models;

namespace ProjectManager
{
    public partial class CreateProjectForm : Window
    {
        private Controller controller;

        public CreateProjectForm(Controller _controller)
        {
            InitializeComponent();
            controller = _controller;
        }

        private void createProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            Project project = new Project() { Name = prjNameBox.Text };
            controller.projectController.Create(project);
            SelectProjectForm mw = new SelectProjectForm();
            mw.Show();
            this.Close();
        }
    }
}