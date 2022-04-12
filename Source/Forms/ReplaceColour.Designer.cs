
namespace AcornPad.Forms
{
    partial class ReplaceColour
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplaceColour));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.statusStrip1 = new AcornPad.Controls.StatusStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonColour1 = new AcornPad.Controls.ButtonColour();
            this.ColourPicker1 = new AcornPad.Controls.ColourPicker();
            this.ColourPicker2 = new AcornPad.Controls.ColourPicker();
            this.buttonColour2 = new AcornPad.Controls.ButtonColour();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.flowLayoutPanel1.Controls.Add(this.ButtonCancel);
            this.flowLayoutPanel1.Controls.Add(this.ButtonOK);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 185);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(276, 29);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(198, 3);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 1;
            this.ButtonCancel.Text = "&Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonOK
            // 
            this.ButtonOK.Location = new System.Drawing.Point(117, 3);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(75, 23);
            this.ButtonOK.TabIndex = 0;
            this.ButtonOK.Text = "&OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Location = new System.Drawing.Point(0, 214);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(276, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Current Colour";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "New Colour";
            // 
            // buttonColour1
            // 
            this.buttonColour1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour1.Location = new System.Drawing.Point(12, 15);
            this.buttonColour1.MaximumSize = new System.Drawing.Size(100, 75);
            this.buttonColour1.MinimumSize = new System.Drawing.Size(42, 42);
            this.buttonColour1.Name = "buttonColour1";
            this.buttonColour1.Size = new System.Drawing.Size(100, 75);
            this.buttonColour1.TabIndex = 12;
            // 
            // ColourPicker1
            // 
            this.ColourPicker1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ColourPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColourPicker1.FormattingEnabled = true;
            this.ColourPicker1.Location = new System.Drawing.Point(97, 116);
            this.ColourPicker1.Name = "ColourPicker1";
            this.ColourPicker1.SelectedItem = null;
            this.ColourPicker1.SelectedValue = System.Drawing.Color.White;
            this.ColourPicker1.Size = new System.Drawing.Size(167, 21);
            this.ColourPicker1.TabIndex = 14;
            this.ColourPicker1.SelectedIndexChanged += new System.EventHandler(this.ColourPicker_SelectedIndexChanged);
            // 
            // ColourPicker2
            // 
            this.ColourPicker2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ColourPicker2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColourPicker2.FormattingEnabled = true;
            this.ColourPicker2.Location = new System.Drawing.Point(97, 144);
            this.ColourPicker2.Name = "ColourPicker2";
            this.ColourPicker2.SelectedItem = null;
            this.ColourPicker2.SelectedValue = System.Drawing.Color.White;
            this.ColourPicker2.Size = new System.Drawing.Size(167, 21);
            this.ColourPicker2.TabIndex = 15;
            this.ColourPicker2.SelectedIndexChanged += new System.EventHandler(this.ColourPicker_SelectedIndexChanged);
            // 
            // buttonColour2
            // 
            this.buttonColour2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour2.Location = new System.Drawing.Point(164, 15);
            this.buttonColour2.MaximumSize = new System.Drawing.Size(100, 75);
            this.buttonColour2.MinimumSize = new System.Drawing.Size(42, 42);
            this.buttonColour2.Name = "buttonColour2";
            this.buttonColour2.Size = new System.Drawing.Size(100, 75);
            this.buttonColour2.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(130, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = ">";
            // 
            // ReplaceColour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 236);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonColour2);
            this.Controls.Add(this.ColourPicker2);
            this.Controls.Add(this.ColourPicker1);
            this.Controls.Add(this.buttonColour1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplaceColour";
            this.Text = "Replace Colour";
            this.Load += new System.EventHandler(this.ReplaceColour_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.StatusStrip statusStrip1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOK;
        
        private System.Windows.Forms.Label label1;
        
        private System.Windows.Forms.Label label2;
        private Controls.ButtonColour buttonColour1;
        private Controls.ColourPicker ColourPicker1;
        private Controls.ColourPicker ColourPicker2;
        private Controls.ButtonColour buttonColour2;
        private System.Windows.Forms.Label label3;
    }
}