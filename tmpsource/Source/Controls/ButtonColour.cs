using System;
using System.Drawing;
using System.Windows.Forms;

namespace AcornPad.Controls
{
    public class ButtonColour : Panel
    {
        private Color SavedBackColour;

        /// <summary>
        ///
        /// </summary>
        public ButtonColour()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new System.Drawing.Size(42, 42);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            LightenColour();
            base.OnMouseEnter(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            RestoreColour();
            base.OnMouseLeave(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            RestoreColour();
            base.OnMouseDown(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            LightenColour();
            base.OnMouseUp(e);
        }

        /// <summary>
        ///
        /// </summary>
        private void RestoreColour()
        {
            this.BackColor = SavedBackColour;
        }

        /// <summary>
        ///
        /// </summary>
        private void LightenColour()
        {
            this.SavedBackColour = BackColor;
            this.BackColor = ControlPaint.Dark(this.BackColor, 0.05f);
        }
    }
}