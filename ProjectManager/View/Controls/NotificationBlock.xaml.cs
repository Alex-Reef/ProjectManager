using System.Windows.Controls;
using System.Windows.Media;
using ProjectManager.Controllers;

namespace ProjectManager.View.Controls
{
    public partial class NotificationBlock : UserControl
    {
        private Controller Controller { get; set; }
        private Model Model { get; set; }

        public NotificationBlock(Controller controller, Model model)
        {
            InitializeComponent();
            this.Controller = controller;
            this.Model = model;
            Update();
        }

        public void Update()
        {
            NotifyList.Children.Clear();
            EmptyList.Visibility = System.Windows.Visibility.Visible;
            Badge.BadgeForeground = new SolidColorBrush(Colors.Transparent);
            foreach (var item in Model.Notifications)
            {
                NotifyList.Children.Add(new NotificationItem(Controller,item, this));
                Badge.BadgeForeground = new SolidColorBrush(Colors.Tomato);
                EmptyList.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
