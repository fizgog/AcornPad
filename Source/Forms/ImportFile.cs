using AcornPad.Common;
using AcornPad.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class ImportFile : Form
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
        private byte[] buffer;

        /// <summary>
        ///
        /// </summary>
        private MachineType GetMachineType => (machine != null && machine.MachineType == "Acorn Atom") ? MachineType.Atom : MachineType.BBC;

        public ImportFile()
        {
            InitializeComponent();

            Project = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportFile_Load(object sender, EventArgs e)
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
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            ButtonLoad.Enabled = false;
            ButtonOK.Enabled = false;
            ButtonCancel.Enabled = false;

            statusStrip1.InProgress(true);

            Worker = new Thread(() => GenerateCharSet());
            Worker.Start();
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
        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open File",
                DefaultExt = "*"
            };

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void GenerateCharSet()
        {
            String[] lines = richTextBox1.SplitText();

            if (lines.Length > 0)
            {
                int numColours = machine.NumColours;

                string data = string.Empty;

                foreach (string line in lines)
                {
                    string currentline = line.ToLower().Trim();

                    if (currentline.StartsWith(".byte") || currentline.StartsWith("equb"))
                    {
                        currentline = currentline.Replace(".byte", "").Replace("equb", "").Trim();

                        // Remove any comments from the end
                        int index = currentline.IndexOf(';');
                        if (index > -1)
                        {
                            currentline = currentline.Substring(0, index);
                        }

                        index = currentline.IndexOf('/');
                        if (index > -1)
                        {
                            currentline = currentline.Substring(0, index);
                        }

                        data += currentline + ",";
                    }
                }

                data = data.TrimEnd(',');       // Remove last comma
                data = data.Replace(" ", "");   // Removed any space

                int nChars = data.Split(',').Count() / 8;

                if (nChars > 0)
                {
                    nChars += 1; //Insert blank tile at start

                    int mapWidth = machine.TextWidth;
                    int mapHeight = machine.TextHeight;

                    palette = new Palette(GetMachineType, numColours);

                    Project = new AcornProject(GetMachineType, nChars, 8, 8, 0, 0, 0, 1, mapWidth, mapHeight)
                    {
                        Machine = machine,
                        Palette = palette
                    };

                    int counter = 0;
                    int index = 1;      // index 0 = blank tile

                    int cnt = Project.Chars.Items[0].Count;

                    foreach (string val in data.Split(','))
                    {
                        //string val = value.Trim();

                        int charByte = 0;

                        if (val.StartsWith("$") || val.StartsWith("&"))
                        {
                            charByte = val.FromHex();
                        }
                        else if (val.StartsWith("%"))
                        {
                            charByte = val.FromBinary();
                        }
                        else
                        {
                            charByte = val.ToInteger();
                        }

                        List<int> bits = (Project.MachineType == MachineType.BBC) ? Sys.Unpack_BBC_Byte(machine, charByte) : Sys.Unpack_Atom_Byte(machine, charByte);

                        for (int i = 0; i < bits.Count; i++)
                        {
                            Project.Chars.Items[index].Data[counter + i] = bits[i];
                        }

                        counter += machine.PixelsBerByte;

                        if (counter % cnt == 0)
                        {
                            index++;
                            counter = 0;
                        }
                    }

                    // Compress the data
                    Project.CompressData(Project.Chars, Project.TilesOnline);
                }
            }

            BeginInvoke(new Action(() => ThreadConvertComplete()));
        }

        /// <summary>
        ///
        /// </summary>
        private void ThreadConvertComplete()
        {
            statusStrip1.InProgress(false);
            ButtonLoad.Enabled = true;
            ButtonOK.Enabled = true;
            ButtonCancel.Enabled = true;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}