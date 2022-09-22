
namespace AcornPad.Forms
{
    partial class MapEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapEdit));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ButtonGrid = new System.Windows.Forms.ToolStripButton();
            this.ButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.ButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonPen = new System.Windows.Forms.ToolStripButton();
            this.ButtonBrush = new System.Windows.Forms.ToolStripButton();
            this.ButtonFloodFill = new System.Windows.Forms.ToolStripButton();
            this.ButtonPicker = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripNumericTextBox3 = new WindowsFormsApp1.Controls.ToolStripNumericTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripNumericTextBox1 = new WindowsFormsApp1.Controls.ToolStripNumericTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripNumericTextBox2 = new WindowsFormsApp1.Controls.ToolStripNumericTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.transformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FlipHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FlipVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ShiftLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShiftRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShiftUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShiftDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageBox1 = new AcornPad.Controls.ImageBox();
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
            this.ButtonPen,
            this.ButtonBrush,
            this.ButtonFloodFill,
            this.ButtonPicker,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.toolStripNumericTextBox3,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.toolStripNumericTextBox1,
            this.toolStripLabel2,
            this.toolStripNumericTextBox2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(584, 25);
            this.toolStrip1.TabIndex = 5;
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
            // ButtonPen
            // 
            this.ButtonPen.Checked = true;
            this.ButtonPen.CheckOnClick = true;
            this.ButtonPen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ButtonPen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonPen.Image = ((System.Drawing.Image)(resources.GetObject("ButtonPen.Image")));
            this.ButtonPen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonPen.Name = "ButtonPen";
            this.ButtonPen.Size = new System.Drawing.Size(23, 22);
            this.ButtonPen.Text = "Pen";
            // 
            // ButtonBrush
            // 
            this.ButtonBrush.CheckOnClick = true;
            this.ButtonBrush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonBrush.Image = ((System.Drawing.Image)(resources.GetObject("ButtonBrush.Image")));
            this.ButtonBrush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonBrush.Name = "ButtonBrush";
            this.ButtonBrush.Size = new System.Drawing.Size(23, 22);
            this.ButtonBrush.Text = "Brush";
            // 
            // ButtonFloodFill
            // 
            this.ButtonFloodFill.CheckOnClick = true;
            this.ButtonFloodFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonFloodFill.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFloodFill.Image")));
            this.ButtonFloodFill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonFloodFill.Name = "ButtonFloodFill";
            this.ButtonFloodFill.Size = new System.Drawing.Size(23, 22);
            this.ButtonFloodFill.Text = "Flood Fill";
            // 
            // ButtonPicker
            // 
            this.ButtonPicker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonPicker.Image = global::AcornPad.Properties.Resources.ColourPicker;
            this.ButtonPicker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonPicker.Name = "ButtonPicker";
            this.ButtonPicker.Size = new System.Drawing.Size(23, 22);
            this.ButtonPicker.Text = "Picker";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(31, 22);
            this.toolStripLabel3.Text = "Map";
            // 
            // toolStripNumericTextBox3
            // 
            this.toolStripNumericTextBox3.MaxValue = 999999;
            this.toolStripNumericTextBox3.MinValue = 1;
            this.toolStripNumericTextBox3.Name = "toolStripNumericTextBox3";
            this.toolStripNumericTextBox3.Size = new System.Drawing.Size(50, 25);
            this.toolStripNumericTextBox3.Text = "1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Width";
            // 
            // toolStripNumericTextBox1
            // 
            this.toolStripNumericTextBox1.MaxValue = 99999;
            this.toolStripNumericTextBox1.MinValue = 1;
            this.toolStripNumericTextBox1.Name = "toolStripNumericTextBox1";
            this.toolStripNumericTextBox1.Size = new System.Drawing.Size(50, 25);
            this.toolStripNumericTextBox1.Text = "1";
            this.toolStripNumericTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ToolStripNumericTextBox1_KeyUp);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabel2.Text = "Height";
            // 
            // toolStripNumericTextBox2
            // 
            this.toolStripNumericTextBox2.MaxValue = 99999;
            this.toolStripNumericTextBox2.MinValue = 1;
            this.toolStripNumericTextBox2.Name = "toolStripNumericTextBox2";
            this.toolStripNumericTextBox2.Size = new System.Drawing.Size(50, 25);
            this.toolStripNumericTextBox2.Text = "1";
            this.toolStripNumericTextBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ToolStripNumericTextBox2_KeyUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusLabel2,
            this.toolStripStatusLabel2,
            this.StatusLabel3,
            this.StatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.StatusLabel1.Text = "Ready";
            this.StatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel2
            // 
            this.StatusLabel2.Name = "StatusLabel2";
            this.StatusLabel2.Size = new System.Drawing.Size(459, 17);
            this.StatusLabel2.Spring = true;
            this.StatusLabel2.Text = "Char";
            this.StatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // StatusLabel3
            // 
            this.StatusLabel3.Name = "StatusLabel3";
            this.StatusLabel3.Size = new System.Drawing.Size(32, 17);
            this.StatusLabel3.Text = "Cells";
            // 
            // StatusLabel4
            // 
            this.StatusLabel4.Name = "StatusLabel4";
            this.StatusLabel4.Size = new System.Drawing.Size(39, 17);
            this.StatusLabel4.Text = "Zoom";
            this.StatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.transformToolStripMenuItem});
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
            // transformToolStripMenuItem
            // 
            this.transformToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FlipHorizontalToolStripMenuItem,
            this.FlipVerticalToolStripMenuItem,
            this.toolStripMenuItem2,
            this.ShiftLeftToolStripMenuItem,
            this.ShiftRightToolStripMenuItem,
            this.ShiftUpToolStripMenuItem,
            this.ShiftDownToolStripMenuItem});
            this.transformToolStripMenuItem.Name = "transformToolStripMenuItem";
            this.transformToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.transformToolStripMenuItem.Text = "Transform";
            // 
            // FlipHorizontalToolStripMenuItem
            // 
            this.FlipHorizontalToolStripMenuItem.Image = global::AcornPad.Properties.Resources.FlipHorizontal;
            this.FlipHorizontalToolStripMenuItem.Name = "FlipHorizontalToolStripMenuItem";
            this.FlipHorizontalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
            this.FlipHorizontalToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.FlipHorizontalToolStripMenuItem.Text = "Flip Horizontal";
            this.FlipHorizontalToolStripMenuItem.Click += new System.EventHandler(this.FlipHorizontalToolStripMenuItem_Click);
            // 
            // FlipVerticalToolStripMenuItem
            // 
            this.FlipVerticalToolStripMenuItem.Image = global::AcornPad.Properties.Resources.FlipVertical;
            this.FlipVerticalToolStripMenuItem.Name = "FlipVerticalToolStripMenuItem";
            this.FlipVerticalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
            this.FlipVerticalToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.FlipVerticalToolStripMenuItem.Text = "Flip Vertical";
            this.FlipVerticalToolStripMenuItem.Click += new System.EventHandler(this.FlipVerticalToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(217, 6);
            // 
            // ShiftLeftToolStripMenuItem
            // 
            this.ShiftLeftToolStripMenuItem.Image = global::AcornPad.Properties.Resources.ShiftLeft;
            this.ShiftLeftToolStripMenuItem.Name = "ShiftLeftToolStripMenuItem";
            this.ShiftLeftToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Left)));
            this.ShiftLeftToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.ShiftLeftToolStripMenuItem.Text = "Shift Left";
            this.ShiftLeftToolStripMenuItem.Click += new System.EventHandler(this.ShiftLeftToolStripMenuItem_Click);
            // 
            // ShiftRightToolStripMenuItem
            // 
            this.ShiftRightToolStripMenuItem.Image = global::AcornPad.Properties.Resources.ShiftRight;
            this.ShiftRightToolStripMenuItem.Name = "ShiftRightToolStripMenuItem";
            this.ShiftRightToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Right)));
            this.ShiftRightToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.ShiftRightToolStripMenuItem.Text = "Shift Right";
            this.ShiftRightToolStripMenuItem.Click += new System.EventHandler(this.ShiftRightToolStripMenuItem_Click);
            // 
            // ShiftUpToolStripMenuItem
            // 
            this.ShiftUpToolStripMenuItem.Image = global::AcornPad.Properties.Resources.ShiftUp;
            this.ShiftUpToolStripMenuItem.Name = "ShiftUpToolStripMenuItem";
            this.ShiftUpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Up)));
            this.ShiftUpToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.ShiftUpToolStripMenuItem.Text = "Shift Up";
            this.ShiftUpToolStripMenuItem.Click += new System.EventHandler(this.ShiftUpToolStripMenuItem_Click);
            // 
            // ShiftDownToolStripMenuItem
            // 
            this.ShiftDownToolStripMenuItem.Image = global::AcornPad.Properties.Resources.ShiftDown;
            this.ShiftDownToolStripMenuItem.Name = "ShiftDownToolStripMenuItem";
            this.ShiftDownToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Down)));
            this.ShiftDownToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.ShiftDownToolStripMenuItem.Text = "Shift Down";
            this.ShiftDownToolStripMenuItem.Click += new System.EventHandler(this.ShiftDownToolStripMenuItem_Click);
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
            this.ImageBox1.ImageSize = new System.Drawing.Size(20, 32);
            this.ImageBox1.Location = new System.Drawing.Point(0, 25);
            this.ImageBox1.Name = "ImageBox1";
            this.ImageBox1.PaintTool = 0;
            this.ImageBox1.PixelFormatString = null;
            this.ImageBox1.PixelSize = 1;
            this.ImageBox1.ShowGrid = true;
            this.ImageBox1.ShowTileGrid = false;
            this.ImageBox1.Size = new System.Drawing.Size(584, 314);
            this.ImageBox1.TabIndex = 6;
            this.ImageBox1.Text = "imageBox1";
            this.ImageBox1.ZoomFactor = 2;
            this.ImageBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageBox1_MouseDown);
            this.ImageBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageBox1_MouseMove);
            this.ImageBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageBox1_MouseUp);
            // 
            // MapEdit
            // 
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.ImageBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Map Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapEdit_FormClosing);
            this.Load += new System.EventHandler(this.MapEdit_Load);
            this.Shown += new System.EventHandler(this.MapEdit_Shown);
            this.Move += new System.EventHandler(this.MapEdit_Move);
            this.Resize += new System.EventHandler(this.MapEdit_Resize);
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
        private System.Windows.Forms.ToolStripButton ButtonPen;
        private System.Windows.Forms.ToolStripButton ButtonBrush;
        private System.Windows.Forms.ToolStripButton ButtonFloodFill;
        private Controls.ImageBox ImageBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem transformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FlipHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FlipVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ShiftLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShiftRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShiftUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShiftDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton ButtonPicker;
        private WindowsFormsApp1.Controls.ToolStripNumericTextBox toolStripNumericTextBox1;
        private WindowsFormsApp1.Controls.ToolStripNumericTextBox toolStripNumericTextBox2;
        private WindowsFormsApp1.Controls.ToolStripNumericTextBox toolStripNumericTextBox3;
    }
}