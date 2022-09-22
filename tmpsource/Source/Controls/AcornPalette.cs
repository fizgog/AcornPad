using AcornPad.Common;
using AcornPad.Internal;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AcornPad.Controls
{
    [Serializable]
    public partial class AcornPalette : UserControl
    {
        private const int MAX_ATOM_COLOURS = 4;
        private const int MAX_BEEB_COLOURS = 16;

        public event EventHandler ColourChanged;

        private Palette palette;

        [ReadOnly(true)]
        [Browsable(false)]
        public Palette Palette
        {
            get
            {
                return palette;
            }
            set
            {
                palette = value;
                //if (palette != null)
                //{
                //    SetColourMode();
                //}
            }
        }

        private MachineType machineType;

        [ReadOnly(true)]
        [Browsable(false)]
        public MachineType MachineType
        {
            get
            {
                return machineType;
            }
            set
            {
                machineType = value;
                SetMachine();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string DrawLabel
        {
            get { return LabelDrawColour.Text; }
            set { LabelDrawColour.Text = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public string EraseLabel
        {
            get { return LabelEraseColour.Text; }
            set { LabelEraseColour.Text = value;  }
        }


        public void ResetButtons(bool show)
        {
            ButtonReset1.Visible = show;
            ButtonReset2.Visible = show;
        }

        /// <summary>
        ///
        /// </summary>
        public AcornPalette()
        {
            InitializeComponent();

            RadioButton1.Location = new Point(121, 86);
            RadioButton2.Location = new Point(121, 114);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeebPalette_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///
        /// </summary>
        private void SetMachine()
        {
            Palette = new Palette(MachineType, MachineType == MachineType.Atom ? MAX_ATOM_COLOURS : MAX_BEEB_COLOURS);

            if (MachineType == MachineType.Atom)
            {
                RadioButton1.Visible = true;
                RadioButton2.Visible = true;
                ButtonReset1.Visible = true;
            }
            else
            {
                RadioButton1.Visible = false;
                RadioButton2.Visible = false;
                ButtonReset1.Visible = false;
            }

            SetColourMode(true, 0);
        }

        /// <summary>
        ///
        /// </summary>
        public void SetColourMode(bool resetPalette, int colourSet = 0)
        {
            switch (MachineType)
            {
                case MachineType.Atom:
                    if (resetPalette) palette.SetColourMode(MachineType.Atom);
                    SetAtomColourMode(colourSet);
                    break;

                case MachineType.BBC:
                    if (resetPalette) palette.SetColourMode(MachineType.BBC);
                    SetBeebColourMode();
                    break;

                default: throw new Exception("Invalid MachineType");
            }

            ButtonDrawColour.BackColor = palette.GetDrawColour;
            ButtonEraseColour.BackColor = palette.GetEraseColour;
        }

        private void SetAtomColourMode(int colourSet)
        {
            // Hide colours
            for (int i = 0; i < MAX_BEEB_COLOURS; i++)
            {
                Controls[i].Hide();
            }

            switch (palette.NumColours)
            {
                case 4:
                    for (int i = 0; i < palette.NumColours; i++)
                    {
                        Controls[i].Show();
                        Controls[i + 8].Show();
                    }
                    break;

                case 2:
                    for (int i = 0; i < palette.NumColours; i++)
                    {
                        Controls[i].Show();
                        Controls[i + 8].Show();
                    }
                    break;

                default: throw new Exception("Unknown number of Atom Colours");
            }

            for (int i = 0; i < palette.NumColours; i++)
            {
                if (colourSet == 0 || colourSet == 1)
                {
                    Controls[i].BackColor = palette.GetWindowsColour(palette.AcornColourSet1[i]);
                }
                if (colourSet == 0 || colourSet == 2)
                {
                    Controls[i + 8].BackColor = palette.GetWindowsColour(palette.AcornColourSet2[i]);
                }
            }
        }

        private void SetBeebColourMode()
        {
            for (int i = 0; i < MAX_BEEB_COLOURS; i++) Controls[i].Show();

            switch (palette.NumColours)
            {
                case 16:
                    break;

                case 4:
                    for (int i = 4; i < MAX_BEEB_COLOURS; i++)
                        Controls[i].Hide();
                    break;

                case 2:
                    for (int i = 2; i < MAX_BEEB_COLOURS; i++)
                        Controls[i].Hide();

                    break;

                default: throw new Exception("Unknown number of Beeb Colours");
            }

            for (int i = 0; i < palette.NumColours; i++)
            {
                Controls[i].BackColor = palette.GetWindowsColour(palette.AcornColourSet1[i]);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="acornColourType"></param>
        /// <returns></returns>
        public AcornColourType GetNextColour(AcornColourType acornColourType)
        {
            switch (MachineType)
            {
                case MachineType.Atom:
                    return (AcornColourType)(((int)acornColourType + 1) & 8);

                case MachineType.BBC:
                    return (AcornColourType)(((int)acornColourType + 1) & 7);

                default: throw new Exception("Invalid machine type");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="acornColourType"></param>
        /// <returns></returns>
        public AcornColourType GetPreviousColour(AcornColourType acornColourType)

        {
            switch (MachineType)
            {
                case MachineType.Atom:
                    return (AcornColourType)(((int)acornColourType - 1) & 8);

                case MachineType.BBC:
                    return (AcornColourType)(((int)acornColourType - 1) & 7);

                default: throw new Exception("Invalid machine type");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="button"></param>
        private void ChangePalette(int index, MouseButtons button)
        {
            if (machineType == MachineType.Atom) return;

            int tmpColourSet = palette.ColourSet;

            if (index >= palette.NumColours)
            {
                palette.ColourSet = 2;
            }

            switch (button)
            {
                case MouseButtons.Left:
                    palette.GetAcornColourSet[index % palette.NumColours] = GetNextColour(palette.GetAcornColourSet[index % palette.NumColours]);
                    break;

                case MouseButtons.Right:
                    palette.GetAcornColourSet[index % palette.NumColours] = GetPreviousColour(palette.GetAcornColourSet[index % palette.NumColours]);
                    break;
            }

            Color col = palette.GetWindowsColour(palette.GetAcornColourSet[index % palette.NumColours]);

            if (tmpColourSet == palette.ColourSet)
            {
                palette.WinColours[index % palette.NumColours] = col;
            }

            palette.ColourSet = tmpColourSet;

            Controls[index].BackColor = col;
            Controls[index].Invalidate();

            ChangeColour(index, button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="button"></param>
        private void ChangeColour(int index, MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    palette.DrawColour = index % palette.NumColours;
                    ButtonDrawColour.BackColor = palette.GetDrawColour;
                    ButtonDrawColour.Invalidate();
                    break;

                case MouseButtons.Right:
                    palette.EraseColour = index % palette.NumColours;
                    ButtonEraseColour.BackColor = palette.GetEraseColour;
                    ButtonEraseColour.Invalidate();
                    break;
            }

            ColourChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour1_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(0, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(0, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour2_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(1, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(1, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour3_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(2, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(2, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour4_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(3, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(3, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour5_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(4, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(4, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour6_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(5, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(5, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour7_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(6, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour7_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(6, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour8_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(7, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour8_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(7, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour9_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(8, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour9_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(8, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour10_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(9, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour10_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(9, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour11_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(10, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour11_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(10, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour12_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(11, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour12_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(11, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour13_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(12, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour13_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(12, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColour14_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(13, e.Button);
        }

        private void buttonColour14_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(13, e.Button);
        }

        private void buttonColour15_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(14, e.Button);
        }

        private void buttonColour15_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(14, e.Button);
        }

        private void buttonColour16_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(15, e.Button);
        }

        private void buttonColour16_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(15, e.Button);
        }

        private void buttonColour17_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeColour(16, e.Button);
        }

        private void buttonColour17_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangePalette(16, e.Button);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReset1_Click(object sender, EventArgs e)
        {
            SetColourMode(true, 1);
            Invalidate();
            ColourChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReset2_Click(object sender, EventArgs e)
        {
            SetColourMode(true, 2);
            Invalidate();
            ColourChanged?.Invoke(this, new EventArgs());
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            palette.ColourSet = 1;

            AcornColourType[] acornColourSet = palette.ColourSet == 1 ? palette.AcornColourSet1 : palette.AcornColourSet2;

            for (int i = 0; i < palette.NumColours; i++)
            {
                Color col = palette.GetWindowsColour(acornColourSet[i]);
                palette.WinColours[i] = col;
            }

            Invalidate();
            ColourChanged?.Invoke(this, new EventArgs());
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            palette.ColourSet = 2;

            AcornColourType[] acornColourSet = palette.ColourSet == 1 ? palette.AcornColourSet1 : palette.AcornColourSet2;

            for (int i = 0; i < palette.NumColours; i++)
            {
                Color col = palette.GetWindowsColour(acornColourSet[i]);
                palette.WinColours[i] = col;
            }

            Invalidate();
            ColourChanged?.Invoke(this, new EventArgs());
        }
    }
}