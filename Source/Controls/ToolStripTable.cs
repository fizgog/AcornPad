using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace WindowsFormsApp1.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
    public class ToolStripTable : ToolStripControlHost
    {
        public event TableEventHandler TableControl_Selected;

        public event EventHandler TableControl_Cancelled;

        public ToolStripTable() : base(CreateControlInstance())
        {
            ToolStripTableControl control = Control as ToolStripTableControl;
            control.Owner = this;
            control.TableControl_Selected += ToolStrip_TableSelected;
            control.TableControl_Cancelled += ToolStrip_TableCancelled;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStrip_TableSelected(object sender, EventArgs.TableEventArgs e)
        {
            TableControl_Selected?.Invoke(sender, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStrip_TableCancelled(object sender, System.EventArgs e)
        {
            TableControl_Cancelled?.Invoke(sender, e);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TableControl TableControl => Control as TableControl;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private static Control CreateControlInstance()
        {
            TableControl table = new ToolStripTableControl();

            return table;
        }
    }
}