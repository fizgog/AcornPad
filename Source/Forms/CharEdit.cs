using System;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class CharEdit : Form
    {
        private const int ZOOM_INCREMENT = 4;
        private const int ZOOM_MIN_FACTOR = 8;
        private const int ZOOM_MAX_FACTOR = 64;

        private readonly AcornProject Project;

        private ToolStripButton[] PaintTools;

        public event EventHandler CharEdit_ImageChanged;

        public CharEdit(AcornProject project)
        {
            InitializeComponent();
            Project = project;

            Location = Project.CharEditForm.Location;
            Size = Project.CharEditForm.Size;

            ImageBox1.ZoomFactor = Project.CharEditForm.ZoomFactor;
            ImageBox1.PixelSize = Project.Machine.PixelSize;
            ImageBox1.ImageSize = new System.Drawing.Size(1, 1);
            ImageBox1.GridSize = new System.Drawing.Size(1 * ImageBox1.PixelSize, 1);
            ImageBox1.MouseWheel += ImageBox1_MouseWheel;

            StatusLabel1.Text = "Ready";
            StatusLabel2.Text = string.Format("Char {0} (${0:X2})", Project.Chars.SelectedItem);
            StatusLabel3.Text = string.Format("Used {0}", Project.Usage(Project.Chars));
            StatusLabel4.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharEdit_Load(object sender, EventArgs e)
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
        private void CharEdit_FormClosing(object sender, FormClosingEventArgs e)
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
        private void CharEdit_Shown(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharEdit_Move(object sender, EventArgs e)
        {
            Project.CharEditForm.Location = Location;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharEdit_Resize(object sender, EventArgs e)
        {
            Project.CharEditForm.Size = Size;
        }

        /// <summary>
        ///
        /// </summary>
        public new void Invalidate()
        {
            if (Project != null && Project.Chars != null)
            {
                ImageBox1.PixelSize = Project.Machine.PixelSize;
                ImageBox1.GridSize = new System.Drawing.Size(1 * ImageBox1.PixelSize, 1);

                ImageBox1.DrawBitmapChar(Project);
                StatusLabel2.Text = string.Format("Char {0} (${0:X2})", Project.Chars.SelectedItem);
                StatusLabel3.Text = string.Format("Used {0}", Project.Usage(Project.Chars));
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
            Project.CharEditForm.ZoomFactor = ImageBox1.ZoomFactor;
            StatusLabel4.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);
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
                    int xPos = ImageBox1.ScrollX(e.X);
                    int yPos = ImageBox1.ScrollY(e.Y);

                    int index = Project.Chars.SelectedItem;
                    int pixelColour = (e.Button == MouseButtons.Left ? Project.Palette.DrawColour : Project.Palette.EraseColour);

                    switch (ImageBox1.PaintTool)
                    {
                        case 0:
                            Project.AddHistory("Paint Character with Pencil");
                            Project.Chars.Items[index].Pencil(xPos, yPos, pixelColour);
                            break;

                        case 1:
                            Project.AddHistory("Paint Character with Brush");
                            Project.Chars.Items[index].Brush(xPos, yPos, pixelColour);
                            break;

                        case 2:
                            Project.AddHistory("Paint Character with Flood Fill");
                            Project.Chars.Items[index].FloodFill(xPos, yPos, pixelColour);
                            break;

                        case 3: // Colour Picker
                            int picker = Project.Chars.Items[index].GetCellValue(xPos, yPos);
                            
                            if (e.Button == MouseButtons.Left)
                                Project.Palette.DrawColour = picker;
                            else if(e.Button == MouseButtons.Right)
                                Project.Palette.EraseColour = picker;
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
                int xPos = ImageBox1.ScrollX(e.X);
                int yPos = ImageBox1.ScrollY(e.Y);

                StatusLabel1.Text = string.Format("({0},{1})", xPos, yPos);

                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                {
                    int index = Project.Chars.SelectedItem;
                    int pixelColour = (e.Button == MouseButtons.Left ? Project.Palette.DrawColour : Project.Palette.EraseColour);

                    switch (ImageBox1.PaintTool)
                    {
                        case 0:
                            Project.Chars.Items[index].Pencil(xPos, yPos, pixelColour);
                            break;

                        case 1:
                            Project.Chars.Items[index].Brush(xPos, yPos, pixelColour);
                            break;

                        case 2: // FloodFill not required on move
                            break;

                        case 3: // Colour Picker not required on move
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
            CharEdit_ImageChanged?.Invoke(this, e);
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
        private void Rotate180ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Rotate Character 180°");
            Project.Chars.Rotate(Common.RotateType.Rotate180);
            CharEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rotate90ClockwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Rotate Character 90° Clockwise");
            Project.Chars.Rotate(Common.RotateType.Rotate90);
            CharEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rotate90CounterClockwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Rotate Character 90° Counter Clockwise");
            Project.Chars.Rotate(Common.RotateType.Rotate270);
            CharEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlipHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Flip Character Horizontal");
            Project.Chars.Flip(Common.FlipType.FlipX);
            CharEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlipVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Flip Character Vertical");
            Project.Chars.Flip(Common.FlipType.FlipY);
            CharEdit_ImageChanged?.Invoke(this, e);
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
                    Project.AddHistory("Character Replace Colour");
                    Project.Chars.Items[Project.Chars.SelectedItem].ReplaceColour(frm.OldColour, frm.NewColour);
                    CharEdit_ImageChanged?.Invoke(this, e);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NegativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Character Negative");
            Project.Chars.Negative(Project.Palette.NumColours);
            CharEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Character Left");
            Project.Chars.Shift(Common.ShiftType.ShiftLeft);
            CharEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Character Right");
            Project.Chars.Shift(Common.ShiftType.ShiftRight);
            CharEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Character Up");
            Project.Chars.Shift(Common.ShiftType.ShiftUp);
            CharEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiftDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Shift Character Down");
            Project.Chars.Shift(Common.ShiftType.ShiftDown);
            CharEdit_ImageChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharEdit_Leave(object sender, EventArgs e)
        {
            StatusLabel1.Text = "Ready";
            Invalidate();
        }
    }
}