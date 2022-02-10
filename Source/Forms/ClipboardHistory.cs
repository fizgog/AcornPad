using System;
using System.Windows.Forms;

namespace AcornPad.Forms
{
    public partial class ClipboardHistory : Form
    {
        /// <summary>
        ///
        /// </summary>
        private readonly AcornProject Project;

        /// <summary>
        ///
        /// </summary>
        public event EventHandler ClipboardHistory_Changed;

        /// <summary>
        ///
        /// </summary>
        /// <param name="project"></param>
        public ClipboardHistory(AcornProject project)
        {
            InitializeComponent();
            Project = project;

            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClipboardHistory_FormClosing(object sender, FormClosingEventArgs e)
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
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public new void Invalidate()
        {
            base.Invalidate();

            if (Project != null && Project.Stack != null && RichTextBox1 != null && RichTextBox2 != null)
            {
                RichTextBox1.Clear();
                RichTextBox1.AppendText("UNDO");
                RichTextBox1.AppendText(Environment.NewLine);
                RichTextBox1.AppendText(Project.Stack.UndoList());

                RichTextBox2.Clear();
                RichTextBox2.AppendText("REDO");
                RichTextBox2.AppendText(Environment.NewLine);
                RichTextBox2.AppendText(Project.Stack.RedoList());
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUndo_Click(object sender, EventArgs e)
        {
            if (Project != null && Project.Stack != null)
            {
                Project.Undo();
                ClipboardHistory_Changed?.Invoke(this, e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRedo_Click(object sender, EventArgs e)
        {
            if (Project != null && Project.Stack != null)
            {
                Project.Redo();
                ClipboardHistory_Changed?.Invoke(this, e);
            }
        }

        
    }
}