using System;
using System.Linq;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class TileEdit : Form
    {
        private const int ZOOM_INCREMENT = 1;
        private const int ZOOM_MIN_FACTOR = 4;
        private const int ZOOM_MAX_FACTOR = 20;

        private readonly AcornProject Project;

        private ToolStripButton[] PaintTools;

        public event EventHandler TileEdit_ImageChanged;

        public TileEdit(AcornProject project)
        {
            InitializeComponent();
            Project = project;

            Location = Project.TileEditForm.Location;
            Size = Project.TileEditForm.Size;

            ImageBox1.ZoomFactor = Project.TileEditForm.ZoomFactor;
            ImageBox1.PixelSize = Project.Machine.PixelSize;
            ImageBox1.ImageSize = new System.Drawing.Size(Project.Tiles.Width, Project.Tiles.Height);
            ImageBox1.ShowTileGrid = true;
            ImageBox1.GridSize = new System.Drawing.Size(1 * ImageBox1.PixelSize, 1);
            ImageBox1.GridTileSize = new System.Drawing.Size(Project.Chars.Width * ImageBox1.PixelSize, Project.Chars.Height);
            ImageBox1.MouseWheel += ImageBox1_MouseWheel;

            StatusLabel1.Text = "Ready";
            StatusLabel2.Text = "";
            StatusLabel3.Text = string.Format("Tile {0} (${0:X2})", Project.Tiles.SelectedItem);
            StatusLabel4.Text = string.Format("Used {0}", Project.Usage(Project.Tiles));
            StatusLabel5.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileEdit_Load(object sender, EventArgs e)
        {
            PaintTools = new ToolStripButton[]
            {
                ButtonPen,
                ButtonBrush,
                ButtonFloodFill,
                ButtonPicker
            };

            foreach (ToolStripButton itm in PaintTools)
            {
                itm.Click += Tools_Click;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileEdit_FormClosing(object sender, FormClosingEventArgs e)
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
        private void Tools_Click(object sender, EventArgs e)
        {
            SelectToolStripButton(sender as ToolStripButton, PaintTools);
            ImageBox1.PaintTool = GetPaintTool();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="selectedButton"></param>
        /// <param name="buttons"></param>
        private void SelectToolStripButton(ToolStripButton selectedButton, ToolStripButton[] buttons)
        {
            foreach (ToolStripButton itm in buttons)
            {
                itm.Checked = (itm == selectedButton);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private int GetPaintTool()
        {
            for (int i = 0; i < PaintTools.Length; i++)
            {
                if (PaintTools[i].Checked) return i;
            }
            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileEdit_Shown(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileEdit_Move(object sender, EventArgs e)
        {
            Project.TileEditForm.Location = Location;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileEdit_Resize(object sender, EventArgs e)
        {
            Project.TileEditForm.Size = Size;
        }

        /// <summary>
        ///
        /// </summary>
        public new void Invalidate()
        {
            if (Project != null && Project.Tiles != null)
            {
                int width = Project.Tiles.Width;
                int height = Project.Tiles.Height;

                ImageBox1.PixelSize = Project.Machine.PixelSize;
                ImageBox1.ImageSize = new System.Drawing.Size(width, height);
                ImageBox1.GridSize = new System.Drawing.Size(1 * ImageBox1.PixelSize, 1);
                ImageBox1.GridTileSize = new System.Drawing.Size(Project.Chars.Width * ImageBox1.PixelSize, Project.Chars.Height);

                ImageBox1.DrawBitmapTile(Project);
                StatusLabel3.Text = string.Format("Tile {0} (${0:X2})", Project.Tiles.SelectedItem);
                StatusLabel4.Text = string.Format("Used {0}", Project.Usage(Project.Tiles));
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
            ImageBox1.ShowTileGrid = ButtonGrid.Checked;
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
            Project.TileEditForm.ZoomFactor = ImageBox1.ZoomFactor;
            StatusLabel5.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X >= 0 && e.X < ImageBox1.ImageRect.Width && e.Y >= 0 && e.Y < ImageBox1.ImageRect.Height)
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                {
                    int xPos = ImageBox1.ScrollTileX(e.X);
                    int yPos = ImageBox1.ScrollTileY(e.Y);

                    int index = Project.Tiles.SelectedItem;
                    int value = (e.Button == MouseButtons.Left ? Project.Chars.SelectedItem : Project.Chars.SelectedItemTile);

                    switch (ImageBox1.PaintTool)
                    {
                        case 0:
                            Project.AddHistory("Paint Tile with Pencil");
                            Project.Tiles.Items[index].Pencil(xPos, yPos, value);
                            break;

                        case 1:
                            Project.AddHistory("Paint Tile with Brush");
                            Project.Tiles.Items[index].Brush(xPos, yPos, value);
                            break;

                        case 2:
                            Project.AddHistory("Paint Tile with Flood Fill");
                            Project.Tiles.Items[index].FloodFill(xPos, yPos, value);
                            break;

                        case 3:
                            int charValue = Project.Tiles.Items[index].GetCellValue(xPos, yPos);
                            Project.Chars.SelectedItem = charValue;
                            break;

                        default: throw new Exception("Invalid paint tool.");
                    }

                    Invalidate();
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
            if (e.X >= 0 && e.X < ImageBox1.ImageRect.Width && e.Y >= 0 && e.Y < ImageBox1.ImageRect.Height)
            {
                int xPos = ImageBox1.ScrollTileX(e.X);
                int yPos = ImageBox1.ScrollTileY(e.Y);

                int CharXY = Project.Tiles.Items[Project.Tiles.SelectedItem].GetCellValue(xPos, yPos);

                StatusLabel1.Text = string.Format("({0},{1})", xPos, yPos);
                StatusLabel2.Text = (CharXY != -1) ? string.Format("Char {0} (${0:X2})", CharXY) : "";

                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                {
                    int index = Project.Tiles.SelectedItem;
                    int value = (e.Button == MouseButtons.Left ? Project.Chars.SelectedItem : Project.Chars.SelectedItemTile);

                    switch (ImageBox1.PaintTool)
                    {
                        case 0:
                            Project.Tiles.Items[index].Pencil(xPos, yPos, value);
                            break;

                        case 1:
                            Project.Tiles.Items[index].Brush(xPos, yPos, value);
                            break;

                        case 2:
                            // Do nothing
                            break;

                        case 3:
                            // Do nothing
                            break;

                        default: throw new Exception("Invalid paint tool.");
                    }

                    Invalidate();
                }
            }
            else
            {
                StatusLabel1.Text = "Ready";
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox1_MouseUp(object sender, MouseEventArgs e)
        {
            TileEdit_ImageChanged(this, e);
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
        /// Transform: Flip Horizontally
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlipHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Flip Tile Horizontal");
            Project.Tiles.Flip(Common.FlipType.FlipX);
            TileEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Flip Vertically
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlipVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Flip Tile Vertical");
            Project.Tiles.Flip(Common.FlipType.FlipY);
            TileEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Shift Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Tile Left");
            Project.Tiles.Shift(Common.ShiftType.ShiftLeft);
            TileEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Shift Right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Tile Right");
            Project.Tiles.Shift(Common.ShiftType.ShiftRight);
            TileEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Shift Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Tile Up");
            Project.Tiles.Shift(Common.ShiftType.ShiftUp);
            TileEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Shift Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Tile Down");
            Project.Tiles.Shift(Common.ShiftType.ShiftDown);
            TileEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Colour Negative
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NegativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Tile Negative");
            int savedSelectedItem = Project.Chars.SelectedItem;

            int[] tileData = Project.Tiles.Items[Project.Tiles.SelectedItem].Data.Distinct().ToArray();

            foreach (var selectedTem in tileData)
            {
                Project.Chars.SelectedItem = selectedTem;
                Project.Chars.Negative(Project.Palette.NumColours);
            }

            Project.Chars.SelectedItem = savedSelectedItem;
            TileEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplaceColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceColour frm = new ReplaceColour(Project);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.OldColour != frm.NewColour)
                {
                    Project.AddHistory("Tile Replace Colour");

                    int[] tileData = Project.Tiles.Items[Project.Tiles.SelectedItem].Data.Distinct().ToArray();

                    foreach (var selectedItem in tileData)
                    {
                        Project.Chars.Negative(Project.Palette.NumColours);
                        Project.Chars.Items[selectedItem].ReplaceColour(frm.OldColour, frm.NewColour);
                    }

                    TileEdit_ImageChanged?.Invoke(this, e);
                }
            }
        }
    }
}