using System.Windows.Controls;
using System.Windows.Media;
using ProjectManager.Models;

namespace ProjectManager.View.Controls
{
    public partial class MarkerBlock : UserControl
    {
        private string ID { get; set; }

        public MarkerBlock(Marker marker)
        {
            InitializeComponent();
            MarkerCard.Background = new SolidColorBrush(marker.Color);
            MarkerText.Content = marker.Text;
            ID = marker.UniqleID;
            Tag = marker;
        }

        public string GetID() => ID;
    }
}
