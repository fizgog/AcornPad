using AcornPad.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AcornPad
{
    public partial class PaletteEdit : Form
    {
        private readonly AcornProject Project;
        private readonly List<Machine> MachineList;

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

            MachineList = Sys.GetMachineList(Project.Machine.MachineType);

            foreach (var itm in MachineList)
            {
                ComboBoxGfxMode.Items.Add(itm.Description);
            }
      
            beebPalette1.MachineType = (Project.Machine.MachineType == "Acorn Atom") ? Common.MachineType.Atom : Common.MachineType.BBC;

            beebPalette1.Palette = Project.Palette;
            beebPalette1.SetColourMode(false, Project.Palette.ColourSet);
       
            ComboBoxGfxMode.SelectedIndex = ComboBoxGfxMode.FindString(Project.Machine.Description);

            TileComboBox.SelectedIndex = Project.TilesOnline == true ? 0 : 1;
            MapsComboBox.SelectedIndex = Project.MultipleMaps == true ? 0 : 1;
            MapNumericUpDown.Value = Project.NumberOfMaps == 0 ? 1 : Project.NumberOfMaps;

            // TODO
            MapsComboBox.Enabled = false;
            MapNumericUpDown.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaletteEdit_Load(object sender, EventArgs e)
        {
            
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
            beebPalette1.Palette = Project.Palette;
            beebPalette1.SetColourMode(false, Project.Palette.ColourSet);

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
        private void GfxModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var itm in MachineList)
            {
                if (itm.Description == ComboBoxGfxMode.SelectedItem.ToString())
                {
                    if (Project.Palette.NumColours > itm.NumColours)
                    {
                        Project.ReduceColours(itm.NumColours);
                    }

                    Project.Machine = itm;
                    Project.Palette = new Internal.Palette(Project.MachineType, itm.NumColours);

                    // Copy existing palette over the top of the new one
                    //foreach(var itm in beebPalette1.Palette.)
                    for(int i = 0; i < Project.Palette.NumColours && i < beebPalette1.Palette.NumColours; i++)
                    {
                        Project.Palette.AcornColourSet1[i] = beebPalette1.Palette.AcornColourSet1[i];
                        Project.Palette.AcornColourSet2[i] = beebPalette1.Palette.AcornColourSet2[i];
                        Project.Palette.WinColours[i] = beebPalette1.Palette.WinColours[i];
                    }

                    PaletteChanged?.Invoke(this, e);
                    return;
                }
            }


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