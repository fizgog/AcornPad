namespace AcornPad.Controls
{
    public partial class StatusStrip : System.Windows.Forms.StatusStrip
    {
        public StatusStrip()
        {
            InitializeComponent();
            InProgress(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void InProgress(bool value)
        {
            if (value)
            {
                StatusLabel1.Text = "Please Wait";
                StatusProgressBar1.Visible = true;
            }
            else
            {
                StatusLabel1.Text = "Ready";
                StatusProgressBar1.Visible = false;
            }
        }
    }
}