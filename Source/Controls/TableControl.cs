using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.EventArgs;

namespace WindowsFormsApp1.Controls
{
    public delegate void TableEventHandler(object sender, TableEventArgs e);

    public class TableControl : Control
    {
        private readonly Color selectedColour = Color.CornflowerBlue;
        private readonly Color borderColour = SystemColors.WindowText;
        private readonly Color textColour = SystemColors.WindowText;

        public event TableEventHandler TableControl_Selected;

        public event EventHandler TableControl_Cancelled;

        [DefaultValue(24)]
        public int CellSize { get; set; }

        [DefaultValue(2)]
        public int CellSpacing { get; set; }

        [DefaultValue("Cancel")]
        public string ButtonText { get; set; }

        [DefaultValue(typeof(Size), "10, 10")]
        public Size TableSize { get; set; }

        private Size selectedSize;

        public Size SelectedSize
        {
            get { return selectedSize; }
            set
            {
                if (selectedSize != value)
                {
                    selectedSize = value;

                    ButtonText = (selectedSize.Width > 0 && selectedSize.Height > 0) ? String.Format("{0} x {1} Tile", selectedSize.Width, selectedSize.Height) : "Cancel";
                    Invalidate();
                }
            }
        }

        public Rectangle TableBounds => new Rectangle(CellSpacing * 2, CellSpacing, CellSize * TableSize.Width, CellSize * TableSize.Height);

        /// <summary>
        ///
        /// </summary>
        public TableControl()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);

            CellSize = 24;
            CellSpacing = 2;
            ButtonText = "Cancel";

            TableSize = new Size(10, 10);
            SelectedSize = new Size(2, 2); // Temp

            Size = TableBounds.Size + new Size(CellSpacing * 2, CellSpacing * 4 + Font.Height);
        }

        protected enum HitPart
        {
            Border,
            Table,
            Button
        }

        protected struct HitInfo
        {
            public HitPart part;
            public int col;
            public int row;
        }

        protected HitInfo QueryHit(Point pt)
        {
            HitInfo info = new HitInfo();

            Rectangle bounds = TableBounds;

            if (bounds.Contains(pt))
            {
                info.part = HitPart.Table;
                info.col = (pt.X - bounds.X) / CellSize;
                info.row = (pt.Y - bounds.Y) / CellSize;
            }
            else
            {
                info.part = (pt.Y > bounds.Bottom) ? HitPart.Button : HitPart.Border;
                info.col = -1;
                info.row = -1;
            }

            return info;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Brush brush = new SolidBrush(selectedColour))
            {
                using (Pen pen = new Pen(borderColour))
                {
                    int extent = CellSize - CellSpacing - 1;

                    Rectangle cellRect = new Rectangle(0, TableBounds.Top, extent, extent);

                    for (int row = 0; row < TableSize.Height; ++row, cellRect.Y += CellSize)
                    {
                        cellRect.X = TableBounds.Left;

                        for (int col = 0; col < TableSize.Width; ++col, cellRect.X += CellSize)
                        {
                            if (col < selectedSize.Width && row < selectedSize.Height)
                                e.Graphics.FillRectangle(brush, cellRect);

                            e.Graphics.DrawRectangle(pen, cellRect);
                        }
                    }
                }
            }

            using (Brush brush = new SolidBrush(textColour))
            {
                Rectangle bounds = ClientRectangle;
                bounds.Y = TableBounds.Bottom + CellSpacing;
                bounds.Height = Font.Height;

                StringFormat format = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                };

                e.Graphics.DrawString(ButtonText, Font, brush, bounds, format);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            HitInfo hit = QueryHit(e.Location);

            if (hit.part == HitPart.Button)
            {
                Capture = false;
                EndSelection();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            HitInfo hit = QueryHit(e.Location);

            SelectedSize = (hit.part == HitPart.Table) ? new Size(hit.col + 1, hit.row + 1) : new Size(0, 0);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (Capture)
            {
                Capture = false;
                EndSelection();
            }
        }

        private void EndSelection()
        {
            if (SelectedSize.Width > 0 && SelectedSize.Height > 0)
                TableControl_Selected?.Invoke(this, new TableEventArgs(selectedSize));
            else
                TableControl_Cancelled?.Invoke(this, new System.EventArgs());
        }
    }
}