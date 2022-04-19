using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using ProjectManager.Controllers;
using ProjectManager.Models;

namespace ProjectManager
{
    public partial class TaskForm : Window
    {
        private Task task { get; set; }
        private Model model { get; set; }
        private Controller controller { get; set; }
        public int CategoryID { get; set; }
        public int PositionID { get; set; }

        public TaskForm(Task ts, Model md, Controller contr)
        {
            InitializeComponent();
            model = md;
            controller = contr;
            task = model.GetTaskByKey(ts.UniqleID);
            TaskNameBox.Text = task.Name;
            DescriptionBox.Text = task.Description;
            PositionID = -1;
            CategoryID = -1;

            var list = model.GetMarkers();
            foreach (var item in list)
                markerBox.Items.Add(item.Text);

            categoryBox.Items.Add("Next Up");
            categoryBox.Items.Add("In Process");
            categoryBox.Items.Add("Complete");

            markerBox.SelectedIndex = model.GetMarkers().FindIndex(x => x.UniqleID == task.MarkerID);
            categoryBox.SelectedIndex = controller.GetTaskPosition(model, task).Item2;
            DataPicker.Text = task.EndTime;
            MarkerCard.Background = new SolidColorBrush(model.GetMarkers()[markerBox.SelectedIndex].Color);
            MarkerCard.Width = MarkerText.Content.ToString().Length + 60;
            MarkerText.Content = markerBox.Items[markerBox.SelectedIndex].ToString();
            CtgNameLabel.Content = categoryBox.SelectedItem.ToString();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            model.RemoveTask(task);
            this.Visibility = Visibility.Hidden;
        }

        private void SaveTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            task.Name = TaskNameBox.Text;
            task.Description = DescriptionBox.Text;
            task.EndTime = DataPicker.Text.ToString();
            model.UpdateTask(categoryBox.SelectedIndex, task, markerBox.SelectedIndex);
            this.Visibility = Visibility.Hidden;
        }

        private void MarkerCard_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void CtgNameLabel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void markerBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            MarkerCard.Background = new SolidColorBrush(model.GetMarkers()[markerBox.SelectedIndex].Color);
            MarkerText.Content = markerBox.Items[markerBox.SelectedIndex].ToString();
            MarkerCard.Width = MarkerText.Content.ToString().Length + 60;
        }
    }
}