using System.Windows;
using System.Windows.Media;
using ProjectManager.Controllers;

namespace ProjectManager.View.Controls
{
    public partial class MessageForm : Window
    {
        public MessageForm(string message, Message.MessageIcon icon, Message.MessageButton button)
        {
            InitializeComponent();

            MsgText.Text = message;
            switch (icon)
            {
                case Message.MessageIcon.Success:
                    MsgIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.CheckCircle;
                    MsgIcon.Foreground = new SolidColorBrush(Colors.Green);
                    break;
                case Message.MessageIcon.Error:
                    MsgIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Error;
                    MsgIcon.Foreground = new SolidColorBrush(Colors.Tomato);
                    break;
                case Message.MessageIcon.Info:
                    MsgIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.InfoCircle;
                    MsgIcon.Foreground = new SolidColorBrush(Colors.DodgerBlue);
                    break;
                case Message.MessageIcon.Question:
                    MsgIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.QuestionAnswer;
                    MsgIcon.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
            }

            FirstBtn.Visibility = Visibility.Visible;
            SecondBtn.Visibility = Visibility.Visible;

            switch (button)
            {
                case Message.MessageButton.OK:
                    FirstBtn.Content = "OK";
                    SecondBtn.Visibility = Visibility.Hidden;
                    break;
                case Message.MessageButton.OkCancel:
                    FirstBtn.Content = "OK";
                    SecondBtn.Content = "Cancel";
                    break;
                case Message.MessageButton.YesNo:
                    FirstBtn.Content = "Yes";
                    SecondBtn.Content = "No";
                    break;
            }
        }

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
