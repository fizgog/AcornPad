namespace AcornPad
{
    public class Machine
    {
        /// <summary>
        ///
        /// </summary>
        public string MachineType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int NumColours { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int BitsPerPixel { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int PixelSize { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int PixelsBerByte => 8 / (BitsPerPixel != 0 ? BitsPerPixel : 1);

        /// <summary>
        ///
        /// </summary>
        public string ShortDesc { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int TextWidth => Width / 8;

        /// <summary>
        ///
        /// </summary>
        public int TextHeight => Height / 8;
    }
}