
using System;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    partial class CharSet
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharSet));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ButtonGrid = new System.Windows.Forms.ToolStripButton();
            this.ButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.ButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripNumericTextBox1 = new WindowsFormsApp1.Controls.ToolStripNumericTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonCompress = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ImageBox1 = new AcornPad.Controls.ImageBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.colourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceColourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonGrid,
            this.ButtonZoomOut,
            this.ButtonZoomIn,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripNumericTextBox1,
            this.toolStripSeparator2,
            this.ButtonCompress});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(400, 25);
            this.toolStrip1.TabIndex = 4;
            // 
            // ButtonGrid
            // 
            this.ButtonGrid.Checked = true;
            this.ButtonGrid.CheckOnClick = true;
            this.ButtonGrid.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.ButtonGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonGrid.Image = global::AcornPad.Properties.Resources.grid;
            this.ButtonGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonGrid.Name = "ButtonGrid";
            this.ButtonGrid.Size = new System.Drawing.Size(23, 22);
            this.ButtonGrid.Text = "Grid";
            this.ButtonGrid.ToolTipText = "Grid";
            this.ButtonGrid.Click += new System.EventHandler(this.ButtonGrid_Click);
            // 
            // ButtonZoomOut
            // 
            this.ButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonZoomOut.Image = global::AcornPad.Properties.Resources.zoomout;
            this.ButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonZoomOut.Name = "ButtonZoomOut";
            this.ButtonZoomOut.Size = new System.Drawing.Size(23, 22);
            this.ButtonZoomOut.Text = "Zoom Out";
            this.ButtonZoomOut.Click += new System.EventHandler(this.ButtonZoomOut_Click);
            // 
            // ButtonZoomIn
            // 
            this.ButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonZoomIn.Image = global::AcornPad.Properties.Resources.zoomin;
            this.ButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonZoomIn.Name = "ButtonZoomIn";
            this.ButtonZoomIn.Size = new System.Drawing.Size(23, 22);
            this.ButtonZoomIn.Text = "Zoom In";
            this.ButtonZoomIn.Click += new System.EventHandler(this.ButtonZoomIn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabel1.Text = "Quantity";
            // 
            // toolStripNumericTextBox1
            // 
            this.toolStripNumericTextBox1.MaxValue = 999999;
            this.toolStripNumericTextBox1.MinValue = 1;
            this.toolStripNumericTextBox1.Name = "toolStripNumericTextBox1";
            this.toolStripNumericTextBox1.Size = new System.Drawing.Size(50, 25);
            this.toolStripNumericTextBox1.Text = "1";
            this.toolStripNumericTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ToolStripNumericTextBox1_KeyUp);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ButtonCompress
            // 
            this.ButtonCompress.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonCompress.Image = global::AcornPad.Properties.Resources.minimize;
            this.ButtonCompress.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonCompress.Name = "ButtonCompress";
            this.ButtonCompress.Size = new System.Drawing.Size(23, 22);
            this.ButtonCompress.Text = "Compress character set";
            this.ButtonCompress.Click += new System.EventHandler(this.ButtonCompress_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusLabel3,
            this.StatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 378);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(400, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(302, 17);
            this.StatusLabel1.Spring = true;
            this.StatusLabel1.Text = "Ready";
            this.StatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel3
            // 
            this.StatusLabel3.Name = "StatusLabel3";
            this.StatusLabel3.Size = new System.Drawing.Size(44, 17);
            this.StatusLabel3.Text = "0 Bytes";
            // 
            // StatusLabel4
            // 
            this.StatusLabel4.Name = "StatusLabel4";
            this.StatusLabel4.Size = new System.Drawing.Size(39, 17);
            this.StatusLabel4.Text = "Zoom";
            this.StatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ImageBox1
            // 
            this.ImageBox1.AutoScroll = true;
            this.ImageBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ImageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageBox1.DrawColour = System.Drawing.Color.Empty;
            this.ImageBox1.EraseColour = System.Drawing.Color.Empty;
            this.ImageBox1.GridSize = new System.Drawing.Size(8, 8);
            this.ImageBox1.Image = null;
            this.ImageBox1.ImageRect = new System.Drawing.Rectangle(0, 0, 192, 192);
            this.ImageBox1.ImageSize = new System.Drawing.Size(64, 1);
            this.ImageBox1.Location = new System.Drawing.Point(0, 49);
            this.ImageBox1.Name = "ImageBox1";
            this.ImageBox1.PaintTool = 0;
            this.ImageBox1.PixelFormatString = null;
            this.ImageBox1.PixelSize = 1;
            this.ImageBox1.ShowGrid = true;
            this.ImageBox1.ShowTileGrid = false;
            this.ImageBox1.Size = new System.Drawing.Size(400, 329);
            this.ImageBox1.TabIndex = 9;
            this.ImageBox1.ZoomFactor = 24;
            this.ImageBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.ImageBox1_Paint);
            this.ImageBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageBox1_MouseDown);
            this.ImageBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageBox1_MouseMove);
            this.ImageBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageBox1_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(400, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.colourToolStripMenuItem});
            this.editToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // colourToolStripMenuItem
            // 
            this.colourToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replaceColourToolStripMenuItem});
            this.colourToolStripMenuItem.Name = "colourToolStripMenuItem";
            this.colourToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.colourToolStripMenuItem.Text = "Colour";
            // 
            // replaceColourToolStripMenuItem
            // 
            this.replaceColourToolStripMenuItem.Name = "replaceColourToolStripMenuItem";
            this.replaceColourToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.replaceColourToolStripMenuItem.Text = "Replace Colour...";
            this.replaceColourToolStripMenuItem.Click += new System.EventHandler(this.ReplaceColourToolStripMenuItem_Click);
            // 
            // CharSet
            // 
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.ImageBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "CharSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Character Set";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CharSet_FormClosing);
            this.Shown += new System.EventHandler(this.CharSet_Shown);
            this.Move += new System.EventHandler(this.CharSet_Move);
            this.Resize += new System.EventHandler(this.Test_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
      
        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ButtonGrid;
        private System.Windows.Forms.ToolStripButton ButtonZoomOut;
        private System.Windows.Forms.ToolStripButton ButtonZoomIn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel4;
        private Controls.ImageBox ImageBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ButtonCompress;
        private WindowsFormsApp1.Controls.ToolStripNumericTextBox toolStripNumericTextBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem colourToolStripMenuItem;
        private ToolStripMenuItem replaceColourToolStripMenuItem;
    }
}