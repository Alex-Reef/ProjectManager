using System.Windows;
using ProjectManager.Controllers;
using ProjectManager.Models;

namespace ProjectManager
{
    public partial class CreateProjectForm : Window
    {
        private Model model;
        private Controller presenter;

        public CreateProjectForm()
        {
            InitializeComponent();

            model = new Model();
            presenter = new Controller(model);
        }

        private void createProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            presenter.CreateProject(prjNameBox.Text);
            SelectProjectForm mw = new SelectProjectForm();
            mw.Show();
            this.Close();
        }
    }
}