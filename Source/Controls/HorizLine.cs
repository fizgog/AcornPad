using System;
using System.Drawing;
using System.Windows.Forms;

namespace AcornPad.Controls
{
    public class HorizLine : Control
    {
        /// <summary>
        ///
        /// </summary>
        public HorizLine()
        {
            Size = new Size(200, 3);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Height != 3) Height = 3;
        }

        /// <summary>
        ///
        /// </summary>
        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                value.Height = 3;
                base.Size = value;
            }
        }
    }
}