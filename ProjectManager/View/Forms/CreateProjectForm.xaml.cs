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
            if (prjNameBox.Text.Length > 0)
            {
                Project project = new Project()
                {
                    Name = prjNameBox.Text,
                    LastOpened = "-"
                };
                controller.projectController.Create(project);
                SelectProjectForm mw = new SelectProjectForm(controller, model);
                mw.Show();
                this.Close();
            }
            else
                Dialog.IsOpen = true;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectProjectForm mw = new SelectProjectForm(controller, model);
            mw.Show();
            this.Close();
        }
    }
}