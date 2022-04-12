using AcornPad.Internal;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AcornPad.Controls
{
    public partial class ColourPicker : ComboBox
    {
        public class ColourInfo
        {
            public string Text { get; set; }
            public Color Color { get; set; }

            public ColourInfo(string text, Color color)
            {
                Text = text;
                Color = color;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ColourPicker()
        {
            InitializeComponent();

            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawFixed;
            DrawItem += OnDrawItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palette"></param>
        public void AddPalette(Palette palette)
        {
            Items.Clear();

            for (int i = 0; i < palette.NumColours; i++)
            {
                Items.Add(new ColourInfo(palette.GetAcornColourSet[i].ToString(), palette.WinColours[i]));
            }
            SelectedIndex = 0;
        }

        // Draw list item
        protected void OnDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();

                ColourInfo current = (ColourInfo)Items[e.Index];

                Rectangle rect = new Rectangle(4, e.Bounds.Top + 2, 21, e.Bounds.Height - 5);

                using (SolidBrush brush = new SolidBrush(current.Color))
                {
                    e.Graphics.FillRectangle(brush, rect);
                    e.Graphics.DrawRectangle(Pens.Black, rect);

                    using (SolidBrush brush2 = new SolidBrush(Color.Black))
                    {
                        e.Graphics.DrawString(string.Format("{0:#0} {1}", e.Index, current.Text), Font, brush2, 30, ((e.Bounds.Height - Font.Height) / 2) + e.Bounds.Top);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the currently selected item.
        /// </summary>
        public new ColourInfo SelectedItem
        {
            get
            {
                return (ColourInfo)base.SelectedItem;
            }
            set
            {
                base.SelectedItem = value;
            }
        }

        /// <summary>
        /// Gets the text of the selected item, or sets the selection to
        /// the item with the specified text.
        /// </summary>
        public new string SelectedText
        {
            get
            {
                if (SelectedIndex >= 0)
                    return SelectedItem.Text;
                return String.Empty;
            }
            set
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (((ColourInfo)Items[i]).Text == value)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the value of the selected item, or sets the selection to
        /// the item with the specified value.
        /// </summary>
        public new Color SelectedValue
        {
            get
            {
                if (SelectedIndex >= 0)
                    return SelectedItem.Color;
                return Color.White;
            }
            set
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (((ColourInfo)Items[i]).Color == value)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}