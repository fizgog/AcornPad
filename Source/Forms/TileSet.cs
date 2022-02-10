using System;
using System.Drawing;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class TileSet : Form
    {
        private const int ZOOM_INCREMENT = 1;
        private const int ZOOM_MIN_FACTOR = 2;
        private const int ZOOM_MAX_FACTOR = 10;

        private Color purple = Color.FromArgb(113, 96, 232);
        private Color green = Color.FromArgb(108, 203, 95);

        private readonly AcornProject Project;

        public event EventHandler TileSet_ImageChanged;

        public TileSet(AcornProject project)
        {
            InitializeComponent();
            Project = project;

            ImageBox1.MouseWheel += ImageBox1_MouseWheel;

            Location = Project.TileSetForm.Location;
            Size = Project.TileSetForm.Size;

            ImageBox1.ZoomFactor = Project.TileSetForm.ZoomFactor;

            toolStripNumericTextBox1.Text = Project.Tiles.Count.ToString();

            ImageBox1.PixelSize = Project.Machine.PixelSize;

            int width = Project.Tiles.Width * Project.Chars.Width;
            int height = Project.Tiles.Height * Project.Chars.Height;
            ImageBox1.CellSize = new System.Drawing.Size(width, height);
            ImageBox1.GridSize = new System.Drawing.Size(width * ImageBox1.PixelSize, height);

            ToolStripTable1.TableControl.SelectedSize = new Size(Project.Tiles.Width, Project.Tiles.Height);
            ToolStripSplitButton1.Text = String.Format("{0} x {1}", ToolStripTable1.TableControl.SelectedSize.Width, ToolStripTable1.TableControl.SelectedSize.Height);

            StatusLabel1.Text = "Ready";
            StatusLabel2.Text = string.Format("{0} Cells", Project.Tiles.TotalBytes);
            StatusLabel3.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileSet_FormClosing(object sender, FormClosingEventArgs e)
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
        private void TileSet_Shown(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileSet_Move(object sender, EventArgs e)
        {
            Project.TileSetForm.Location = Location;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileSet_Resize(object sender, EventArgs e)
        {
            Project.TileSetForm.Size = Size;
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        public new void Invalidate()
        {
            if (Project != null && Project.Tiles != null)
            {
                toolStripNumericTextBox1.Text = Project.Tiles.Count.ToString();

                int width = (ClientRectangle.Width / (Project.Tiles.Width * ImageBox1.PixelSize * Project.Chars.Width) / ImageBox1.ZoomFactor) - 1;

                width = width > 0 ? width : 1;

                int height = (Project.Tiles.Count / width) + 1;

                height = height > 0 ? height : 1;

                ImageBox1.ImageSize = new Size(width, height);

                ImageBox1.DrawBitmapTileSet(Project);
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
            Project.TileSetForm.ZoomFactor = ImageBox1.ZoomFactor;
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
            if (e.X >= 0 && e.X < ImageBox1.ClientSize.Width && e.Y >= 0 && e.Y < ImageBox1.ClientSize.Height)
            {
                int xPos = ImageBox1.ScrollX(e.X);
                int yPos = ImageBox1.ScrollY(e.Y);

                int tile = ((yPos * ImageBox1.ImageSize.Width) + xPos);

                if (tile < Project.Tiles.Count)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        Project.Tiles.SelectedItem = tile;
                        ImageBox1.Refresh();
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        Project.Tiles.SelectedItemTile = tile;
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
            if (e.X >= 0 && e.X < ImageBox1.ClientSize.Width && e.Y >= 0 && e.Y < ImageBox1.ClientSize.Height)
            {
                int xPos = ImageBox1.ScrollX(e.X);
                int yPos = ImageBox1.ScrollY(e.Y);

                int TileXY = ((yPos * ImageBox1.ImageSize.Width) + xPos);

                if (TileXY < Project.Tiles.Count)
                {
                    StatusLabel1.Text = string.Format("Tile {0} (${0:X2})", TileXY);
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
            TileSet_ImageChanged?.Invoke(this, e);
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
            ImageBox1.PaintSelector(Project.Tiles.SelectedItem, green, e.Graphics);
            ImageBox1.PaintSelector(Project.Tiles.SelectedItemTile, purple, e.Graphics);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripTable1_TableControl_Cancelled(object sender, EventArgs e)
        {
            ToolStripSplitButton1.HideDropDown();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripTable1_TableControl_Selected(object sender, WindowsFormsApp1.EventArgs.TableEventArgs e)
        {
            int width = ToolStripTable1.TableControl.SelectedSize.Width;
            int height = ToolStripTable1.TableControl.SelectedSize.Height;

            ToolStripSplitButton1.HideDropDown();

            if (width != Project.Tiles.Width || height != Project.Tiles.Height)
            {
                Project.AddHistory(string.Format("Resize Tiles to {0}x{1}", width.ToString(), height.ToString()));

                ImageBox1.ImageSize = new Size(width, height);
                for (int i = 0; i < Project.Tiles.Count; i++)
                {
                    Project.Tiles.Items[i].Resize(width, height);
                }

                ToolStripSplitButton1.Text = String.Format("{0} x {1}", width, height);
                StatusLabel2.Text = string.Format("{0} Bytes", Project.Tiles.Count * 8);
                TileSet_ImageChanged?.Invoke(this, e);
            }
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

                if (qty != Project.Tiles.Count)
                {
                    Project.AddHistory(string.Format("Tiles Qty Resized to {0}", toolStripNumericTextBox1.Text));
                    Project.Tiles.Resize(qty);
                    Project.Maps.Items[0].ValidateChars(Project.Tiles.Count);

                    StatusLabel2.Text = string.Format("{0} Bytes", Project.Tiles.Count * 64);

                    TileSet_ImageChanged?.Invoke(this, e);
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
            Project.AddHistory("Compress Tiles");
            Project.CompressData(Project.Tiles);
            TileSet_ImageChanged?.Invoke(this, e);
        }
    }
}