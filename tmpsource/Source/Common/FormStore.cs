using System.Drawing;

namespace AcornPad.Common
{
    public class FormStore
    {
        /// <summary>
        ///
        /// </summary>
        public int ZoomFactor { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        ///
        /// </summary>
        public FormStore()
        {
            Initialize(1, 10, 10, 400, 400);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public FormStore(int zoomFactor, int x, int y)
        {
            Initialize(zoomFactor, x, y, 400, 400);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="zoomFactor"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public FormStore(int zoomFactor, int x, int y, int width, int height)
        {
            Initialize(zoomFactor, x, y, width, height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void Initialize(int zoomFactor, int x, int y, int width, int height)
        {
            ZoomFactor = zoomFactor;
            Location = new Point(x, y);
            Size = new Size(width, height);
        }
    }
}