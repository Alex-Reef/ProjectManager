using System.Windows.Controls;

namespace ProjectManager.View.Controls
{
    public partial class HeaderColumn : UserControl
    {
        public string ColumnName { get; set; }
        public HeaderColumn(string Text, int count)
        {
            InitializeComponent();
            ColumnName = Text;
            Content.Text = Text;
            Count.Text = count.ToString();
        }

        public void SetCount(int count) => Count.Text = count.ToString();
    }
}
