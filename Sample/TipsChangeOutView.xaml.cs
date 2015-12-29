using System.Windows.Controls;

namespace Prototyping
{
    public partial class TipsChangeOutView : UserControl
    {
        public TipsChangeOutView()
        {
            InitializeComponent();
        }

        private void DataForm_EditEnded(object sender, DataFormEditEndedEventArgs e)
        {
            var d = DataContext;
        }
    }
}