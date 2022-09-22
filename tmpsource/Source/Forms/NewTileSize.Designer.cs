
namespace AcornPad.Forms
{
    partial class NewTileSize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTileSize));
            this.tableControl1 = new WindowsFormsApp1.Controls.TableControl();
            this.SuspendLayout();
            // 
            // tableControl1
            // 
            this.tableControl1.ButtonText = "2 x 2 Tile";
            this.tableControl1.Location = new System.Drawing.Point(8, 7);
            this.tableControl1.Name = "tableControl1";
            this.tableControl1.SelectedSize = new System.Drawing.Size(2, 2);
            this.tableControl1.Size = new System.Drawing.Size(244, 261);
            this.tableControl1.TabIndex = 7;
            this.tableControl1.Text = "tableControl1";
            this.tableControl1.TableControl_Selected += new WindowsFormsApp1.Controls.TableEventHandler(this.tableControl1_TableControl_Selected);
            this.tableControl1.TableControl_Cancelled += new System.EventHandler(this.tableControl1_TableControl_Cancelled);
            // 
            // NewTileSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 275);
            this.Controls.Add(this.tableControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewTileSize";
            this.Text = "New Tile Size";
            this.ResumeLayout(false);

        }

        #endregion
        private WindowsFormsApp1.Controls.TableControl tableControl1;
    }
}