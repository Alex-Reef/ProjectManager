using ProjectManager.View.Controls;
namespace ProjectManager.Controllers
{
    public static class Message
    {
        public enum MessageIcon{
            Success,
            Error,
            Info,
            Question
        }

        public enum MessageButton {
            OK,
            OkCancel,
            YesNo
        }

        public static void Show(string Title, MessageIcon icon, MessageButton button)
        {
            MessageForm mf = new MessageForm(Title, icon, button);
            mf.ShowDialog();
        }
    }
}
