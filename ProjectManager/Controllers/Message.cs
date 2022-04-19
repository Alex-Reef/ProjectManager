using ProjectManager.View.Forms;
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

        public static void Show(string name, MessageIcon icon, MessageButton button)
        {
            MessageForm mf = new MessageForm(name, icon, button);
            mf.ShowDialog();
        }
    }
}
