using AcornPad.Internal;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AcornPad.Controls
{
    public partial class ImageBox : ScrollableControl
    {
        private AcornProject Project;

        public new event MouseEventHandler MouseWheel;

        private Color grey = Color.FromArgb(61, 61, 61);
        private Color blue = Color.FromArgb(96, 205, 255);

        public Bitmap Image { get; set; }

        public Rectangle ImageRect { get; set; }

        [DefaultValue("True")]
        public bool ShowGrid { get; set; }

        [DefaultValue("False")]
        public bool ShowTileGrid { get; set; }

        public int ScrollX(int x) => (((x + Math.Abs(AutoScrollPosition.X)) / mZoomFactor) / GridSize.Width);

        public int ScrollY(int y) => (((y + Math.Abs(AutoScrollPosition.Y)) / mZoomFactor) / GridSize.Height);

        public int ScrollTileX(int x) => (((x + Math.Abs(AutoScrollPosition.X)) / mZoomFactor) / GridTileSize.Width);

        public int ScrollTileY(int y) => (((y + Math.Abs(AutoScrollPosition.Y)) / mZoomFactor) / GridTileSize.Height);

        private int mZoomFactor;

        [DefaultValue("24")]
        public int ZoomFactor
        {
            get { return mZoomFactor; }
            set
            {
                mZoomFactor = value;

                HorizontalScroll.SmallChange = mZoomFactor;
                VerticalScroll.SmallChange = mZoomFactor;

                Invalidate();
            }
        }

        [JsonIgnore]
        [DefaultValue(typeof(Size), "1, 1")]
        public Size GridSize { get; set; }

        [JsonIgnore]
        [DefaultValue(typeof(Size), "1, 1")]
        public Size GridTileSize { get; set; }

        [DefaultValue(typeof(Size), "8,8")]
        public Size CellSize { get; set; }

        [DefaultValue(typeof(Size), "1,1")]
        public Size ImageSize { get; set; }

        public Pen PenGrid { get; set; }

        public Pen PenTileGrid { get; set; }

        public Color DrawColour { get; set; }
        public Color EraseColour { get; set; }
        public int PaintTool { get; set; }

        public int PixelSize { get; set; }

        public string PixelFormatString { get; set; }

        public ImageBox()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            ShowGrid = true;
            ShowTileGrid = false;
            ZoomFactor = 24;

            PixelSize = 1;

            CellSize = new Size(8, 8);
            ImageSize = new Size(1, 1);
            GridSize = new Size(1, 1);
            GridTileSize = new Size(1, 1);

            PenGrid = new Pen(SystemColors.ControlDarkDark)
            {
                Width = 1f,
                DashStyle = DashStyle.Dash
            };

            PenTileGrid = new Pen(blue)
            {
                Width = 2f,
                DashStyle = DashStyle.Dash
            };

            int width = ImageSize.Width * CellSize.Width * mZoomFactor;
            int height = ImageSize.Height * CellSize.Height * mZoomFactor;

            ImageRect = new Rectangle(0, 0, width, height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="se"></param>
        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //base.OnMouseWheel(e); // block wheel from using scrollbars
            MouseWheel?.Invoke(this, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);

            e.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

            if (Image != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.None;
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.CompositingMode = CompositingMode.SourceCopy;
                e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

                e.Graphics.DrawImage(Image, ImageRect);
            }

            PaintGrid(e.Graphics, ImageRect);
            PaintTileGrid(e.Graphics, ImageRect);

            base.OnPaint(e);
        }

        /// <summary>
        ///
        /// </summary>
        public new void Invalidate()
        {
            if (Image != null)
            {
                //AutoScrollMinSize = new Size(Image.Width * mZoomFactor, (Image.Height * mZoomFactor));

                int width = ImageSize.Width * PixelSize * CellSize.Width * mZoomFactor;
                int height = ImageSize.Height * CellSize.Height * mZoomFactor;

                AutoScrollMinSize = new Size(width, height);

                ImageRect = new Rectangle(0, 0, width, height);
            }

            base.Invalidate();
        }

        /// <summary>
        /// Paint Grid
        /// </summary>
        /// <param name="e"></param>
        private void PaintGrid(Graphics gfx, Rectangle rect)
        {
            if (ShowGrid)
            {
                int GridXOffset = GridSize.Width * mZoomFactor;

                for (int x = GridXOffset; x < rect.Width; x += GridXOffset)
                {
                    gfx.DrawLine(PenGrid, x, 0, x, rect.Height);
                }

                int GridYOffset = GridSize.Height * mZoomFactor;

                for (int y = GridYOffset; y < rect.Height; y += GridYOffset)
                {
                    gfx.DrawLine(PenGrid, 0, y, rect.Width, y);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="rect"></param>
        private void PaintTileGrid(Graphics gfx, Rectangle rect)
        {
            if (ShowTileGrid)
            {
                int GridXOffset = GridTileSize.Width * mZoomFactor;

                for (int x = GridXOffset; x < rect.Width; x += GridXOffset)
                {
                    gfx.DrawLine(PenTileGrid, x, 0, x, rect.Height);
                }

                int GridYOffset = GridTileSize.Height * mZoomFactor;

                for (int y = GridYOffset; y < rect.Height; y += GridYOffset)
                {
                    gfx.DrawLine(PenTileGrid, 0, y, rect.Width, y);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName"></param>
        public void Load(string fileName)
        {
            Image = (Bitmap)System.Drawing.Image.FromFile(fileName);
            ImageSize = new Size(Image.Width, Image.Height);

            PixelFormatString = Image.PixelFormat.ToString();
            Invalidate();
        }

        /// <summary>
        /// Paint Selector
        /// </summary>
        /// <param name="index"></param>
        /// <param name="colour"></param>
        /// <param name="gfx"></param>
        public void PaintSelector(int index, Color color, Graphics gfx)
        {
            if (index != -1)
            {
                Pen pen = new Pen(color)
                {
                    Width = 2f,
                    DashPattern = new float[] { 2, 1 }
                };

                int GridXOffset = GridSize.Width * ZoomFactor;
                int GridYOffset = GridSize.Height * ZoomFactor;

                int colWidth = (ImageRect.Width / GridXOffset);

                if (colWidth != 0)
                {
                    int x = (index % colWidth) * GridXOffset;
                    int y = (index / colWidth) * GridYOffset;

                    Rectangle rect = new Rectangle(x, y, GridXOffset, GridYOffset);
                    gfx.DrawRectangle(pen, rect);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="project"></param>
        public void DrawBitmapChar(AcornProject project)
        {
            Project = project;

            // ImageSize or get from project instead?
            int width = ImageSize.Width * PixelSize * project.Chars.Width;
            int height = ImageSize.Height * project.Chars.Height;

            width = width > 0 ? width : 1;
            height = height > 0 ? height : 1;

            Image = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, Image.Width, Image.Height);
            BitmapData bmpData = Image.LockBits(rect, ImageLockMode.ReadWrite, Image.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * Image.Height;
            byte[] rgbValues = new byte[bytes];

            PaintChar(0, Project.Chars.SelectedItem, bmpData.Stride, rgbValues);

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            Image.UnlockBits(bmpData);

            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="project"></param>
        public void DrawBitmapCharSet(AcornProject project)
        {
            Project = project;

            // ImageSize or get from project instead?
            int width = ImageSize.Width * PixelSize * project.Chars.Width;
            int height = ImageSize.Height * project.Chars.Height;

            width = width > 0 ? width : 1;
            height = height > 0 ? height : 1;

            Image = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, Image.Width, Image.Height);

            BitmapData bmpData = Image.LockBits(rect, ImageLockMode.ReadWrite, Image.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * Image.Height;
            byte[] rgbValues = new byte[bytes];

            ClearImage(rgbValues, SystemColors.ControlDarkDark);

            int curOffset = 0;

            for (int y = 0; y < ImageSize.Height; y++)
            {
                for (int x = 0; x < ImageSize.Width; x++)
                {
                    int cellData = y * ImageSize.Width + x;

                    if (cellData < project.Chars.Count)
                    {
                        curOffset = PaintChar(curOffset, cellData, bmpData.Stride, rgbValues);
                    }
                }
                curOffset += (7 * bmpData.Stride);
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            Image.UnlockBits(bmpData);

            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="project"></param>
        public void DrawBitmapTile(AcornProject project)
        {
            Project = project;

            // ImageSize or get from project instead?
            int width = ImageSize.Width * PixelSize * project.Chars.Width;
            int height = ImageSize.Height * project.Chars.Height;

            width = width > 0 ? width : 1;
            height = height > 0 ? height : 1;

            Image = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, Image.Width, Image.Height);
            BitmapData bmpData = Image.LockBits(rect, ImageLockMode.ReadWrite, Image.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * Image.Height;
            byte[] rgbValues = new byte[bytes];

            PaintTile(0, Project.Tiles.SelectedItem, bmpData.Stride, rgbValues);

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            Image.UnlockBits(bmpData);

            Invalidate();
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="project"></param>
        public void DrawBitmapTileSet(AcornProject project)
        {
            Project = project;

            // ImageSize or get from project instead?
            int width = ImageSize.Width * project.Tiles.Width * Project.Chars.Width * PixelSize;
            int height = ImageSize.Height * project.Tiles.Height * Project.Chars.Height;

            width = width > 0 ? width : 1;
            height = height > 0 ? height : 1;

            Image = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, Image.Width, Image.Height);

            BitmapData bmpData = Image.LockBits(rect, ImageLockMode.ReadWrite, Image.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * Image.Height;
            byte[] rgbValues = new byte[bytes];

            ClearImage(rgbValues, SystemColors.ControlDarkDark);

            int curOffset = 0;

            for (int y = 0; y < ImageSize.Height; y++)
            {
                curOffset = y * Project.Tiles.Height * Project.Chars.Width * bmpData.Stride;

                for (int x = 0; x < ImageSize.Width; x++)
                {
                    int cellData = y * ImageSize.Width + x;

                    if (cellData < project.Tiles.Count)
                    {
                        curOffset = PaintTile(curOffset, cellData, bmpData.Stride, rgbValues);
                    }
                }
                //curOffset += (((4 * 8) - 1) * bmpData.Stride);
                //curOffset += (7 * bmpData.Stride);
                //curOffset += ((project.Tiles.Height * project.Chars.Height) - 1) * bmpData.Stride;
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            Image.UnlockBits(bmpData);

            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="project"></param>
        public void DrawBitmapMap(AcornProject project)
        {
            Project = project;

            int width = ImageSize.Width * PixelSize * project.Chars.Width;
            int height = ImageSize.Height * project.Chars.Height;

            width = width > 0 ? width : 1;
            height = height > 0 ? height : 1;

            Image = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, Image.Width, Image.Height);
            BitmapData bmpData = Image.LockBits(rect, ImageLockMode.ReadWrite, Image.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * Image.Height;

            byte[] rgbValues = new byte[bytes];

            int curOffset;

            for (int y = 0; y < ImageSize.Height; y++)
            {
                curOffset = y * 8 * bmpData.Stride;

                for (int x = 0; x < ImageSize.Width; x++)
                {
                    int cellData = project.Maps.Items[project.Maps.SelectedItem].GetCellValue(x, y);

                    if (cellData != -1)
                    {
                        curOffset = PaintChar(curOffset, cellData, bmpData.Stride, rgbValues);
                    }
                }
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            Image.UnlockBits(bmpData);

            Invalidate();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="project"></param>
        public void DrawBitmapTileMap(AcornProject project)
        {
            Project = project;

            int width = ImageSize.Width * project.Tiles.Width * project.Chars.Width * PixelSize;
            int height = ImageSize.Height * project.Tiles.Height * project.Chars.Height;

            width = width > 0 ? width : 1;
            height = height > 0 ? height : 1;

            Image = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, Image.Width, Image.Height);
            BitmapData bmpData = Image.LockBits(rect, ImageLockMode.ReadWrite, Image.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * Image.Height;

            byte[] rgbValues = new byte[bytes];

            int curOffset;

            for (int y = 0; y < ImageSize.Height; y++)
            {
                curOffset = y * Project.Tiles.Height * Project.Chars.Height * bmpData.Stride;

                for (int x = 0; x < ImageSize.Width; x++)
                {
                    int cellData = project.Maps.Items[0].GetCellValue(x, y);

                    if (cellData != -1)
                    {
                        curOffset = PaintTile(curOffset, cellData, bmpData.Stride, rgbValues);
                    }
                }
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            Image.UnlockBits(bmpData);

            Invalidate();
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="item"></param>
        /// <param name="stride"></param>
        /// <param name="rgbValues"></param>
        private int PaintTile(int startIndex, int item, int stride, byte[] rgbValues)
        {
            int nextCell = startIndex + (Project.Tiles.Width * Project.Chars.Width * 4 * PixelSize);
           
            for (int y = 0; y < Project.Tiles.Height; y++)
            {
                int curOffset = startIndex + (y * Project.Chars.Width * stride);

                for (int x = 0; x < Project.Tiles.Width; x++)
                {
                    int index = y * Project.Tiles.Width + x;
                    int cellData = Project.Tiles.Items[item].Data[index];

                    curOffset = PaintChar(curOffset, cellData, stride, rgbValues);
                }
            }

            return nextCell;
        }

        /// <summary>
        /// Paint one character into a bitmap
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="item"></param>
        /// <param name="stride"></param>
        /// <param name="rgbValues"></param>
        private int PaintChar(int startIndex, int item, int stride, byte[] rgbValues)
        {
            int nextCell = startIndex + (Project.Chars.Width * 4 * PixelSize);

            for (int y = 0; y < Project.Chars.Height; y++)
            {
                int curOffset = startIndex + (y * stride);

                for (int x = 0; x < Project.Chars.Width; x++)
                {
                    int index = y * Project.Chars.Width + x;

                    if (item < Project.Chars.Count)
                    {
                        int data = Project.Chars.Items[item].Data[index];

                        Color col = Project.Palette.WinColours[data];

                        for (int w = 0; w < PixelSize; w++)
                        {
                            rgbValues[curOffset] = col.B;
                            rgbValues[curOffset + 1] = col.G;
                            rgbValues[curOffset + 2] = col.R;
                            rgbValues[curOffset + 3] = col.A;

                            curOffset += 4;
                        }
                    }
                }
            }

            return nextCell;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rgbValues"></param>
        /// <param name="col"></param>
        private void ClearImage(byte[] rgbValues, Color col)
        {
            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                rgbValues[i] = col.B;
                rgbValues[i + 1] = col.G;
                rgbValues[i + 2] = col.R;
                rgbValues[i + 3] = col.A;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public AcornProject CreateProject(AcornProject project)
        {
            Size CellSize = new Size(8, 8);

            //Map
            int width = Image.Width / CellSize.Width;
            int height = Image.Height / CellSize.Height;

            ImageData mapItem = new ImageData(0, "map", width, height);
            project.Maps.Items.Add(mapItem);

            Rectangle rect = new Rectangle(0, 0, Image.Width, Image.Height);
            BitmapData bmpData = Image.LockBits(rect, ImageLockMode.ReadWrite, Image.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * Image.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Add blank as first character
            ImageData blank = new ImageData()
            {
                Id = 0,
                Name = "char_sprite",
                Width = CellSize.Width,
                Height = CellSize.Height,
                Data = new int[CellSize.Width * CellSize.Height]
            };
            project.Chars.Items.Add(blank);

            int index = 1;

            for (int y = 0; y < Image.Height; y += CellSize.Height)
            {
                for (int x = 0; x < Image.Width; x += CellSize.Width)
                {
                    ImageData itm = new ImageData()
                    {
                        Id = index,
                        Name = "char_sprite",
                        Width = CellSize.Width,
                        Height = CellSize.Height,
                        Data = CutTile(project.Palette, bmpData, rgbValues, x, y)
                    };

                    project.Chars.Items.Add(itm);

                    int tmp = (y / 8) * width + (x / 8);

                    if (tmp < project.Maps.Items[0].Data.Length)
                    {
                        project.Maps.Items[0].Data[tmp] = index;
                    }

                    index++;
                }
            }

            Image.UnlockBits(bmpData);

            Image.Dispose();

            return project;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bmpData"></param>
        /// <param name="rgbValues"></param>
        /// <param name="tx"></param>
        /// <param name="ty"></param>
        /// <returns></returns>
        private int[] CutTile(Palette pal, BitmapData bmpData, byte[] rgbValues, int tx, int ty)
        {
            int[] pixels = new int[64];

            int counter = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    int px = tx + x;
                    int py = ty + y;
                    int ptr = (py * bmpData.Stride + px * 4);

                    int pixel = 0;

                    if (ptr < rgbValues.Length)
                    {
                        int b = rgbValues[ptr];
                        int g = rgbValues[ptr + 1];
                        int r = rgbValues[ptr + 2];
                        int a = rgbValues[ptr + 3];

                        Color col = Color.FromArgb(a, r, g, b);

                        pixel = pal.GetColour(col);
                    }

                    pixels[counter++] = (byte)pixel;
                }
            }
            return pixels;
        }
    }
}