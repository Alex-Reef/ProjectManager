using System.Windows.Controls;

namespace ProjectManager.View.Controls
{
    public partial class ColumnHeader : UserControl
    {
        public ColumnHeader(string Header, int count)
        {
            InitializeComponent();
            HeaderName.Text = Header.ToUpper();
            Counter.Text = count.ToString();
        }

        public void SetCount(int count)
        {
            Counter.Text = count.ToString();
        }
    }
}
