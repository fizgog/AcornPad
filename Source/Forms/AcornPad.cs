using AcornPad.Common;
using AcornPad.Forms;
using AcornPad.Internal;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AcornPad
{
    public partial class MainForm : Form
    {
        private AcornProject Project;

        private RecentFilesList RecentFiles;

        private PaletteEdit paletteEdit;

        private CharSet charSet;

        private CharEdit charEdit;

        private TileSet tileSet;

        private TileEdit tileEdit;

        private MapEdit mapEdit;

        private ClipboardHistory clipboardHistory;

        //private readonly List<Machine> machineList;

        private string Filename { get; set; }

        public bool IsProjectDirty { get; set; }

        public bool IsProjectOnline { get; set; }

        /// <summary>
        ///
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Filename = null;
            IsProjectOnline = false;
            IsProjectDirty = false;

            ApplySettings();
        }

        public void ApplySettings()
        {
            if ((bool)Properties.Settings.Default["AcornPad_Backdrop_1"])
            {
                BackgroundImage = Properties.Resources.AcornBackdrop;
            }
            else
            {
                BackgroundImage = null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateMenus();
            RecentFiles = new RecentFilesList(RecentFilesToolStripMenuItem, OpenProject);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsProjectDirty)
            {
                DialogResult result = MessageBox.Show("You have unsaved work.\r\n\r\nDo you wish to save before creating a new project?",
                                                      "Warning",
                                                      MessageBoxButtons.YesNoCancel,
                                                      MessageBoxIcon.Exclamation);

                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    if (SaveProject(Filename) == false)
                        return;
                }
            }

            CloseProject();

            NewProject newProject = new NewProject();

            if (newProject.ShowDialog() == DialogResult.OK)
            {
                //
                int mapWidth = newProject.GetMachine.TextWidth;
                int mapHeight = newProject.GetMachine.TextHeight;
                MachineType machineType = newProject.GetMachine.MachineType == "Acorn Atom" ? MachineType.Atom : MachineType.BBC;

                Project = new AcornProject(machineType, 64, 8, 8, 0, 0, 0, 1, mapWidth, mapHeight)
                {
                    Machine = newProject.GetMachine,
                    Palette = new Palette(machineType, newProject.GetMachine.NumColours)
                };

                ShowProject();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsProjectDirty)
            {
                DialogResult result = MessageBox.Show("You have unsaved work.\r\n\r\nDo you wish to save before opening a new project?",
                                                      "Warning",
                                                      MessageBoxButtons.YesNoCancel,
                                                      MessageBoxIcon.Exclamation);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    if (SaveProject(Filename) == false)
                        return;
                }
            }

            CloseProject();
            OpenProject();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Project != null)
            {
                SaveProject(Filename);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Project != null)
            {
                SaveProject(null);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsProjectDirty)
            {
                DialogResult result = MessageBox.Show("You have unsaved work.\r\n\r\nDo you wish to save before closing?",
                                               "Warning",
                                               MessageBoxButtons.YesNoCancel,
                                               MessageBoxIcon.Exclamation);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    if (SaveProject(Filename) == false)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Palette_PaletteChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void ShowProject()
        {
            if (Project != null)
            {
                IsProjectOnline = true;
                IsProjectDirty = false;

                SetTitle();
                UpdateMenus();

                paletteEdit = new PaletteEdit(Project)
                {
                    MdiParent = this
                };

                paletteEdit.PaletteChanged += Palette_PaletteChanged;
                paletteEdit.TilesOnlineChanged += Palette_TilesOnlineChanged;
                paletteEdit.MultipleMapChanged += Palette_MultipleMapChanged;
                paletteEdit.Show();

                charSet = new CharSet(Project)
                {
                    MdiParent = this
                };

                charSet.CharSet_ImageChanged += ImageChanged;
                charSet.Show();

                charEdit = new CharEdit(Project)
                {
                    MdiParent = this
                };

                charEdit.CharEdit_ImageChanged += ImageChanged;
                charEdit.Show();

                mapEdit = new MapEdit(Project)
                {
                    MdiParent = this
                };

                mapEdit.MapEdit_ImageChanged += ImageChanged;
                mapEdit.Show();

                ShowProjectTiles();
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void ShowProjectTiles()
        {
            // Only show tiles if they have been turned on
            if (Project.TilesOnline && Project.Tiles != null)
            {
                // Tile Set Form
                tileSet = new TileSet(Project)
                {
                    MdiParent = this
                };
                tileSet.TileSet_ImageChanged += ImageChanged;
                tileSet.Show();

                // Tile Editor Form
                tileEdit = new TileEdit(Project)
                {
                    MdiParent = this
                };
                tileEdit.TileEdit_ImageChanged += ImageChanged;
                tileEdit.Show();
            }
            else
            {
                if (tileSet != null)
                {
                    tileSet.Dispose();
                    tileSet.Close();
                    tileSet = null;
                }
                if (tileEdit != null)
                {
                    tileEdit.Dispose();
                    tileEdit.Close();
                    tileEdit = null;
                }

                Project.Tiles = null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Palette_MultipleMapChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Palette_TilesOnlineChanged(object sender, EventArgs e)
        {
            if (Project.TilesOnline)
            {
                NewTileSize newTileSize = new NewTileSize();
                if (newTileSize.ShowDialog() == DialogResult.OK)
                {
                    int width = newTileSize.TileSize.Width;
                    int height = newTileSize.TileSize.Height;

                    Project.AddHistory("Convert Characters to Tiles");
                    Project.ConvertCharMapToTiles(width, height);
                }
                else
                {
                    Project.TilesOnline = false;
                    paletteEdit.Invalidate();
                }
            }
            else
            {
                Project.AddHistory("Convert Tiles to Characters");
                // Destroy tiles and reset map to using chars
                Project.ConvertTileMapToChars();
                //Project.Maps.RemapTilesToChars();
                //Project.Tiles.Dispose(); / TODO
            }

            ShowProjectTiles();
            mapEdit.Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        private void OpenProject()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Open Acorn Project File",
                Filter = "Acorn Project Files|*.prj"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenProject(openFileDialog1.FileName);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        public void OpenProject(string filename)
        {
            if (filename != "")
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamReader sr = new StreamReader(filename))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        try
                        {
                            Project = (AcornProject)serializer.Deserialize(reader, typeof(AcornProject));
                        }
                        catch
                        {
                            MessageBox.Show("Invalid Acorn project file", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                Filename = filename;

                RecentFiles.Add(filename);

                ShowProject();
            }
        }

        /// <summary>
        ///
        /// </summary>
        private bool SaveProject(string filename)
        {
            if (filename == null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog
                {
                    Title = "Save Acorn Project File",
                    Filter = "Acorn Project Files|*.prj"
                };
                saveFileDialog1.ShowDialog();
                filename = saveFileDialog1.FileName;
            }

            if (filename != null && filename != "")
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamWriter sw = new StreamWriter(filename))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, Project);
                    }
                }

                Filename = filename;
                IsProjectDirty = false;
                SetTitle();
                return true;
            }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        private void CloseProject()
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Dispose();
                frm.Close();
            }

            Project = null;
            clipboardHistory = null;
            Filename = null;
            IsProjectDirty = false;
            Text = "AcornPad";
        }

        /// <summary>
        ///
        /// </summary>
        private new void Invalidate()
        {
            // If we can undo then the project is dirty!
            IsProjectDirty = Project.CanUndo();
            SetTitle();
            UpdateMenus();

            if (paletteEdit != null) paletteEdit.Invalidate();
            if (charSet != null) charSet.Invalidate();
            if (charEdit != null) charEdit.Invalidate();
            if (tileSet != null) tileSet.Invalidate();
            if (tileEdit != null) tileEdit.Invalidate();
            if (mapEdit != null) mapEdit.Invalidate();

            if (clipboardHistory != null && clipboardHistory.Visible) clipboardHistory.Invalidate();

            UndoToolStripMenuItem.Enabled = Project.CanUndo();
            RedoToolStripMenuItem.Enabled = Project.CanRedo();

            base.Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options()
            {
                MdiParent = this
            };

            options.Show();
        }

        /// <summary>
        ///
        /// </summary>
        private void SetTitle()
        {
            Text = "AcornPad - ";
            Text += Filename ?? "Untitled";
            Text += (IsProjectDirty) ? "*" : "";
        }

        /// <summary>
        ///
        /// </summary>
        private void UpdateMenus()
        {
            // File
            SaveToolStripMenuItem.Enabled = IsProjectDirty;
            SaveAsToolStripMenuItem.Enabled = IsProjectDirty;
            ExportToolStripMenuItem.Enabled = IsProjectOnline;

            // Edit
            UndoToolStripMenuItem.Enabled = IsProjectOnline && Project.CanUndo();
            RedoToolStripMenuItem.Enabled = IsProjectOnline && Project.CanRedo();

            ButtonCut.Enabled = CutTool.Enabled = IsProjectOnline;// Project.CanCut();
            ButtonCopy.Enabled = CopyTool.Enabled = IsProjectOnline;// Project.CanCopy();
            ButtonPaste.Enabled = PasteTool.Enabled = IsProjectOnline;// Project.CanPaste();

            // View
            PaletteToolStripMenuItem.Enabled = IsProjectOnline;
            CharacterSetToolStripMenuItem.Enabled = IsProjectOnline;
            CharacterEditorToolStripMenuItem.Enabled = IsProjectOnline;
            TileSetToolStripMenuItem.Enabled = IsProjectOnline && Project.TilesOnline;
            TileEditorToolStripMenuItem.Enabled = IsProjectOnline && Project.TilesOnline;
            MapEditorToolStripMenuItem.Enabled = IsProjectOnline;

            // Tools
            ExportToScreenToolStripMenuItem.Enabled = IsProjectOnline;
            ClipboardHistoryToolStripMenuItem.Enabled = IsProjectOnline;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;

            if (IsProjectDirty)
            {
                result = MessageBox.Show("You have unsaved work.\r\n\r\nDo you wish to save before creating a new project?",
                                                      "Warning",
                                                      MessageBoxButtons.YesNoCancel,
                                                      MessageBoxIcon.Exclamation);

                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    if (SaveProject(Filename) == false)
                        return;
                }
            }

            CloseProject();

            ImportImage frm = new ImportImage();

            result = frm.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (frm.Project != null)
                {
                    Project = frm.Project;
                    //Project.AddHistory("Import Image");
                    ShowProject();
                }
            }

            frm.Dispose();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportToScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportScreen exportScreen = new ExportScreen(Project)
            {
                MdiParent = this
            };

            exportScreen.Show();
        }

        private void CutTool_Click(object sender, EventArgs e)
        {
            // Check for active form
            Form activeChild = this.ActiveMdiChild;

            if (activeChild is CharSet) Project.Cut(Common.DataType.Char);
            if (activeChild is CharEdit) Project.Cut(Common.DataType.Char);
            if (activeChild is TileSet) Project.Cut(Common.DataType.Tile);
            if (activeChild is TileEdit) Project.Cut(Common.DataType.Tile);
            //if (activeChild is MapEdit) Project.Cut(Common.DataType.Map); // dont cut

            Invalidate();
        }

        private void CopyTool_Click(object sender, EventArgs e)
        {
            // Check for active form
            Form activeChild = this.ActiveMdiChild;

            if (activeChild is CharSet) Project.Copy(Common.DataType.Char);
            if (activeChild is CharEdit) Project.Copy(Common.DataType.Char);
            if (activeChild is TileSet) Project.Copy(Common.DataType.Tile);
            if (activeChild is TileEdit) Project.Copy(Common.DataType.Tile);
            if (activeChild is MapEdit) Project.Copy(Common.DataType.Map);

            Invalidate();
        }

        private void PasteTool_Click(object sender, EventArgs e)
        {
            // Check for active form
            Form activeChild = this.ActiveMdiChild;

            if (activeChild is CharSet) Project.Paste(Common.DataType.Char);
            if (activeChild is CharEdit) Project.Paste(Common.DataType.Char);
            if (activeChild is TileSet) Project.Paste(Common.DataType.Tile);
            if (activeChild is TileEdit) Project.Paste(Common.DataType.Tile);
            if (activeChild is MapEdit) Project.Paste(Common.DataType.Map);

            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (paletteEdit != null)
            {
                paletteEdit.Show();
                paletteEdit.WindowState = FormWindowState.Normal;
                paletteEdit.BringToFront();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (charSet != null)
            {
                charSet.Show();
                charSet.WindowState = FormWindowState.Normal;
                charSet.BringToFront();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (charEdit != null)
            {
                charEdit.Show();
                charEdit.WindowState = FormWindowState.Normal;
                charEdit.BringToFront();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tileSet != null)
            {
                tileSet.Show();
                tileSet.WindowState = FormWindowState.Normal;
                tileSet.BringToFront();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tileEdit != null)
            {
                tileEdit.Show();
                tileEdit.WindowState = FormWindowState.Normal;
                tileEdit.BringToFront();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapEdit != null)
            {
                mapEdit.Show();
                mapEdit.WindowState = FormWindowState.Normal;
                mapEdit.BringToFront();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.Undo();

            if (Project.TilesOnline == false && tileEdit != null)
            {
                tileSet.Dispose();
                tileSet.Close();
                tileSet = null;

                tileEdit.Dispose();
                tileEdit.Close();
                tileEdit = null;
            }

            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.Redo();

            if (Project.TilesOnline == false && tileEdit != null)
            {
                tileSet.Dispose();
                tileSet.Close();
                tileSet = null;

                tileEdit.Dispose();
                tileEdit.Close();
                tileEdit = null;
            }

            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClipboardHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clipboardHistory == null)
            {
                clipboardHistory = new ClipboardHistory(Project)
                {
                    MdiParent = this
                };
                clipboardHistory.ClipboardHistory_Changed += ClipboardHistory_Changed;
            }

            clipboardHistory.Show();
            clipboardHistory.WindowState = FormWindowState.Normal;
            clipboardHistory.BringToFront();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClipboardHistory_Changed(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCompress_Click(object sender, EventArgs e)
        {
            Project.AddHistory("Compress Character");
            Project.CompressData(Project.Chars, Project.TilesOnline);
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportASMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Save Acorn project",
                Filter = "Acorn project |*.asm"
            };

            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK && saveFileDialog1.FileName != string.Empty)
            {
                AcornPad.Controls.RichTextBox rtb = new AcornPad.Controls.RichTextBox();
                rtb.Clear();
                rtb.AppendText(string.Format("; {0} Build {1}", Sys.AssemblyTitle, Sys.AssemblyVersion));
                rtb.AppendText(Environment.NewLine);
                rtb.AppendText(";");
                rtb.AppendText(Environment.NewLine);
                rtb.AppendText(Environment.NewLine);

                ExportPaletteSet(rtb);
                ExportCharSet(rtb);
                ExportTileSet(rtb);
                ExportMapSet(rtb);

                rtb.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rtb"></param>
        private void ExportPaletteSet(RichTextBox rtb)
        {
            rtb.AppendText("; Palette data...");
            rtb.AppendText(Environment.NewLine);
            rtb.AppendText(string.Format("; {0} colours", Project.Palette.GetAcornColourSet.Length));
            rtb.AppendText(Environment.NewLine);

            for (int i = 0; i < Project.Palette.GetAcornColourSet.Length; i++)
            {
                rtb.AppendText(string.Format("    EQUS 19,{0},{1},0,0,0", i, (int)Project.Palette.GetAcornColourSet[i]));
                rtb.AppendText(Environment.NewLine);
            }
            rtb.AppendText(Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportCharacterSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Save Acorn Character Set",
                Filter = "Acorn Character Set |*.asm"
            };

            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK && saveFileDialog1.FileName != string.Empty)
            {
                AcornPad.Controls.RichTextBox rtb = new AcornPad.Controls.RichTextBox();
                rtb.Clear();
                rtb.AppendText(string.Format("; {0} Build {1}", Sys.AssemblyTitle, Sys.AssemblyVersion));
                rtb.AppendText(Environment.NewLine);
                rtb.AppendText(string.Format("; {0}", Project.Machine.Description));
                rtb.AppendText(Environment.NewLine);
                rtb.AppendText(Environment.NewLine);

                ExportCharSet(rtb);

                rtb.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rtb"></param>
        private void ExportCharSet(AcornPad.Controls.RichTextBox rtb)
        {
            ExportFormatType format = (ExportFormatType)Properties.Settings.Default["Options_Format_1"];
            ConversionType conversion = (ConversionType)Properties.Settings.Default["Options_Conversion_2"];
            bool comments = (bool)Properties.Settings.Default["Options_Comments_3"];

            int digits = 3;
            string charName = (string)Properties.Settings.Default["Character_Name_1"];
            LayoutType layout = (LayoutType)Properties.Settings.Default["Character_Layout_2"];

            string layoutPrefix = layout.ToString();
            bool addPrefix = (bool)Properties.Settings.Default["Character_Prefix_3"];
            int columns = (int)Properties.Settings.Default["Character_Columns_4"];

            charName = addPrefix ? layoutPrefix + "" + charName : charName;

            int bytes = Project.Machine.PixelsBerByte * 8;
            rtb.AppendText("; Charset data...");
            rtb.AppendText(Environment.NewLine);
            rtb.AppendText(string.Format("; {0} images, {1} bytes per image, total size is {2} bytes.", Project.Chars.Count, bytes, Project.Chars.Count * bytes));
            rtb.AppendText(Environment.NewLine);

            for (int i = 0; i < Project.Chars.Count; i++)
            {
                Sys.GenerateChar(Project, rtb, i, format, charName, columns, digits, layout, comments, conversion);
            }
            rtb.AppendText(Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportTileSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Save Acorn Tile Set",
                Filter = "Acorn Tile Set |*.asm"
            };

            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK && saveFileDialog1.FileName != string.Empty)
            {
                AcornPad.Controls.RichTextBox rtb = new AcornPad.Controls.RichTextBox();
                rtb.Clear();
                rtb.AppendText(string.Format("; {0} Build {1}", Sys.AssemblyTitle, Sys.AssemblyVersion));
                rtb.AppendText(Environment.NewLine);
                rtb.AppendText(";");
                rtb.AppendText(Environment.NewLine);
                rtb.AppendText(Environment.NewLine);

                ExportTileSet(rtb);

                rtb.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rtb"></param>
        private void ExportTileSet(AcornPad.Controls.RichTextBox rtb)
        {
            ExportFormatType format = (ExportFormatType)Properties.Settings.Default["Options_Format_1"];
            ConversionType conversion = (ConversionType)Properties.Settings.Default["Options_Conversion_2"];
            bool comments = (bool)Properties.Settings.Default["Options_Comments_3"];
            decimal columns = (decimal)Properties.Settings.Default["Tile_Columns_1"];

            rtb.AppendText("; Tileset data...");
            rtb.AppendText(Environment.NewLine);

            if (Project.TilesOnline)
            {
                int digits = 3;
                int width = Project.Tiles.Width;
                int height = Project.Tiles.Height;

                rtb.AppendText(string.Format("; {0} tiles, {1} cells per tile {2}x{3}, total size is {4} bytes.", Project.Tiles.Count, Project.Tiles.Area, width, height, Project.Tiles.TotalBytes));
                rtb.AppendText(Environment.NewLine);

                for (int i = 0; i < Project.Tiles.Count; i++)
                {
                    Sys.GenerateTile(Project, rtb, i, format, (int)columns, digits, comments, conversion);
                }
            }
            else
            {
                rtb.AppendText("; Tile not used");
            }

            rtb.AppendText(Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Save Acorn Map Set",
                Filter = "Acorn Map Set |*.asm"
            };

            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK && saveFileDialog1.FileName != string.Empty)
            {
                AcornPad.Controls.RichTextBox rtb = new AcornPad.Controls.RichTextBox();
                rtb.Clear();
                rtb.AppendText(string.Format("; {0} Build {1}", Sys.AssemblyTitle, Sys.AssemblyVersion));
                rtb.AppendText(Environment.NewLine);
                rtb.AppendText(";");
                rtb.AppendText(Environment.NewLine);
                rtb.AppendText(Environment.NewLine);

                ExportMapSet(rtb);

                rtb.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        private void ExportMapSet(AcornPad.Controls.RichTextBox rtb)
        {
            ExportFormatType format = (ExportFormatType)Properties.Settings.Default["Options_Format_1"];
            ConversionType conversion = (ConversionType)Properties.Settings.Default["Options_Conversion_2"];
            bool comments = (bool)Properties.Settings.Default["Options_Comments_3"];

            int digits = 3;
            CompressionType compressionType = (CompressionType)Properties.Settings.Default["Map_Compression_1"];

            decimal columns = (decimal)Properties.Settings.Default["Map_Columns_2"];

            bool compression = compressionType != CompressionType.None;

            int width = Project.Maps.Items[0].Width;
            int height = Project.Maps.Items[0].Height;
            int bytes = width * height;

            rtb.AppendText("; Mapset data...");
            rtb.AppendText(Environment.NewLine);
            rtb.AppendText(string.Format("; {0} maps, {1} bytes per map {2}x{3}, total size is {4} bytes.", Project.Maps.Count, bytes, width, height, Project.Maps.Count * bytes));
            rtb.AppendText(Environment.NewLine);

            for (int i = 0; i < Project.Maps.Count; i++)
            {
                Sys.GenerateMap(Project, rtb, i, format, (int)columns, digits, comments, conversion, compression);
            }

            rtb.AppendText(Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportMapToScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Save Acorn Screen Image",
                Filter = "Acorn Screen Image |*.bin"
            };

            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK && saveFileDialog1.FileName != string.Empty)
            {
                FileStream stream = new FileStream(saveFileDialog1.FileName, FileMode.Create);

                int bitsPerPixel = Project.Machine.PixelsBerByte;
                int mapWidth = Project.Maps.Items[0].Width;
                int mapHeight = Project.Maps.Items[0].Height;

                for (int my = 0; my < mapHeight; my++)
                {
                    for (int mx = 0; mx < mapWidth; mx++)
                    {
                        // get map data
                        int index = my * mapWidth + mx;
                        int tileXY = Project.Maps.Items[0].Data[index];

                        int[] beeb = Sys.ConvertToRow(bitsPerPixel, Project.Chars.Items[tileXY].Data);

                        byte[] bytes = beeb.Select(i => (byte)i).ToArray();

                        stream.Write(bytes, 0, beeb.Length);
                    }
                }

                stream.Close();
            }
        }

        private void ReleaseNotesMenu_Click(object sender, EventArgs e)
        {
            ReleaseNotes notes = new ReleaseNotes()
            {
                MdiParent = this
            };

            notes.Show();
        }
    }
}