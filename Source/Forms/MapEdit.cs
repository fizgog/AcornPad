using System;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class MapEdit : Form
    {
        private const int ZOOM_INCREMENT = 1;
        private const int ZOOM_MIN_FACTOR = 1;
        private const int ZOOM_MAX_FACTOR = 10;

        private readonly AcornProject Project;

        private ToolStripButton[] PaintTools;

        public event EventHandler MapEdit_ImageChanged;

        private int crossX;
        private int crossY;

        public MapEdit(AcornProject project)
        {
            InitializeComponent();
            Project = project;

            Location = Project.MapForm.Location;
            Size = Project.MapForm.Size;

            toolStripNumericTextBox1.Text = Project.Maps.Width.ToString();
            toolStripNumericTextBox2.Text = Project.Maps.Height.ToString();
            toolStripNumericTextBox3.Text = Project.NumberOfMaps.ToString();

            ImageBox1.PixelSize = Project.Machine.PixelSize;
            ImageBox1.ImageSize = new System.Drawing.Size(Project.Machine.TextWidth, Project.Machine.TextHeight);
            ImageBox1.GridSize = new System.Drawing.Size(Project.Chars.Width * ImageBox1.PixelSize, Project.Chars.Height);
            ImageBox1.ZoomFactor = Project.MapForm.ZoomFactor;
            ImageBox1.MouseWheel += ImageBox1_MouseWheel;

            StatusLabel1.Text = "Ready";
            StatusLabel2.Text = "";
            StatusLabel3.Text = string.Format("{0} Cells", Project.Maps.Count);
            StatusLabel4.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);

            if (Project.MultipleMaps == false)
            {
                toolStripNumericTextBox3.Enabled = false;
            }
        }

        /// <summary>
        /// Initialise Paint Tools
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapEdit_Load(object sender, EventArgs e)
        {
            PaintTools = new ToolStripButton[]
            {
                ButtonPen,
                ButtonBrush,
                ButtonFloodFill,
                ButtonPicker,
                ButtonIncrease,
                ButtonDecrease
            };

            foreach (ToolStripButton itm in PaintTools)
            {
                itm.Click += Tools_Click;
            }
        }

        /// <summary>
        /// Don't close the form but hide it instead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapEdit_FormClosing(object sender, FormClosingEventArgs e)
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
            Refresh();
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
        private void MapEdit_Shown(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Move form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapEdit_Move(object sender, EventArgs e)
        {
            Project.MapForm.Location = Location;
        }

        /// <summary>
        /// Resize form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapEdit_Resize(object sender, EventArgs e)
        {
            Project.MapForm.Size = Size;
        }

        /// <summary>
        /// Invalidate Map
        /// </summary>
        public new void Invalidate()
        {
            if (Project != null && Project.Maps != null)
            {
                int width = Project.Maps.Width;
                int height = Project.Maps.Height;

                toolStripNumericTextBox1.Text = width.ToString();
                toolStripNumericTextBox2.Text = height.ToString();

                ImageBox1.PixelSize = Project.Machine.PixelSize;
                ImageBox1.ImageSize = new System.Drawing.Size(width, height);
                ImageBox1.GridSize = new System.Drawing.Size(Project.Chars.Width * ImageBox1.PixelSize, Project.Chars.Height);

                if (Project.TilesOnline)
                {
                    ButtonPicker.ToolTipText = "Tile Picker";

                    width = Project.Tiles.Width * Project.Chars.Width;
                    height = Project.Tiles.Height * Project.Chars.Height;
                    ImageBox1.CellSize = new System.Drawing.Size(width, height);
                    ImageBox1.GridSize = new System.Drawing.Size(width * ImageBox1.PixelSize, height);
                    ImageBox1.DrawBitmapTileMap(Project);
                }
                else
                {
                    width = Project.Chars.Width;
                    height = Project.Chars.Height;
                    ImageBox1.CellSize = new System.Drawing.Size(width, height);
                    ImageBox1.GridSize = new System.Drawing.Size(width * ImageBox1.PixelSize, height);

                    ImageBox1.DrawBitmapMap(Project);
                }

                StatusLabel3.Text = string.Format("{0} Cells", Project.Maps.Count);
            }

            base.Invalidate();
        }

        /// <summary>
        /// Show / Hide grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGrid_Click(object sender, EventArgs e)
        {
            ImageBox1.ShowGrid = ButtonGrid.Checked;
            ImageBox1.Invalidate();
        }

        /// <summary>
        /// Zoom out of Map
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
        /// Zoom in to Map
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
        /// Zoom in or out of map
        /// </summary>
        /// <param name="value"></param>
        private void Zoom(int value)
        {
            ImageBox1.ZoomFactor += value;
            Project.MapForm.ZoomFactor = ImageBox1.ZoomFactor;
            StatusLabel4.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);
        }

        /// <summary>
        /// Get selected character or tile item
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private int GetItem(MouseButtons e)
        {
            switch (e)
            {
                case MouseButtons.Left:
                    return Project.TilesOnline ? Project.Tiles.SelectedItem : Project.Chars.SelectedItem;

                default:
                    return Project.TilesOnline ? Project.Tiles.SelectedItemTile : Project.Chars.SelectedItemTile;
            }
        }

        /// <summary>
        /// Mouse Button Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X >= 0 && e.X < ImageBox1.ClientSize.Width && e.Y >= 0 && e.Y < ImageBox1.ClientSize.Height)
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                {
                    int xPos = ImageBox1.ScrollX(e.X);
                    int yPos = ImageBox1.ScrollY(e.Y);

                    int index = Project.Maps.SelectedItem;
                    int oldValue = Project.Maps.Items[index].GetCellValue(xPos, yPos);

                    int newValue = GetItem(e.Button);

                    switch (ImageBox1.PaintTool)
                    {
                        case 0: // Pencil
                            if (newValue != oldValue)
                            {
                                Project.AddHistory("Paint Map with Pencil");
                                Project.Maps.Items[0].Pencil(xPos, yPos, newValue);
                            }
                            break;

                        case 1: // Brush
                            if (newValue != oldValue)
                            {
                                Project.AddHistory("Paint Map with Brush");
                                Project.Maps.Items[0].Brush(xPos, yPos, newValue);
                            }
                            break;

                        case 2: // Flood fill
                            if (newValue != oldValue)
                            {
                                Project.AddHistory("Paint Map with Flood Fill");
                                Project.Maps.Items[0].FloodFill(xPos, yPos, newValue);
                            }
                            break;

                        case 3: // Picker
                            if (Project.TilesOnline)
                            {
                                Project.Tiles.SelectedItem = oldValue;
                            }
                            else
                            {
                                Project.Chars.SelectedItem = oldValue;
                            }
                            break;

                        case 4: // Increase
                            if (e.Button == MouseButtons.Left)
                            {
                                Project.AddHistory("Insert Row");
                                Project.Maps.Items[0].InsertRow(yPos);
                            }
                            else
                            {
                                Project.AddHistory("Insert Column");
                                Project.Maps.Items[0].InsertColumn(xPos);
                            }
                            break;

                        case 5: // Decrease
                            if (e.Button == MouseButtons.Left)
                            {
                                Project.AddHistory("Delete Row");
                                Project.Maps.Items[0].DeleteRow(yPos);
                            }
                            else
                            {
                                Project.AddHistory("Delete Column");
                                Project.Maps.Items[0].DeleteColumn(xPos);
                            }
                            break;

                        default:
                            throw new Exception("Invalid paint tool.");
                    }

                    Invalidate();
               
                }
            }
        }

        /// <summary>
        /// Mouse Moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= 0 && e.X < ImageBox1.ImageRect.Width && e.Y >= 0 && e.Y < ImageBox1.ImageRect.Height)
            {
                int xPos = ImageBox1.ScrollX(e.X);
                int yPos = ImageBox1.ScrollY(e.Y);

                StatusLabel1.Text = string.Format("({0},{1})", xPos, yPos);

                int TileXY = Project.Maps.Items[0].GetCellValue(xPos, yPos);

                StatusLabel2.Text = (TileXY != -1) ? string.Format("{0} {1} (${1:X2})", Project.TilesOnline ? "Tile" : "Char", TileXY) : "";

                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                {
                    int index = Project.Maps.SelectedItem;
                    int oldValue = Project.Maps.Items[index].GetCellValue(xPos, yPos);
                    int newValue = GetItem(e.Button);

                    if (newValue != oldValue)
                    {
                        switch (ImageBox1.PaintTool)
                        {
                            case 0: // Pencil
                                Project.Maps.Items[0].Pencil(xPos, yPos, newValue);
                                break;

                            case 1: // Brush
                                Project.Maps.Items[0].Brush(xPos, yPos, newValue);
                                break;

                            case 2: // Flood fill
                            case 3: // Picker
                            case 4: // Increase row or column
                            case 5: // Decrease row or column
                                break;

                            default: throw new Exception("Invalid paint tool.");
                        }

                        Invalidate();
                    }
                }
                else
                {
                    if (ImageBox1.PaintTool == 4 || ImageBox1.PaintTool == 5)
                    {
                        crossX = xPos;
                        crossY = yPos;
                        Refresh();
                    }
                }
            }
            else
            {
                StatusLabel1.Text = "Ready";
                StatusLabel2.Text = "";
            }
        }

        /// <summary>
        /// Mouse Button Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MapEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Zoom in and out using mouse wheel
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
        /// Map Width changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripNumericTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ResizeMap(e);
            }
        }

        /// <summary>
        /// Map Height changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripNumericTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ResizeMap(e);
            }
        }

        /// <summary>
        /// Resize Map
        /// </summary>
        private void ResizeMap(KeyEventArgs e)
        {
            int width = toolStripNumericTextBox1.Text.ToInteger();
            int height = toolStripNumericTextBox2.Text.ToInteger();

            if (width != Project.Maps.Width || height != Project.Maps.Height)
            {
                Project.AddHistory(string.Format("Resize Map to {0}x{1}", width.ToString(), height.ToString()));

                ImageBox1.ImageSize = new System.Drawing.Size(width, height);

                Project.Maps.Items[0].Resize(width, height);
                StatusLabel3.Text = string.Format("{0} Cells", Project.Maps.Count);
                MapEdit_ImageChanged?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Transform: Rotate 180
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rotate180ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Rotate Map 180°");
            Project.Maps.Rotate(Common.RotateType.Rotate180);
            MapEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Rotate 90 Clockwise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rotate90ClockwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Rotate Map 90° Clockwise");
            Project.Maps.Rotate(Common.RotateType.Rotate90);
            MapEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Rotate 90 Counter Clockwise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rotate90CounterClockwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Rotate Map 90° Counter Clockwise");
            Project.Maps.Rotate(Common.RotateType.Rotate270);
            MapEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Flip Horizontally
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlipHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Flip Map Horizontal");
            Project.Maps.Flip(Common.FlipType.FlipX);
            MapEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Flip Vertically
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlipVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Flip Map Vertical");
            Project.Maps.Flip(Common.FlipType.FlipY);
            MapEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Shift Left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Map Left");
            Project.Maps.Shift(Common.ShiftType.ShiftLeft);
            MapEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Shift Right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Map Right");
            Project.Maps.Shift(Common.ShiftType.ShiftRight);
            MapEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Shift Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Map Up");
            Project.Maps.Shift(Common.ShiftType.ShiftUp);
            MapEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Transform: Shift Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Map Down");
            Project.Maps.Shift(Common.ShiftType.ShiftDown);
            MapEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox1_Paint(object sender, PaintEventArgs e)
        {
            if (ImageBox1.PaintTool == 4 || ImageBox1.PaintTool == 5)
            {
                ImageBox1.PaintCrossSelector(crossX, crossY, e.Graphics);
            }
        }
    }
}