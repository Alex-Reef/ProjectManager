using System.Windows;
using System.Windows.Media;
using ProjectManager.Controllers;

namespace ProjectManager.View.Controls
{
    public partial class InputDialog : Window
    {
        public InputDialog(string message)
        {
            InitializeComponent();

            MsgText.Text = message;
        }

        public string GetString() => TextLine.Text;

        private void FirstBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void SecondBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
