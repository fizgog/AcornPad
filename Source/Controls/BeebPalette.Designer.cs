using System.Drawing;
using System.Windows.Forms;

namespace AcornPad.Controls
{
    partial class BeebPalette
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
            this.LabelDrawColour = new System.Windows.Forms.Label();
            this.LabelEraseColour = new System.Windows.Forms.Label();
            this.ButtonReset2 = new System.Windows.Forms.Button();
            this.ButtonDrawColour = new System.Windows.Forms.Button();
            this.ButtonEraseColour = new System.Windows.Forms.Button();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.ButtonReset1 = new System.Windows.Forms.Button();
            this.buttonColour1 = new AcornPad.Controls.ButtonColour();
            this.buttonColour2 = new AcornPad.Controls.ButtonColour();
            this.buttonColour3 = new AcornPad.Controls.ButtonColour();
            this.buttonColour4 = new AcornPad.Controls.ButtonColour();
            this.buttonColour5 = new AcornPad.Controls.ButtonColour();
            this.buttonColour6 = new AcornPad.Controls.ButtonColour();
            this.buttonColour7 = new AcornPad.Controls.ButtonColour();
            this.buttonColour8 = new AcornPad.Controls.ButtonColour();
            this.buttonColour9 = new AcornPad.Controls.ButtonColour();
            this.buttonColour10 = new AcornPad.Controls.ButtonColour();
            this.buttonColour11 = new AcornPad.Controls.ButtonColour();
            this.buttonColour12 = new AcornPad.Controls.ButtonColour();
            this.buttonColour13 = new AcornPad.Controls.ButtonColour();
            this.buttonColour14 = new AcornPad.Controls.ButtonColour();
            this.buttonColour15 = new AcornPad.Controls.ButtonColour();
            this.buttonColour16 = new AcornPad.Controls.ButtonColour();
            this.buttonColour17 = new AcornPad.Controls.ButtonColour();
            this.SuspendLayout();
            // 
            // LabelDrawColour
            // 
            this.LabelDrawColour.AutoSize = true;
            this.LabelDrawColour.Location = new System.Drawing.Point(64, 16);
            this.LabelDrawColour.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelDrawColour.Name = "LabelDrawColour";
            this.LabelDrawColour.Size = new System.Drawing.Size(65, 13);
            this.LabelDrawColour.TabIndex = 43;
            this.LabelDrawColour.Text = "Draw Colour";
            // 
            // LabelEraseColour
            // 
            this.LabelEraseColour.AutoSize = true;
            this.LabelEraseColour.Location = new System.Drawing.Point(64, 41);
            this.LabelEraseColour.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelEraseColour.Name = "LabelEraseColour";
            this.LabelEraseColour.Size = new System.Drawing.Size(67, 13);
            this.LabelEraseColour.TabIndex = 44;
            this.LabelEraseColour.Text = "Erase Colour";
            // 
            // ButtonReset2
            // 
            this.ButtonReset2.AutoSize = true;
            this.ButtonReset2.Location = new System.Drawing.Point(205, 111);
            this.ButtonReset2.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonReset2.Name = "ButtonReset2";
            this.ButtonReset2.Size = new System.Drawing.Size(47, 23);
            this.ButtonReset2.TabIndex = 16;
            this.ButtonReset2.Text = "Reset";
            this.ButtonReset2.UseVisualStyleBackColor = true;
            this.ButtonReset2.Click += new System.EventHandler(this.ButtonReset2_Click);
            // 
            // ButtonDrawColour
            // 
            this.ButtonDrawColour.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonDrawColour.Enabled = false;
            this.ButtonDrawColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDrawColour.Location = new System.Drawing.Point(9, 11);
            this.ButtonDrawColour.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonDrawColour.MaximumSize = new System.Drawing.Size(45, 22);
            this.ButtonDrawColour.MinimumSize = new System.Drawing.Size(21, 22);
            this.ButtonDrawColour.Name = "ButtonDrawColour";
            this.ButtonDrawColour.Size = new System.Drawing.Size(45, 22);
            this.ButtonDrawColour.TabIndex = 17;
            this.ButtonDrawColour.TabStop = false;
            this.ButtonDrawColour.UseVisualStyleBackColor = false;
            // 
            // ButtonEraseColour
            // 
            this.ButtonEraseColour.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonEraseColour.Enabled = false;
            this.ButtonEraseColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonEraseColour.Location = new System.Drawing.Point(9, 36);
            this.ButtonEraseColour.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonEraseColour.MaximumSize = new System.Drawing.Size(45, 22);
            this.ButtonEraseColour.MinimumSize = new System.Drawing.Size(21, 22);
            this.ButtonEraseColour.Name = "ButtonEraseColour";
            this.ButtonEraseColour.Size = new System.Drawing.Size(45, 22);
            this.ButtonEraseColour.TabIndex = 18;
            this.ButtonEraseColour.TabStop = false;
            this.ButtonEraseColour.UseVisualStyleBackColor = false;
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Location = new System.Drawing.Point(172, 39);
            this.RadioButton2.Margin = new System.Windows.Forms.Padding(2);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(80, 17);
            this.RadioButton2.TabIndex = 46;
            this.RadioButton2.TabStop = true;
            this.RadioButton2.Text = "ColourSet 2";
            this.RadioButton2.UseVisualStyleBackColor = true;
            this.RadioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Checked = true;
            this.RadioButton1.Cursor = System.Windows.Forms.Cursors.Default;
            this.RadioButton1.Location = new System.Drawing.Point(172, 14);
            this.RadioButton1.Margin = new System.Windows.Forms.Padding(2);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(80, 17);
            this.RadioButton1.TabIndex = 45;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "ColourSet 1";
            this.RadioButton1.UseVisualStyleBackColor = true;
            this.RadioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // ButtonReset1
            // 
            this.ButtonReset1.AutoSize = true;
            this.ButtonReset1.Location = new System.Drawing.Point(205, 82);
            this.ButtonReset1.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonReset1.Name = "ButtonReset1";
            this.ButtonReset1.Size = new System.Drawing.Size(47, 23);
            this.ButtonReset1.TabIndex = 47;
            this.ButtonReset1.Text = "Reset";
            this.ButtonReset1.UseVisualStyleBackColor = true;
            this.ButtonReset1.Click += new System.EventHandler(this.ButtonReset1_Click);
            // 
            // buttonColour1
            // 
            this.buttonColour1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour1.Location = new System.Drawing.Point(9, 83);
            this.buttonColour1.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour1.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour1.Name = "buttonColour1";
            this.buttonColour1.Size = new System.Drawing.Size(22, 22);
            this.buttonColour1.TabIndex = 27;
            this.buttonColour1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour1_MouseClick);
            this.buttonColour1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour1_MouseDoubleClick);
            // 
            // buttonColour2
            // 
            this.buttonColour2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour2.Location = new System.Drawing.Point(33, 83);
            this.buttonColour2.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour2.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour2.Name = "buttonColour2";
            this.buttonColour2.Size = new System.Drawing.Size(22, 22);
            this.buttonColour2.TabIndex = 28;
            this.buttonColour2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour2_MouseClick);
            this.buttonColour2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour2_MouseDoubleClick);
            // 
            // buttonColour3
            // 
            this.buttonColour3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour3.Location = new System.Drawing.Point(57, 83);
            this.buttonColour3.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour3.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour3.Name = "buttonColour3";
            this.buttonColour3.Size = new System.Drawing.Size(22, 22);
            this.buttonColour3.TabIndex = 29;
            this.buttonColour3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour3_MouseClick);
            this.buttonColour3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour3_MouseDoubleClick);
            // 
            // buttonColour4
            // 
            this.buttonColour4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour4.Location = new System.Drawing.Point(81, 83);
            this.buttonColour4.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour4.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour4.Name = "buttonColour4";
            this.buttonColour4.Size = new System.Drawing.Size(22, 22);
            this.buttonColour4.TabIndex = 30;
            this.buttonColour4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour4_MouseClick);
            this.buttonColour4.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour4_MouseDoubleClick);
            // 
            // buttonColour5
            // 
            this.buttonColour5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour5.Location = new System.Drawing.Point(105, 83);
            this.buttonColour5.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour5.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour5.Name = "buttonColour5";
            this.buttonColour5.Size = new System.Drawing.Size(22, 22);
            this.buttonColour5.TabIndex = 31;
            this.buttonColour5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour5_MouseClick);
            this.buttonColour5.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour5_MouseDoubleClick);
            // 
            // buttonColour6
            // 
            this.buttonColour6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour6.Location = new System.Drawing.Point(129, 83);
            this.buttonColour6.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour6.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour6.Name = "buttonColour6";
            this.buttonColour6.Size = new System.Drawing.Size(22, 22);
            this.buttonColour6.TabIndex = 32;
            this.buttonColour6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour6_MouseClick);
            this.buttonColour6.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour6_MouseDoubleClick);
            // 
            // buttonColour7
            // 
            this.buttonColour7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour7.Location = new System.Drawing.Point(153, 83);
            this.buttonColour7.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour7.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour7.Name = "buttonColour7";
            this.buttonColour7.Size = new System.Drawing.Size(22, 22);
            this.buttonColour7.TabIndex = 33;
            this.buttonColour7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour7_MouseClick);
            this.buttonColour7.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour7_MouseDoubleClick);
            // 
            // buttonColour8
            // 
            this.buttonColour8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour8.Location = new System.Drawing.Point(177, 83);
            this.buttonColour8.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour8.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour8.Name = "buttonColour8";
            this.buttonColour8.Size = new System.Drawing.Size(22, 22);
            this.buttonColour8.TabIndex = 34;
            this.buttonColour8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour8_MouseClick);
            this.buttonColour8.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour8_MouseDoubleClick);
            // 
            // buttonColour9
            // 
            this.buttonColour9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour9.Location = new System.Drawing.Point(9, 111);
            this.buttonColour9.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour9.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour9.Name = "buttonColour9";
            this.buttonColour9.Size = new System.Drawing.Size(22, 22);
            this.buttonColour9.TabIndex = 35;
            this.buttonColour9.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour9_MouseClick);
            this.buttonColour9.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour9_MouseDoubleClick);
            // 
            // buttonColour10
            // 
            this.buttonColour10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour10.Location = new System.Drawing.Point(33, 111);
            this.buttonColour10.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour10.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour10.Name = "buttonColour10";
            this.buttonColour10.Size = new System.Drawing.Size(22, 22);
            this.buttonColour10.TabIndex = 36;
            this.buttonColour10.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour10_MouseClick);
            this.buttonColour10.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour10_MouseDoubleClick);
            // 
            // buttonColour11
            // 
            this.buttonColour11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour11.Location = new System.Drawing.Point(57, 111);
            this.buttonColour11.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour11.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour11.Name = "buttonColour11";
            this.buttonColour11.Size = new System.Drawing.Size(22, 22);
            this.buttonColour11.TabIndex = 37;
            this.buttonColour11.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour11_MouseClick);
            this.buttonColour11.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour11_MouseDoubleClick);
            // 
            // buttonColour12
            // 
            this.buttonColour12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour12.Location = new System.Drawing.Point(81, 111);
            this.buttonColour12.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour12.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour12.Name = "buttonColour12";
            this.buttonColour12.Size = new System.Drawing.Size(22, 22);
            this.buttonColour12.TabIndex = 38;
            this.buttonColour12.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour12_MouseClick);
            this.buttonColour12.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour12_MouseDoubleClick);
            // 
            // buttonColour13
            // 
            this.buttonColour13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour13.Location = new System.Drawing.Point(105, 111);
            this.buttonColour13.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour13.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour13.Name = "buttonColour13";
            this.buttonColour13.Size = new System.Drawing.Size(22, 22);
            this.buttonColour13.TabIndex = 38;
            this.buttonColour13.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour13_MouseClick);
            this.buttonColour13.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour13_MouseDoubleClick);
            // 
            // buttonColour14
            // 
            this.buttonColour14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour14.Location = new System.Drawing.Point(129, 111);
            this.buttonColour14.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour14.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour14.Name = "buttonColour14";
            this.buttonColour14.Size = new System.Drawing.Size(22, 22);
            this.buttonColour14.TabIndex = 39;
            this.buttonColour14.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour14_MouseClick);
            this.buttonColour14.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour14_MouseDoubleClick);
            // 
            // buttonColour15
            // 
            this.buttonColour15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour15.Location = new System.Drawing.Point(153, 111);
            this.buttonColour15.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour15.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour15.Name = "buttonColour15";
            this.buttonColour15.Size = new System.Drawing.Size(22, 22);
            this.buttonColour15.TabIndex = 40;
            this.buttonColour15.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour15_MouseClick);
            this.buttonColour15.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour15_MouseDoubleClick);
            // 
            // buttonColour16
            // 
            this.buttonColour16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour16.Location = new System.Drawing.Point(177, 111);
            this.buttonColour16.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour16.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour16.Name = "buttonColour16";
            this.buttonColour16.Size = new System.Drawing.Size(22, 22);
            this.buttonColour16.TabIndex = 41;
            this.buttonColour16.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour16_MouseClick);
            this.buttonColour16.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour16_MouseDoubleClick);
            // 
            // buttonColour17
            // 
            this.buttonColour17.BackColor = System.Drawing.Color.Red;
            this.buttonColour17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonColour17.Location = new System.Drawing.Point(66, 473);
            this.buttonColour17.MaximumSize = new System.Drawing.Size(22, 22);
            this.buttonColour17.MinimumSize = new System.Drawing.Size(22, 22);
            this.buttonColour17.Name = "buttonColour17";
            this.buttonColour17.Size = new System.Drawing.Size(22, 22);
            this.buttonColour17.TabIndex = 42;
            this.buttonColour17.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour17_MouseClick);
            this.buttonColour17.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.buttonColour17_MouseDoubleClick);
            // 
            // BeebPalette
            // 
            this.Controls.Add(this.buttonColour1);
            this.Controls.Add(this.buttonColour2);
            this.Controls.Add(this.buttonColour3);
            this.Controls.Add(this.buttonColour4);
            this.Controls.Add(this.buttonColour5);
            this.Controls.Add(this.buttonColour6);
            this.Controls.Add(this.buttonColour7);
            this.Controls.Add(this.buttonColour8);
            this.Controls.Add(this.buttonColour9);
            this.Controls.Add(this.buttonColour10);
            this.Controls.Add(this.buttonColour11);
            this.Controls.Add(this.buttonColour12);
            this.Controls.Add(this.buttonColour13);
            this.Controls.Add(this.buttonColour14);
            this.Controls.Add(this.buttonColour15);
            this.Controls.Add(this.buttonColour16);
            this.Controls.Add(this.ButtonDrawColour);
            this.Controls.Add(this.ButtonEraseColour);
            this.Controls.Add(this.LabelEraseColour);
            this.Controls.Add(this.LabelDrawColour);
            this.Controls.Add(this.ButtonReset1);
            this.Controls.Add(this.ButtonReset2);
            this.Controls.Add(this.RadioButton2);
            this.Controls.Add(this.RadioButton1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BeebPalette";
            this.Size = new System.Drawing.Size(268, 141);
            this.Load += new System.EventHandler(this.BeebPalette_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private ButtonColour buttonColour1;
        private ButtonColour buttonColour2;
        private ButtonColour buttonColour3;
        private ButtonColour buttonColour4;
        private ButtonColour buttonColour5;
        private ButtonColour buttonColour6;
        private ButtonColour buttonColour7;
        private ButtonColour buttonColour8;
        private ButtonColour buttonColour9;
        private ButtonColour buttonColour10;
        private ButtonColour buttonColour11;
        private ButtonColour buttonColour12;
        private ButtonColour buttonColour13;
        private ButtonColour buttonColour14;
        private ButtonColour buttonColour15;
        private ButtonColour buttonColour16;
        private ButtonColour buttonColour17;

        private Button ButtonReset2;
        private Button ButtonDrawColour;
        private Button ButtonEraseColour;

        private Label LabelDrawColour;
        private Label LabelEraseColour;
        private RadioButton RadioButton2;
        private RadioButton RadioButton1;
        private Button ButtonReset1;
    }
}

