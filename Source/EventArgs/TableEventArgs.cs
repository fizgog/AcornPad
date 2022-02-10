using System.Drawing;

namespace WindowsFormsApp1.EventArgs
{
    public class TableEventArgs : System.EventArgs
    {
        public Size SelectedSize { get; set; }

        public TableEventArgs(Size size)
        {
            SelectedSize = size;
        }
    }
}