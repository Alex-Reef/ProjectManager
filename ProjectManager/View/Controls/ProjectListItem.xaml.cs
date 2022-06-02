using System.Windows;
using System.Windows.Controls;
using ProjectManager.Models;
using ProjectManager.Controllers;
using System;

namespace ProjectManager.View.Controls
{
    public partial class ProjectListItem : UserControl
    {
        private Project _project { get; set; }
        private Controller _controller { get; set; }
        private Model _model { get; set; }
        private Window _window { get; set; }

        public ProjectListItem(Project project, Window window, Model model, Controller controller)
        {
            InitializeComponent();
            _project = project;
            _controller = controller;
            _window = window;
            _model = model;
            NameBlock.Text = project.Name;
            LastOpened.Text = project.LastOpened;
        }

        private void OpenPrj_Click(object sender, RoutedEventArgs e)
        {
            _project.LastOpened = DateTime.Now.ToString("d MMMM");
            MainWindow mw = new MainWindow(_model, _controller, _project);
            mw.Show();
            _window.Hide();
        }

        private void DeletePrj_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
