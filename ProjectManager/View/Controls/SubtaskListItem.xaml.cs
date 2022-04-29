using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectManager.Models;

namespace ProjectManager.View.Controls
{
    public partial class SubtaskListItem : UserControl
    {
        public SubtaskListItem(Subtask subtask)
        {
            InitializeComponent();
            SubtaskBlock.Text = subtask.Name;
            if (subtask.Complete)
                CheckBtn.Foreground = (SolidColorBrush)FindResource("Primary");
            else
                CheckBtn.Foreground = new SolidColorBrush(Colors.LightGray);
        }

        private void SubtaskGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }
    }
}
