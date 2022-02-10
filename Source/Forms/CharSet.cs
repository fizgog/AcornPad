using System;
using System.Drawing;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class CharSet : Form
    {
        private const int ZOOM_INCREMENT = 1;
        private const int ZOOM_MIN_FACTOR = 2;
        private const int ZOOM_MAX_FACTOR = 10;

        private Color purple = Color.FromArgb(113, 96, 232);
        private Color green = Color.FromArgb(108, 203, 95);

        private readonly AcornProject Project;

        public event EventHandler CharSet_ImageChanged;

        public CharSet(AcornProject project)
        {
            InitializeComponent();
            Project = project;

            ImageBox1.MouseWheel += ImageBox1_MouseWheel;

            Location = Project.CharSetForm.Location;
            Size = Project.CharSetForm.Size;

            ImageBox1.ZoomFactor = Project.CharSetForm.ZoomFactor;

            toolStripNumericTextBox1.Text = Project.Chars.Count.ToString();

            ImageBox1.PixelSize = Project.Machine.PixelSize;
            ImageBox1.ImageSize = new System.Drawing.Size(64, 1);
            ImageBox1.GridSize = new System.Drawing.Size(Project.Chars.Width * ImageBox1.PixelSize, Project.Chars.Height);

            StatusLabel1.Text = "Ready";
            StatusLabel2.Text = string.Format("{0} Bytes", Project.Chars.Count * 8 * Project.Machine.PixelsBerByte);
            StatusLabel3.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharSet_Shown(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharSet_Move(object sender, EventArgs e)
        {
            Project.CharSetForm.Location = Location;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Test_Resize(object sender, EventArgs e)
        {
            Project.CharSetForm.Size = Size;
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        public new void Invalidate()
        {
            if (Project != null && Project.Chars != null)
            {
                toolStripNumericTextBox1.Text = Project.Chars.Count.ToString();

                int width = (ClientRectangle.Width / Project.Chars.Width / ImageBox1.PixelSize / ImageBox1.ZoomFactor) - 1;

                width = width > 0 ? width : 1;

                int height = (Project.Chars.Count / width) + 1;

                ImageBox1.ImageSize = new Size(width, height);

                ImageBox1.DrawBitmapCharSet(Project);

                int bytes = Project.Machine.PixelsBerByte * 8;

                StatusLabel2.Text = string.Format("{0} Bytes", Project.Chars.Count * bytes);
            }

            base.Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGrid_Click(object sender, EventArgs e)
        {
            ImageBox1.ShowGrid = ButtonGrid.Checked;
            ImageBox1.Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonZoomOut_Click(object sender, EventArgs e)
        {
            if (ImageBox1.ZoomFactor > ZOOM_MIN_FACTOR)
            {
                Zoom(-ZOOM_INCREMENT);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonZoomIn_Click(object sender, EventArgs e)
        {
            if (ImageBox1.ZoomFactor < ZOOM_MAX_FACTOR)
            {
                Zoom(+ZOOM_INCREMENT);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        private void Zoom(int value)
        {
            ImageBox1.ZoomFactor += value;
            Project.CharSetForm.ZoomFactor = ImageBox1.ZoomFactor;
            StatusLabel3.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);

            // Only do this on the set forms
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.X >= 0 && e.X < ImageBox1.ClientSize.Width && e.Y >= 0 && e.Y < ImageBox1.ClientSize.Height)
            if (e.X >= 0 && e.X < ImageBox1.ImageRect.Width && e.Y >= 0 && e.Y < ImageBox1.ImageRect.Height)
            {
                int xPos = ImageBox1.ScrollX(e.X);
                int yPos = ImageBox1.ScrollY(e.Y);

                int tile = ((yPos * ImageBox1.ImageSize.Width) + xPos);

                if (tile < Project.Chars.Count)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        Project.Chars.SelectedItem = tile;
                        ImageBox1.Refresh();
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        Project.Chars.SelectedItemTile = tile;
                        ImageBox1.Refresh();
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.X >= 0 && e.X < ImageBox1.ClientSize.Width && e.Y >= 0 && e.Y < ImageBox1.ClientSize.Height)
            if (e.X >= 0 && e.X < ImageBox1.ImageRect.Width && e.Y >= 0 && e.Y < ImageBox1.ImageRect.Height)
            {
                int xPos = ImageBox1.ScrollX(e.X);
                int yPos = ImageBox1.ScrollY(e.Y);

                int TileXY = ((yPos * ImageBox1.ImageSize.Width) + xPos);

                if (TileXY < Project.Chars.Count)
                {
                    StatusLabel1.Text = string.Format("Char {0} (${0:X2})", TileXY);
                }
                else
                {
                    StatusLabel1.Text = "Ready";
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox1_MouseUp(object sender, MouseEventArgs e)
        {
            CharSet_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (ImageBox1.ZoomFactor > ZOOM_MIN_FACTOR)
                {
                    Zoom(-ZOOM_INCREMENT);
                }
            }
            else
            {
                if (ImageBox1.ZoomFactor < ZOOM_MAX_FACTOR)
                {
                    Zoom(+ZOOM_INCREMENT);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox1_Paint(object sender, PaintEventArgs e)
        {
            ImageBox1.PaintSelector(Project.Chars.SelectedItem, green, e.Graphics);
            ImageBox1.PaintSelector(Project.Chars.SelectedItemTile, purple, e.Graphics);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripNumericTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int qty = toolStripNumericTextBox1.Text.ToInteger();

                if (qty != Project.Chars.Count)
                {
                    Project.AddHistory(string.Format("Character Qty Resized to {0}", toolStripNumericTextBox1.Text));
                    Project.Chars.Resize(qty);
                    Project.Maps.Items[0].ValidateChars(Project.Chars.Count);

                    StatusLabel2.Text = string.Format("{0} Bytes", Project.Chars.Count * 64);

                    CharSet_ImageChanged?.Invoke(this, e);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCompress_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Compress Character");
            Project.CompressData(Project.Chars);
            CharSet_ImageChanged?.Invoke(this, e);
        }
    }
}