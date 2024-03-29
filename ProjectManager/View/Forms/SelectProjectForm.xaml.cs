﻿using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using ProjectManager.Controllers;
using ProjectManager.View.Controls;
using ProjectManager.Models;

namespace ProjectManager
{
    public partial class SelectProjectForm : Window
    {
        private Model model;
        private Controller controller;
        private List<Project> projectList;

        public SelectProjectForm(Controller controller, Model model)
        {
            InitializeComponent();
            this.model = model;
            this.controller = controller;

            projectList = new List<Project>();
            projectList = model.Projects;

            LoadListProject(projectList);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        public int LoadListProject(List<Project> projects)
        {
            foreach (var item in projectList)
            {
                ProjectListItem project = new ProjectListItem(item, this, model, controller);
                project.Margin = new Thickness(20);
                project.Height = 50;
                prjList.Children.Add(project);
            }

            return 0;
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

        private void NewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectForm create = new CreateProjectForm(controller, model);
            create.Show();
            this.Hide();
        }
    }
}