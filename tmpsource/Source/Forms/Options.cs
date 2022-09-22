using AcornPad.Common;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class Options : Form
    {
        private readonly SettingsInfoCollection settings = new SettingsInfoCollection();

        /// <summary>
        ///
        /// </summary>
        public Options()
        {
            InitializeComponent();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Options_Load(object sender, EventArgs e)
        {
            foreach (SettingsProperty sp in Properties.Settings.Default.Properties)
            {
                if (sp.Name.IndexOf("_") != 1 && Properties.Settings.Default.IsUserScoped(sp.Name))
                {
                    settings.Add(new SettingInfo(sp.Name));
                }
            }

            settings.Sort();

            LoadTreeView();
        }

        /// <summary>
        ///
        /// </summary>
        public void LoadTreeView()
        {
            if (settings.Count > 0)
            {
                foreach (SettingInfo info in settings)
                {
                    AddCat(TreeView1.Nodes, info.Category.Split('.'), 0);
                }
            }

            if (TreeView1.Nodes.Count > 0)
            {
                TreeView1.SelectedNode = TreeView1.Nodes[0];
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parentNodes"></param>
        /// <param name="fields"></param>
        /// <param name="index"></param>
        private void AddCat(TreeNodeCollection parentNodes, string[] fields, int index)
        {
            if (index > fields.GetUpperBound(0)) return;

            bool fieldFound = false;

            foreach (TreeNode childNode in parentNodes)
            {
                if (childNode.Text == fields[index])
                {
                    AddCat(childNode.Nodes, fields, index + 1);
                    fieldFound = true;
                }
            }

            if (fieldFound == false)
            {
                TreeNode node = parentNodes.Add(fields[index]);
                AddCat(node.Nodes, fields, index + 1);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            ButtonOK.Focus();

            foreach (SettingInfo info in settings)
            {
                Properties.Settings.Default[info.FullName] = info.Value;
            }

            Properties.Settings.Default.Save();
            ((MainForm)MdiParent).ApplySettings();
            DialogResult = DialogResult.OK;
            Close();
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
        private void ButtonApply_Click(object sender, EventArgs e)
        {
            ButtonApply.Focus();

            foreach (SettingInfo info in settings)
            {
                Properties.Settings.Default[info.FullName] = info.Value;
            }

            Properties.Settings.Default.Save();
            // TODO Raise Callback to update apply settings but stay on this page
            ((MainForm)MdiParent).ApplySettings();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TableLayoutPanel1.SuspendLayout();
            ClearOptions();

            TableLayoutPanel1.RowStyles.Clear();
            TableLayoutPanel1.RowCount = 1;

            // Get all items from category
            SettingInfo[] sets = settings.GetByCategory(TreeView1.SelectedNode.FullPath);

            for (int i = 0; i < sets.Length; i++)
            {
                RowStyle rowStyle = new RowStyle
                {
                    SizeType = SizeType.AutoSize
                };

                Label lbl = new Label
                {
                    AutoSize = true,
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                };

                lbl.Text = sets[i].Name;
                TableLayoutPanel1.Controls.Add(lbl);
                TableLayoutPanel1.RowStyles.Add(rowStyle);

                if (sets[i].Value is Boolean)
                {
                    CheckBox chk = new CheckBox
                    {
                        Name = sets[i].FullName,
                        Tag = sets[i].Value.GetType(),
                        Checked = sets[i].Value.ToBool()
                    };
                    TableLayoutPanel1.Controls.Add(chk);
                    chk.LostFocus += CheckBox_LostFocus;
                }
                else if (sets[i].Value is int || sets[i].Value is string)
                {
                    TextBox txt = new TextBox
                    {
                        Name = sets[i].FullName,
                        Tag = sets[i].Value.GetType(),
                        Text = sets[i].Value.ToString(),
                        Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
                    };
                    TableLayoutPanel1.Controls.Add(txt);
                    txt.LostFocus += TextBox_LostFocus;
                }
                else if (sets[i].Value is decimal)
                {
                    NumericUpDown num = new NumericUpDown
                    {
                        Name = sets[i].FullName,
                        Tag = sets[i].Value.GetType(),
                        Value = sets[i].Value.ToInteger(),
                        Minimum = 1,
                        Maximum = 1000,
                        Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
                    };
                    TableLayoutPanel1.Controls.Add(num);
                    num.LostFocus += NumericUpDown_LostFocus;
                }
                else if (sets[i].Value is Enum @enum)
                {
                    Enum en = @enum;

                    ComboBox cb = new ComboBox
                    {
                        Name = sets[i].FullName,
                        Tag = sets[i].Value,        // Don't use GetType()
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
                    };

                    foreach (Object o in Enum.GetValues(en.GetType()))
                    {
                        cb.Items.Add(o);
                    }

                    cb.SelectedItem = sets[i].Value;

                    TableLayoutPanel1.Controls.Add(cb);
                    cb.LostFocus += ComboBox_LostFocus;
                }
            }

            TableLayoutPanel1.ResumeLayout();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericUpDown_LostFocus(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            settings.SetValueByFullName(num.Name, (decimal)num.Value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_LostFocus(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            var enumType = cb.Tag.GetType();
            var value = Enum.Parse(enumType, cb.Text);

            settings.SetValueByFullName(cb.Name, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (txt.Tag is int)
            {
                settings.SetValueByFullName(txt.Name, txt.Text.ToInteger());
            }
            else
            {
                settings.SetValueByFullName(txt.Name, txt.Text);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_LostFocus(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            settings.SetValueByFullName(chk.Name, chk.Checked);
        }

        /// <summary>
        ///
        /// </summary>
        private void ClearOptions()
        {
            while (TableLayoutPanel1.Controls.Count > 0)
            {
                Control ctrl = TableLayoutPanel1.Controls[0];

                // Remove event handlers
                if (ctrl is CheckBox)
                {
                    ctrl.LostFocus -= CheckBox_LostFocus;
                }
                else if (ctrl is TextBox)
                {
                    ctrl.LostFocus -= TextBox_LostFocus;
                }
                else if (ctrl is ComboBox)
                {
                    ctrl.LostFocus -= ComboBox_LostFocus;
                }
                else if (ctrl is NumericUpDown)
                {
                    ctrl.LostFocus -= NumericUpDown_LostFocus;
                }

                // Remove control
                ctrl.Dispose();
            }
        }
    }
}