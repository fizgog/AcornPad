using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcornPad.Controls
{
    [ToolboxBitmap(typeof(ComboBox))]
    class ColourComboBox : ComboBox
    {
        /// <summary>
        /// 
        /// </summary>
        public ColourComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (string ColorName in Enum.GetNames(typeof(KnownColor)))
                Items.Add(Color.FromName(ColorName));

            SelectedIndex = 0;
            ItemHeight = 16;
        }

        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index >= 0)
            {
                Color CurrentColor = (Color)Items[e.Index];

                Rectangle SizeRect = new Rectangle(4, e.Bounds.Top + 2, 21, e.Bounds.Height - 5);

                using (SolidBrush sb = new SolidBrush(CurrentColor))
                {
                    e.Graphics.FillRectangle(sb, SizeRect);
                    e.Graphics.DrawRectangle(Pens.Black, SizeRect);

                    using (SolidBrush ComboBrush = new SolidBrush(Color.Black))
                    {
                        e.Graphics.DrawString(CurrentColor.Name, this.Font, ComboBrush, 30, ((e.Bounds.Height - this.Font.Height) / 2) + e.Bounds.Top);
                    }
                }
            }
        }


    }
}
