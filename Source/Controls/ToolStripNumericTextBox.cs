using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace WindowsFormsApp1.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
    public class ToolStripNumericTextBox : ToolStripTextBox
    {
        /// <summary>
        ///
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int MaxValue { get; set; }

        public ToolStripNumericTextBox() : base()
        {
            MinValue = 1;
            MaxValue = 999999;
            Text = MinValue.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == '\b')
                return;

            if (char.IsDigit(e.KeyChar))
            {
                string value = Text + e.KeyChar;

                if (Int32.Parse(value) >= MinValue && Int32.Parse(value) <= MaxValue)
                    return;
            }

            e.Handled = true;
        }
    }
}