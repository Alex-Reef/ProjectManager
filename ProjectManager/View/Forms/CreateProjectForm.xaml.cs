using System.Windows;
using ProjectManager.Controllers;
using ProjectManager.Models;

namespace ProjectManager
{
    public partial class CreateProjectForm : Window
    {
        private Controller controller { get; set; }
        private Model model { get; set; }

        public CreateProjectForm(Controller _controller, Model _model)
        {
            InitializeComponent();
            controller = _controller;
            model = _model;
        }

        private void createProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            Project project = new Project() { Name = prjNameBox.Text };
            controller.projectController.Create(project);
            SelectProjectForm mw = new SelectProjectForm(controller, model);
            mw.Show();
            this.Close();
        }
    }
}