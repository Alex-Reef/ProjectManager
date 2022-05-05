namespace ProjectManager.Models
{
    public class Notification
    {
        public string Title { get; private set; }
        public string Message { get; private set; }
        public string UniqleID { get; private set; }

        public Notification(string Title, string Message, string UniqleID)
        {
            this.Message = Message;
            this.UniqleID = UniqleID;
            this.Title = Title;
        }
    }
}
