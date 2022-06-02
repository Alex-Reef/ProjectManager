using System.Windows.Controls;
using ProjectManager.Models;

namespace ProjectManager.View.Controls
{
    public partial class HeaderColumn : UserControl
    {
        public HeaderColumn(string title, int count)
        {
            InitializeComponent();
            Content.Text = title.ToUpper();
            Count.Text = count.ToString();
        }

        public void SetCount(int count) => Count.Text = count.ToString();
    }
}
