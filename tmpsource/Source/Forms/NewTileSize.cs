using System;
using System.Drawing;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class NewTileSize : Form
    {
        public Size TileSize { get; set; }

        public NewTileSize()
        {
            InitializeComponent();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void tableControl1_TableControl_Cancelled(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableControl1_TableControl_Selected(object sender, WindowsFormsApp1.EventArgs.TableEventArgs e)
        {
            TileSize = tableControl1.SelectedSize;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}