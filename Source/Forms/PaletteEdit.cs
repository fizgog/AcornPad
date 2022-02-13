using System;
using System.Windows.Forms;

namespace AcornPad
{
    public partial class PaletteEdit : Form
    {
        private readonly AcornProject Project;
        //private readonly List<Machine> machineList;

        public event EventHandler PaletteChanged;

        public event EventHandler TilesOnlineChanged;

        public event EventHandler MultipleMapChanged;

        /// <summary>
        ///
        /// </summary>
        /// <param name="project"></param>
        /// <param name="mList"></param>
        public PaletteEdit(AcornProject project)
        {
            InitializeComponent();
            Project = project;

            Location = Project.PaletteForm.Location;
            Size = Project.PaletteForm.Size;

            //machineList = Project.Machine;

            //foreach (var itm in machineList)
            //{
            //    comboBox2.Items.Add(itm.Description);
            //}
            comboBox2.Items.Add(Project.Machine.Description);

            beebPalette1.MachineType = (Project.Machine.MachineType == "Acorn Atom") ? Common.MachineType.Atom : Common.MachineType.BBC;

            beebPalette1.Palette = Project.Palette;
            beebPalette1.SetColourMode(false, Project.Palette.ColourSet);
            //beebPalette1.AcornPalette.NumColours = Project.Machine.NumColours;

            comboBox2.SelectedIndex = comboBox2.FindString(Project.Machine.Description);

            TileComboBox.SelectedIndex = Project.TilesOnline == true ? 0 : 1;
            MapsComboBox.SelectedIndex = Project.MultipleMaps == true ? 0 : 1;
            MapNumericUpDown.Value = Project.NumberOfMaps == 0 ? 1 : Project.NumberOfMaps;

            // TODO
            //TileComboBox.Enabled = false;
            MapsComboBox.Enabled = false;
            MapNumericUpDown.Enabled = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Palette_FormClosing(object sender, FormClosingEventArgs e)
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
        private void Palette_Move(object sender, EventArgs e)
        {
            Project.PaletteForm.Location = Location;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Palette_Resize(object sender, EventArgs e)
        {
            Project.PaletteForm.Size = Size;
        }

        /// <summary>
        ///
        /// </summary>
        public new void Invalidate()
        {
            // Colours????

            TileComboBox.SelectedIndex = Project.TilesOnline == true ? 0 : 1;
            MapsComboBox.SelectedIndex = Project.MultipleMaps == true ? 0 : 1;
            MapNumericUpDown.Value = Project.NumberOfMaps == 0 ? 1 : Project.NumberOfMaps;

            base.Invalidate();
        }

        /// <summary>
        /// Colour Changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeebPalette1_ColourChanged(object sender, EventArgs e)
        {
            Project.Palette.AcornColourSet1 = beebPalette1.Palette.AcornColourSet1;
            Project.Palette.AcornColourSet2 = beebPalette1.Palette.AcornColourSet2;

            Project.Palette.WinColours = beebPalette1.Palette.WinColours;
            Project.Palette.DrawColour = beebPalette1.Palette.DrawColour;
            Project.Palette.EraseColour = beebPalette1.Palette.EraseColour;

            PaletteChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //beebPalette1.AcornPalette.NumColours = machineList[comboBox2.SelectedIndex].NumColours;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Project.TilesOnline != (TileComboBox.SelectedIndex == 0))
            {
                Project.TilesOnline = TileComboBox.SelectedIndex == 0;
                TilesOnlineChanged?.Invoke(this, e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Project.MultipleMaps = MapsComboBox.SelectedIndex == 0;
            MultipleMapChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Project.NumberOfMaps = MapNumericUpDown.Value.ToInteger();
            MultipleMapChanged?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripTable1_TableControl_Cancelled(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripTable1_TableControl_Selected(object sender, WindowsFormsApp1.EventArgs.TableEventArgs e)
        {
            
        }
    }
}