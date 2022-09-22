
namespace AcornPad.Controls
{
    partial class StatusStrip
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StatusLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.StatusProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.SuspendLayout();
            //
            // StatusLabel1
            //
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.StatusLabel1.Text = "Ready";
            //
            // StatusProgressBar1
            //
            this.StatusProgressBar1.Name = "StatusProgressBar1";
            this.StatusProgressBar1.Size = new System.Drawing.Size(100,16);
            this.StatusProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            //
            // StatusStrip
            //
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusProgressBar1});
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripLabel StatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar StatusProgressBar1;

    }
}
