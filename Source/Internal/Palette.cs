using AcornPad.Common;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Linq;

namespace AcornPad.Internal
{
    [Serializable]
    public class Palette
    {
        private const int MAX_ATOM_COLOURS = 4;
        private const int MAX_BEEB_COLOURS = 16;

        /// <summary>
        /// Number of colours used
        /// </summary>
        public int NumColours { get; set; }

        /// <summary>
        /// Current drawing colour
        /// </summary>
        public int DrawColour { get; set; }

        /// <summary>
        /// Current erasing colour
        /// </summary>
        public int EraseColour { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ColourSet { get; set; }

        /// <summary>
        /// Version 1.02 property - remove when people are on version >= 1.04
        /// Replaced with AcornColourSet1
        /// </summary>
        [JsonIgnore]
        public AcornColourType[] BeebColours { get; }

        [JsonProperty(nameof(BeebColours))]
        private AcornColourType[] BeebColoursSetter
        {
            set => AcornColourSet1 = value;
        }

        /// <summary>
        /// BBC colour palette
        /// </summary>
        public AcornColourType[] AcornColourSet1 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public AcornColourType[] AcornColourSet2 { get; set; }

        /// <summary>
        /// Windows colour palette
        /// </summary>
        public Color[] WinColours { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public int GetColour(Color col) => GetAcornColour(col);

        /// <summary>
        /// Get windows colour
        /// </summary>
        [JsonIgnore]
        public Color GetDrawColour => GetWindowsColour(ColourSet == 1 ? AcornColourSet1[DrawColour] : AcornColourSet2[DrawColour]);

        /// <summary>
        /// Get windows colour
        /// </summary>
        [JsonIgnore]
        public Color GetEraseColour => GetWindowsColour(ColourSet == 1 ? AcornColourSet1[EraseColour] : AcornColourSet2[EraseColour]);

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public AcornColourType[] GetAcornColourSet => ColourSet == 1 ? AcornColourSet1 : AcornColourSet2;

        public Palette(MachineType machineType)
        {
            switch (machineType)
            {
                case MachineType.Atom:
                    Initialize(machineType, MAX_ATOM_COLOURS);
                    break;

                case MachineType.BBC:
                    Initialize(machineType, MAX_BEEB_COLOURS);
                    break;

                default: throw new Exception("Unknown Machine Type");
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Palette(MachineType machineType, int numColours)
        {
            Initialize(machineType, numColours);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="machineType"></param>
        /// <param name="numColours"></param>
        private void Initialize(MachineType machineType, int numColours)
        {
            NumColours = numColours;

            ColourSet = 1;
            DrawColour = 1;
            EraseColour = 0;

            AcornColourSet1 = new AcornColourType[numColours];
            AcornColourSet2 = new AcornColourType[numColours];

            WinColours = new Color[numColours];

            SetColourMode(machineType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="acornColourType"></param>
        /// <returns></returns>
        public Color GetWindowsColour(AcornColourType acornColourType)
        {
            switch (acornColourType)
            {
                case AcornColourType.Black: return Color.FromArgb(0, 0, 0);
                case AcornColourType.Red: return Color.FromArgb(255, 0, 0);
                case AcornColourType.Green: return Color.FromArgb(0, 255, 0);
                case AcornColourType.Yellow: return Color.FromArgb(255, 255, 0);
                case AcornColourType.Blue: return Color.FromArgb(0, 0, 255);
                case AcornColourType.Magenta: return Color.FromArgb(255, 0, 255);
                case AcornColourType.Cyan: return Color.FromArgb(0, 255, 255);
                case AcornColourType.White: return Color.FromArgb(255, 255, 255);
                case AcornColourType.Orange: return Color.FromArgb(255, 165, 0);
                default: throw new Exception("Unknown Acorn Colour Type");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public int GetAcornColour(Color col)
        {
            for (int i = 0; i < NumColours; i++)
            {
                if (col == WinColours[i]) return i;
            }

            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        public void SetColourMode(MachineType mType)
        {
            switch (mType)
            {
                case MachineType.Atom:
                    SetAtomColourMode();
                    break;

                case MachineType.BBC:
                    SetBeebColourMode();
                    break;

                default:
                    throw new Exception("Invalid Machine Type");
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void SetAtomColourMode()
        {
            switch (NumColours)
            {
                case 4:
                    // Colour Set 1
                    AcornColourSet1[0] = AcornColourType.Green;
                    AcornColourSet1[1] = AcornColourType.Yellow;
                    AcornColourSet1[2] = AcornColourType.Blue;
                    AcornColourSet1[3] = AcornColourType.Red;
                    // Colour Set 2
                    AcornColourSet2[0] = AcornColourType.White;
                    AcornColourSet2[1] = AcornColourType.Cyan;
                    AcornColourSet2[2] = AcornColourType.Magenta;
                    AcornColourSet2[3] = AcornColourType.Orange;

                    break;

                case 2:
                    // Colour Set 1
                    AcornColourSet1[0] = AcornColourType.Black;
                    AcornColourSet1[1] = AcornColourType.Green;
                    // Colour Set 2
                    AcornColourSet2[0] = AcornColourType.Green;
                    AcornColourSet2[1] = AcornColourType.Yellow;

                    break;

                default: throw new Exception("Unknown number of Atom colours");
            }

            AcornColourType[] acornColourSet = ColourSet == 1 ? AcornColourSet1 : AcornColourSet2;
            for (int i = 0; i < NumColours; i++)
            {
                Color col = GetWindowsColour(acornColourSet[i]);
                WinColours[i] = col;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void SetBeebColourMode()
        {
            switch (NumColours)
            {
                case 16:
                    AcornColourSet1[0] = AcornColourType.Black;
                    AcornColourSet1[1] = AcornColourType.Red;
                    AcornColourSet1[2] = AcornColourType.Green;
                    AcornColourSet1[3] = AcornColourType.Yellow;
                    AcornColourSet1[4] = AcornColourType.Blue;
                    AcornColourSet1[5] = AcornColourType.Magenta;
                    AcornColourSet1[6] = AcornColourType.Cyan;
                    AcornColourSet1[7] = AcornColourType.White;
                    AcornColourSet1[8] = AcornColourType.Black;
                    AcornColourSet1[9] = AcornColourType.Red;
                    AcornColourSet1[10] = AcornColourType.Green;
                    AcornColourSet1[11] = AcornColourType.Yellow;
                    AcornColourSet1[12] = AcornColourType.Blue;
                    AcornColourSet1[13] = AcornColourType.Magenta;
                    AcornColourSet1[14] = AcornColourType.Cyan;
                    AcornColourSet1[15] = AcornColourType.White;
                    break;

                case 4:
                    AcornColourSet1[0] = AcornColourType.Black;
                    AcornColourSet1[1] = AcornColourType.Red;
                    AcornColourSet1[2] = AcornColourType.Yellow;
                    AcornColourSet1[3] = AcornColourType.White;
                    break;

                case 2:
                    AcornColourSet1[0] = AcornColourType.Black;
                    AcornColourSet1[1] = AcornColourType.White;
                    break;

                default: throw new Exception("Unknown number of BBC colours");
            }

            for (int i = 0; i < NumColours; i++)
            {
                Color col = GetWindowsColour(AcornColourSet1[i]);
                WinColours[i] = col;
            }
        }

        /// <summary>
        /// Closest match in RGB space
        /// </summary>
        /// <param name="colors"></param>
        /// <param name="origCol"></param>
        /// <returns></returns>
        public Color FindClosestRGBColour(Color origCol)
        {
            var colorDiffs = WinColours.ToList().Select(n => ColorDiff(n, origCol)).Min(n => n);
            int col = WinColours.ToList().FindIndex(n => ColorDiff(n, origCol) == colorDiffs);

            AcornColourType[] acornColourSet = ColourSet == 1 ? AcornColourSet1 : AcornColourSet2;

            return GetWindowsColour(acornColourSet[col]);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="origCol"></param>
        /// <returns></returns>
        public Color FindClosestHueColour(Color origCol)
        {
            var hue1 = origCol.GetHue();
            var diffs = WinColours.ToList().Select(n => GetHueDistance(n.GetHue(), hue1));
            var diffMin = diffs.Min(n => n);
            int col = diffs.ToList().FindIndex(n => n == diffMin);

            AcornColourType[] acornColourSet = ColourSet == 1 ? AcornColourSet1 : AcornColourSet2;

            return GetWindowsColour(acornColourSet[col]);
        }

        /// <summary>
        /// Weighed distance using hue, saturation and brightness
        /// </summary>
        /// <param name="origCol"></param>
        /// <returns></returns>
        public Color FindClosestSatBrightColour(Color origCol, int saturation, int brightness)
        {
            float hue1 = origCol.GetHue();
            var num1 = GetWeightedSatBright(origCol,saturation,brightness);
            var diffs = WinColours.ToList().Select(n => Math.Abs(GetWeightedSatBright(n,saturation, brightness) - num1) + GetHueDistance(n.GetHue(), hue1));
            var diffMin = diffs.Min(x => x);
            int col = diffs.ToList().FindIndex(n => n == diffMin);

            AcornColourType[] acornColourSet = ColourSet == 1 ? AcornColourSet1 : AcornColourSet2;

            return GetWindowsColour(acornColourSet[col]);
        }

        /// <summary>
        /// Weighed only by saturation and brightness
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        private int ColorDiff(Color c1, Color c2)
        {
            return (int)Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R)
                                   + (c1.G - c2.G) * (c1.G - c2.G)
                                   + (c1.B - c2.B) * (c1.B - c2.B));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private float GetBrightness(Color c)
        {
            return (c.R * 0.299f + c.G * 0.587f + c.B * 0.114f) / 256f;
        }

        /// <summary>
        /// distance between two hues:
        /// </summary>
        /// <param name="hue1"></param>
        /// <param name="hue2"></param>
        /// <returns></returns>
        private float GetHueDistance(float hue1, float hue2)
        {
            float d = Math.Abs(hue1 - hue2); return d > 180 ? 360 - d : d;
        }

        /// <summary>
        /// Weighed only by saturation and brightness
        /// </summary>
        /// <param name="c"></param>
        /// <param name="saturation"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        private float GetWeightedSatBright(Color c, int saturation, int brightness)
        {
            return c.GetSaturation() * saturation + GetBrightness(c) * brightness;
        }
        
    }
}