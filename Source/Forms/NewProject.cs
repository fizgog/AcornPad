using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class NewProject : Form
    {
        /// <summary>
        ///
        /// </summary>
        private Machine machine;

        /// <summary>
        ///
        /// </summary>
        public Machine GetMachine => machine;

        /// <summary>
        ///
        /// </summary>
        private List<Machine> machineList;

        /// <summary>
        ///
        /// </summary>
        public NewProject()
        {
            InitializeComponent();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewProject_Load(object sender, EventArgs e)
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
        /// Populate Machine combobox with machine types
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxGfxMode.Items.Clear();

            foreach (var itm in machineList)
            {
                if (itm.MachineType == ComboBoxMachine.SelectedItem.ToString())
                    ComboBoxGfxMode.Items.Add(itm.Description);
            }

            ComboBoxGfxMode.SelectedIndex = 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            foreach (var itm in machineList)
            {
                if (itm.Description == ComboBoxGfxMode.SelectedItem.ToString())
                {
                    machine = itm;
                    DialogResult = DialogResult.OK;
                    Close();
                }
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
    }
}