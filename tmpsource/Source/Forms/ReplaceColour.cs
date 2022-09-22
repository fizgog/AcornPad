using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class ReplaceColour : Form
    {
        private readonly AcornProject Project;

        public int OldColour => ColourPicker1.SelectedIndex;
        public int NewColour => ColourPicker2.SelectedIndex;

        public ReplaceColour(AcornProject project)
        {
            InitializeComponent();
            Project = project;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplaceColour_Load(object sender, System.EventArgs e)
        {
            ColourPicker1.AddPalette(Project.Palette);
            ColourPicker2.AddPalette(Project.Palette);

            UpdateColour();
        }

        private void ColourPicker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateColour();
        }

        /// <summary>
        ///
        /// </summary>
        private void UpdateColour()
        {
            buttonColour1.BackColor = ColourPicker1.SelectedValue;
            buttonColour2.BackColor = ColourPicker2.SelectedValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOK_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}