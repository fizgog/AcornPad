using AcornPad.Common;
using AcornPad.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class ImportImage : Form
    {
        private const int ZOOM_INCREMENT = 1;
        private const int ZOOM_MIN_FACTOR = 1;
        private const int ZOOM_MAX_FACTOR = 10;

        private System.Threading.Thread Worker;

        /// <summary>
        ///
        /// </summary>
        public AcornProject Project;

        /// <summary>
        ///
        /// </summary>
        private List<Machine> machineList;

        private Palette palette { get; set; }

        /// <summary>
        ///
        /// </summary>
        private Machine machine;

        /// <summary>
        ///
        /// </summary>
        private MachineType GetMachineType => (machine != null && machine.MachineType == "Acorn Atom") ? MachineType.Atom : MachineType.BBC;

        public ImportImage()
        {
            InitializeComponent();

            Project = null;

            ZoomLabel1.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);
            ZoomLabel2.Text = string.Format("Zoom x{0}", ImageBox2.ZoomFactor);
        }

        private void ImportImage_Load(object sender, EventArgs e)
        {
            // Load json file into machine array
            using (StreamReader r = new StreamReader("Machine.json"))
            {
                string json = r.ReadToEnd();
                machineList = JsonConvert.DeserializeObject<List<Machine>>(json);
            }

            // Get all machine types
            List<string> distinctList = machineList.Select(x => x.MachineType).Distinct().ToList();

            // Populate Machine combobox with machine types
            foreach (var itm in distinctList)
            {
                ComboBoxMachine.Items.Add(itm);
            }

            ComboBoxMachine.SelectedIndex = 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxGfxMode.Items.Clear();

            foreach (var itm in machineList)
            {
                if (itm.MachineType == ComboBoxMachine.SelectedItem.ToString())
                    ComboBoxGfxMode.Items.Add(itm.ShortDesc);
            }

            ComboBoxGfxMode.SelectedIndex = 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxGfxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var itm in machineList)
            {
                if (itm.ShortDesc == ComboBoxGfxMode.SelectedItem.ToString())
                {
                    machine = itm;
                    return;
                }
            }
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
                ImageBox1.ZoomFactor -= ZOOM_INCREMENT;
                ZoomLabel1.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);
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
                ImageBox1.ZoomFactor += ZOOM_INCREMENT;
                ZoomLabel1.Text = string.Format("Zoom x{0}", ImageBox1.ZoomFactor);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open Image File",
                Filter = Helper.GetImageFilter(),
                DefaultExt = "png"
            };

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                ImageBox1.Load(openFileDialog.FileName);
                InfoLabel1.Text = string.Format("PixelFormat: {0}", ImageBox1.PixelFormatString);
                InfoLabel2.Text = string.Format("Size: {0}x{1} pixels", ImageBox1.Image.Width, ImageBox1.Image.Height);

                ImageBox2.Image = null;
                ImageBox2.Invalidate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonConvert_Click(object sender, EventArgs e)
        {
            ButtonLoad.Enabled = false;
            ButtonConvert.Enabled = false;
            ButtonOK.Enabled = false;
            ButtonCancel.Enabled = false;

            statusStrip1.InProgress(true);
      
            Worker = new Thread(() => GenerateImage());
            Worker.Start();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            // Generate Project from image
            if (ImageBox2.Image != null)
            {
                ButtonLoad.Enabled = false;
                ButtonConvert.Enabled = false;
                ButtonOK.Enabled = false;
                ButtonCancel.Enabled = false;

                statusStrip1.InProgress(true);
                
                Worker = new Thread(() => GenerateProject());
                Worker.Start();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ImageBox2.ShowGrid = toolStripButton1.Checked;
            ImageBox2.Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            if (ImageBox2.ZoomFactor > ZOOM_MIN_FACTOR)
            {
                ImageBox2.ZoomFactor -= ZOOM_INCREMENT;
                ZoomLabel2.Text = string.Format("Zoom x{0}", ImageBox2.ZoomFactor);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            if (ImageBox2.ZoomFactor < ZOOM_MAX_FACTOR)
            {
                ImageBox2.ZoomFactor += ZOOM_INCREMENT;
                ZoomLabel2.Text = string.Format("Zoom x{0}", ImageBox2.ZoomFactor);
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void GenerateImage()
        {
            if (ImageBox1.Image != null)
            {
                int numColours = machine.NumColours;

                int conv = 3;

                if (radioButton1.Checked)
                {
                    conv = 1;
                }
                else if (radioButton2.Checked)
                {
                    conv = 2;
                }

                int xOffset = OffsetX.Text.ToInteger();
                int yOffset = OffsetY.Text.ToInteger();

                ImageBox2.PixelSize = machine.PixelSize;

                palette = new Palette(GetMachineType, numColours);

                // Auto Palette
                if (CheckBoxAutoPalette.Checked && numColours < 16)
                {
                    // Count colours for modes < 16
                    Palette pal = new Palette(GetMachineType);
                    int[] colCount = ImageBox1.Image.CountPalette(pal, conv, xOffset, yOffset);

                    // Order indices
                    var colOrder = Enumerable.Range(0, colCount.Length).OrderByDescending(i => colCount[i]).ToList();

                    // Create new palette from colours
                    for (int i = 0; i < numColours; i++)
                    {
                        palette.GetAcornColourSet[i] = (Common.AcornColourType)colOrder[i];
                        Color col = palette.GetWindowsColour(palette.GetAcornColourSet[i]);
                        palette.WinColours[i] = col;
                    }
                }

                // Convert to Acorn Image
                ImageBox2.Image = ImageBox1.Image.ImageToAcorn(palette, conv, xOffset, yOffset);

                // Resize Image ?
                if (CheckBoxResize.Checked)
                {
                    ImageBox2.Image = ImageBox2.Image.ResizeImage(new Size(machine.Width, machine.Height));
                }

                InfoLabel3.Text = string.Format("PixelFormat: {0}", machine.ShortDesc);
                InfoLabel4.Text = string.Format("Size: {0}x{1} pixels", ImageBox2.Image.Width, ImageBox2.Image.Height);

                ImageBox2.ImageSize = new Size(ImageBox2.Image.Width, ImageBox2.Image.Height);
            }

            BeginInvoke(new Action(() => ThreadConvertComplete()));
        }

        /// <summary>
        ///
        /// </summary>
        private void GenerateProject()
        {
            Project = new AcornProject(GetMachineType)
            {
                Machine = machine,
                Palette = palette
            };

            Project = ImageBox2.CreateProject(Project);

            Project.NumberOfMaps = 1;

            // Compress the data
            Project.CompressData(Project.Chars, Project.TilesOnline);

            BeginInvoke(new Action(() => ThreadCreateComplete()));
        }

        /// <summary>
        /// 
        /// </summary>
        private void ThreadConvertComplete()
        {
            statusStrip1.InProgress(false);
            ButtonLoad.Enabled = true;
            ButtonConvert.Enabled = true;
            ButtonOK.Enabled = true;
            ButtonCancel.Enabled = true;

            ImageBox2.Invalidate();
        }

        private void ThreadCreateComplete()
        {
            statusStrip1.InProgress(false);
            ButtonLoad.Enabled = true;
            ButtonConvert.Enabled = true;
            ButtonOK.Enabled = true;
            ButtonCancel.Enabled = true;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}