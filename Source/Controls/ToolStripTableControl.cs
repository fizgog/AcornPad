namespace WindowsFormsApp1.Controls
{
    public class ToolStripTableControl : TableControl
    {
        private ToolStripTable ownerItem;

        public ToolStripTableControl()
        {
        }

        public ToolStripTable Owner
        {
            get { return ownerItem; }
            set { ownerItem = value; }
        }
    }
}