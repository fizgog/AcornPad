using AcornPad.Common;
using System;
using System.Threading;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class ExportScreen : Form
    {
        private readonly AcornProject Project;

        private System.Threading.Thread Worker;

        public ExportScreen(AcornProject project)
        {
            InitializeComponent();

            Project = project;

            TextBoxCharName.Text = "char_sprite";
            statusStrip1.InProgress(false);
        }

        private void ExportScreen_Load(object sender, EventArgs e)
        {
            // Chars
            ComboBoxExportFormat.DataSource = Enum.GetValues(typeof(ExportFormatType));
            ComboBoxExportFormat.SelectedItem = Properties.Settings.Default["Options_Format_1"];

            ComboBoxConversion1.DataSource = Enum.GetValues(typeof(ConversionType));
            ComboBoxConversion1.SelectedItem = Properties.Settings.Default["Options_Conversion_2"];

            CheckBoxCommentsChar.Checked = (bool)Properties.Settings.Default["Options_Comments_3"];

            TextBoxCharName.Text = (string)Properties.Settings.Default["Character_Name_1"];

            ComboBoxLayoutChar.DataSource = Enum.GetValues(typeof(LayoutType));
            ComboBoxConversion1.SelectedItem = Properties.Settings.Default["Character_Layout_2"];

            CheckBoxPrefixCharacterName.Checked = (bool)Properties.Settings.Default["Character_Prefix_3"];

            ComboBoxCharacterColumns.DataSource = Enum.GetValues(typeof(ColumnType));
            ComboBoxCharacterColumns.SelectedItem = Properties.Settings.Default["Character_Columns_4"];

            // Tiles
            ComboBoxExportFormatTile.DataSource = Enum.GetValues(typeof(ExportFormatType));
            ComboBoxExportFormatTile.SelectedItem = Properties.Settings.Default["Options_Format_1"];

            ComboBoxConversionTile.DataSource = Enum.GetValues(typeof(ConversionType));
            ComboBoxConversionTile.SelectedItem = Properties.Settings.Default["Options_Conversion_2"];

            CheckBoxCommentsTile.Checked = (bool)Properties.Settings.Default["Options_Comments_3"];

            // TODO
            ComboBoxExportFormatTile.DataSource = Enum.GetValues(typeof(ExportFormatType));
            ComboBoxExportFormatTile.SelectedItem = Properties.Settings.Default["Options_Format_1"];

            ComboBoxConversionTile.DataSource = Enum.GetValues(typeof(ConversionType));
            ComboBoxConversionTile.SelectedItem = Properties.Settings.Default["Options_Conversion_2"];

            CheckBoxCommentsTile.Checked = (bool)Properties.Settings.Default["Options_Comments_3"];

            NumericUpDownColumnTile.Value = (decimal)Properties.Settings.Default["Tile_Columns_1"];

            // Maps
            ComboBoxExportFormatMap.DataSource = Enum.GetValues(typeof(ExportFormatType));
            ComboBoxExportFormatMap.SelectedItem = Properties.Settings.Default["Options_Format_1"];

            ComboBoxConversionMap.DataSource = Enum.GetValues(typeof(ConversionType));
            ComboBoxConversionMap.SelectedItem = Properties.Settings.Default["Options_Conversion_2"];

            CheckBoxCommentsMap.Checked = (bool)Properties.Settings.Default["Options_Comments_3"];

            ComboBoxCompressionMap.DataSource = Enum.GetValues(typeof(CompressionType));
            ComboBoxCompressionMap.SelectedItem = Properties.Settings.Default["Map_Compression_1"];

            NumericUpDownColumnMap.Value = (decimal)Properties.Settings.Default["Map_Columns_2"];
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Worker != null && Worker.IsAlive)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Worker != null && Worker.IsAlive)
            {
                return;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (Worker != null && Worker.IsAlive)
                return;

            Close();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            ButtonSettings.Enabled = false;
            ButtonGenerate.Enabled = false;
            ButtonOK.Enabled = false;

            statusStrip1.InProgress(true);

            // Chars
            ExportFormatType CharFormat = (ExportFormatType)ComboBoxExportFormat.SelectedItem;
            ConversionType CharConversion = (ConversionType)ComboBoxConversion1.SelectedItem;
            bool CharComments = CheckBoxCommentsChar.Checked;
            string CharName = TextBoxCharName.Text;
            LayoutType layout = (LayoutType)ComboBoxLayoutChar.SelectedItem;
            string layoutPrefix = layout.ToString();
            bool addPrefix = CheckBoxPrefixCharacterName.Checked;
            int columns = (int)ComboBoxCharacterColumns.SelectedItem;
            CharName = addPrefix ? layoutPrefix + "" + CharName : CharName;

            // Tiles
            ExportFormatType TileFormat = (ExportFormatType)ComboBoxExportFormatTile.SelectedItem;
            ConversionType TileConversion = (ConversionType)ComboBoxConversionTile.SelectedItem;
            bool TileComments = CheckBoxCommentsTile.Checked;
            int TileColumns = (int)NumericUpDownColumnTile.Value;

            // Map
            ExportFormatType MapFormat = (ExportFormatType)ComboBoxExportFormatMap.SelectedItem;
            ConversionType MapConversion = (ConversionType)ComboBoxConversionMap.SelectedItem;
            bool MapComments = CheckBoxCommentsMap.Checked;
            int MapColumns = (int)NumericUpDownColumnMap.Value;
            bool compression = (ComboBoxCompressionMap.SelectedIndex == 1);

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    Worker = new Thread(() => GenerateChars(CharFormat, CharName, columns, layout, CharComments, CharConversion));
                    break;

                case 1:
                    Worker = new Thread(() => GenerateTiles(TileFormat, TileConversion, TileComments, TileColumns));

                    break;

                case 2:
                    Worker = new Thread(() => GenerateMaps(MapFormat, MapConversion, MapComments, MapColumns, compression));

                    break;

                default: throw new Exception("Invalid tab option");
            }

            Worker.Start();
        }

        /// <summary>
        ///
        /// </summary>
        private void GenerateChars(ExportFormatType format, string charName, int columns, LayoutType layout, bool comments, ConversionType conversion)
        {
            int digits = 3;
            int bytes = Project.Machine.BitsPerPixel * 8;

            richTextBox1.Clear();
            richTextBox1.AppendText(string.Format("; {0} Build {1}", Sys.AssemblyTitle, Sys.AssemblyVersion));
            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.AppendText(string.Format("; {0}", Project.Machine.Description));
            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.AppendText(Environment.NewLine);

            richTextBox1.AppendText("; Charset data...");
            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.AppendText(string.Format("; {0} images, {1} bytes per image, total size is {2} bytes.", Project.Chars.Count, bytes, Project.Chars.Count * bytes));
            richTextBox1.AppendText(Environment.NewLine);

            for (int i = 0; i < Project.Chars.Count; i++)
            {
                Sys.GenerateChar(Project, richTextBox1, i, format, charName, columns, digits, layout, comments, conversion);
            }
            richTextBox1.AppendText(Environment.NewLine);

            BeginInvoke(new Action(() => ThreadComplete()));
        }

        /// <summary>
        ///
        /// </summary>
        private void GenerateTiles(ExportFormatType format, ConversionType conversion, bool comments, int columns)
        {
            richTextBox2.Clear();
            richTextBox2.AppendText(string.Format("; {0} Build {1}", Sys.AssemblyTitle, Sys.AssemblyVersion));
            richTextBox2.AppendText(Environment.NewLine);
            richTextBox2.AppendText(string.Format("; {0}", Project.Machine.Description));
            richTextBox2.AppendText(Environment.NewLine);
            richTextBox2.AppendText(Environment.NewLine);

            richTextBox2.AppendText("; Tileset data...");
            richTextBox2.AppendText(Environment.NewLine);

            if (Project.TilesOnline)
            {
                int digits = 3;
                int width = Project.Tiles.Width;
                int height = Project.Tiles.Height;

                richTextBox2.AppendText(string.Format("; {0} tiles, {1} cells per tile {2}x{3}, total size is {4} bytes.", Project.Tiles.Count, Project.Tiles.Area, width, height, Project.Tiles.TotalBytes));
                richTextBox2.AppendText(Environment.NewLine);

                for (int i = 0; i < Project.Tiles.Count; i++)
                {
                    Sys.GenerateTile(Project, richTextBox2, i, format, columns, digits, comments, conversion);
                }
            }
            else
            {
                richTextBox2.AppendText("; Tile not used");
            }

            richTextBox2.AppendText(Environment.NewLine);

            BeginInvoke(new Action(() => ThreadComplete()));
        }

        /// <summary>
        /// Generate map into a RichTextBox
        /// </summary>
        private void GenerateMaps(ExportFormatType format, ConversionType conversion, bool comments, int columns, bool compression)
        {
            int digits = 3;
            int width = Project.Maps.Width;
            int height = Project.Maps.Height;

            richTextBox3.Clear();
            richTextBox3.AppendText(string.Format("; {0} Build {1}", Sys.AssemblyTitle, Sys.AssemblyVersion));
            richTextBox3.AppendText(Environment.NewLine);
            richTextBox3.AppendText(string.Format("; {0}", Project.Machine.Description));
            richTextBox3.AppendText(Environment.NewLine);
            richTextBox3.AppendText(Environment.NewLine);

            richTextBox3.AppendText("; Mapset data...");
            richTextBox3.AppendText(Environment.NewLine);
            richTextBox3.AppendText(string.Format("; {0} maps, {1} cells per map {2}x{3}, total size is {4} bytes.", Project.Maps.Count, Project.Maps.Area, width, height, Project.Maps.TotalBytes));
            richTextBox3.AppendText(Environment.NewLine);

            for (int i = 0; i < Project.Maps.Count; i++)
            {
                Sys.GenerateMap(Project, richTextBox3, i, format, columns, digits, comments, conversion, compression);
            }

            BeginInvoke(new Action(() => ThreadComplete()));
        }

        /// <summary>
        ///
        /// </summary>
        private void ThreadComplete()
        {
            statusStrip1.InProgress(false);
            ButtonSettings.Enabled = true;
            ButtonGenerate.Enabled = true;
            ButtonOK.Enabled = true;
        }
    }
}