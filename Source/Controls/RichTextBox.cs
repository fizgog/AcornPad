using System;
using System.Drawing;

namespace AcornPad.Controls
{
    public partial class RichTextBox : System.Windows.Forms.RichTextBox
    {
        //private delegate SetStringHandler(string value);

        /// <summary>
        ///
        /// </summary>
        public RichTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        ///
        /// </summary>
        public new void Clear()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Clear()));
                return;
            }

            base.Clear();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        public new void AppendText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendText), new object[] { text });
                return;
            }

            base.AppendText(text);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="box"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public void AppendText(string text, Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, Color>(AppendText), new object[] { text, color });
                return;
            }

            SelectionStart = TextLength;
            SelectionLength = 0;

            SelectionColor = color;
            base.AppendText(text);
            //AppendText(text);
            SelectionColor = ForeColor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] SplitText()
        {
            if (InvokeRequired)
            {
                var text = (string)Invoke(new Func<string>(() => Text));

                return text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return base.Text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        }
    }
}