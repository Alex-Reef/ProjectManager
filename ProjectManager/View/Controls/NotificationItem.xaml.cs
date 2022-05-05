using System.Windows;
using System.Windows.Controls;
using ProjectManager.Controllers;
using ProjectManager.Models;

namespace ProjectManager.View.Controls
{
    public partial class NotificationItem : UserControl
    {
        private Controller Controller { get; set; }
        private Notification Notification { get; set; }
        private NotificationBlock NotificationBlock { get; set; }

        public NotificationItem(Controller controller, Notification notification, NotificationBlock notificationBlock)
        {
            InitializeComponent();
            this.Controller = controller;
            this.Notification = notification;
            this.NotificationBlock = notificationBlock;

            NotifyTitle.Text = notification.Title;
            NotifyDescr.Text = notification.Message;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            Controller.notificationsController.Delete(Notification);
            NotificationBlock.Update();
        }
    }
}
