using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace AcornPad.Common
{
    public static class Sys
    {
        public static Color[] palette;

        /// <summary>
        ///
        /// </summary>
        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static string AssemblyVersion
        {
            get
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static List<Machine> GetMachineList()
        {
            List<Machine> machineList = new List<Machine>();

            // Load json file into machine array
            using (StreamReader r = new StreamReader("Machine.json"))
            {
                string json = r.ReadToEnd();
                machineList = JsonConvert.DeserializeObject<List<Machine>>(json);
            }

            return machineList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="machineType"></param>
        /// <returns></returns>
        public static List<Machine> GetMachineList(string machineType)
        {
            return GetMachineList().FindAll(x => x.MachineType == machineType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="digits"></param>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static string Format(int value, int digits, ConversionType conversion)
        {
            switch (conversion)
            {
                case ConversionType.Decimal:
                    return value.ToDecimal(digits);

                case ConversionType.Hexidecimal:
                    return "$" + value.ToHex();

                case ConversionType.Binary:
                    return "%" + value.ToBinary();

                default: throw new Exception("Invalid conversion");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static int CalcPadding(int columns, ConversionType conversion)
        {
            switch (conversion)
            {
                case ConversionType.Decimal:
                    return 8 + (5 * columns);

                case ConversionType.Hexidecimal:
                    return 8 + (5 * columns);

                case ConversionType.Binary:
                    return 8 + (11 * columns);

                default: throw new Exception("Invalid conversion");
            }
        }

        /// <summary>
        /// Compress map to run length encoding
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int[] Compress(int width, int height, int[] Data)
        {
            List<int> tmpData = new List<int>();

            for (int y = 0; y < height; y++)
            {
                int counter = 0;
                int oldValue = -1;
                int newValue;

                for (int x = 0; x < width; x++)
                {
                    newValue = Data[y * width + x];

                    if (newValue != oldValue)
                    {
                        if (oldValue != -1)
                        {
                            tmpData.Add(counter);
                        }
                        tmpData.Add(newValue);
                        oldValue = newValue;
                        counter = 0;
                    }
                    counter++;
                }

                tmpData.Add(counter);
            }

            return tmpData.ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="bitsPerPixel"></param>
        /// <param name="pixelsPerByte"></param>
        /// <returns></returns>
        public static int GetBeebColour(int colour, int bitsPerPixel, int pixelsPerByte)
        {
            int beebByte = 0;
            for (int i = 0; i < bitsPerPixel; i++)
            {
                if ((colour & (1 << i)) != 0)
                {
                    beebByte |= (byte)(1 << (i * pixelsPerByte));
                }
            }

            return beebByte;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bitsPerPixel"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int[] ConvertToColumn(int bitsPerPixel, int[] Data)
        {
            int pixelsPerByte = 8 / bitsPerPixel;

            int Count = Data.Length;

            int[] result = new int[Count / pixelsPerByte];

            int counter = 0;

            for (int i = 0; i < Count; i += pixelsPerByte)
            {
                int beeb = 0;

                for (int x = 0; x < pixelsPerByte; x++)
                {
                    int pixel = GetBeebColour(Data[i + x], bitsPerPixel, pixelsPerByte);
                    beeb |= (pixel << (pixelsPerByte - 1 - x));
                }
                result[counter++] = beeb;
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bitsPerPixel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int[] ConvertToRow(int bitsPerPixel, int[] data)
        {
            // Convert data iinto Beeb column
            int[] tmp = Sys.ConvertToColumn(bitsPerPixel, data);

            int[] result = new int[tmp.Length];

            int counter = 0;

            // Convert column to row
            for (int x = 0; x < bitsPerPixel; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    result[counter++] = tmp[y * bitsPerPixel + x];
                }
            }

            return result;
        }

        /// <summary>
        /// TODO Move to sys
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="selectedItem"></param>
        /// <param name="mode"></param>
        /// <param name="charName"></param>
        /// <param name="columns"></param>
        /// <param name="digits"></param>
        /// <param name="layout"></param>
        /// <param name="comments"></param>
        /// <param name="conversion"></param>
        public static void GenerateChar(AcornProject Project, AcornPad.Controls.RichTextBox rtb, int selectedItem, ExportFormatType format, string charName, int columns, int digits, LayoutType layout, bool comments, ConversionType conversion)
        {
            rtb.AppendText(string.Format("{0}{1}_{2}", format == ExportFormatType.BeebASM ? "." : "", charName, selectedItem));

            int bitsPerByte = Project.Machine.BitsPerPixel;

            int[] beeb;

            switch (layout)
            {
                case LayoutType.Row:
                    beeb = Sys.ConvertToRow(bitsPerByte, Project.Chars.Items[selectedItem].Data);
                    break;

                case LayoutType.Column:
                    beeb = Sys.ConvertToColumn(bitsPerByte, Project.Chars.Items[selectedItem].Data);
                    break;

                default:
                    throw new Exception("Invalid layout.");
            }

            int commentLine = 0;

            for (int i = 0; i < beeb.Length; i++)
            {
                if (i % columns == 0)
                {
                    if (i != 0 && comments)
                        PrintCommentChar(Project, rtb, selectedItem, commentLine++, 1, i == commentLine);

                    rtb.AppendText(Environment.NewLine);
                    rtb.AppendText(string.Format("    {0} ", format == ExportFormatType.BeebASM ? "EQUB" : ".byte"));
                }
                else
                {
                    rtb.AppendText(", ");
                }

                rtb.AppendText(Sys.Format(beeb[i], digits, conversion));
            }

            int padding = Sys.CalcPadding(columns, conversion);

            if (comments)
            {
                for (int i = commentLine; i < 8; i++)
                {
                    bool first = (i == commentLine);
                    PrintCommentChar(Project, rtb, selectedItem, i, padding, first);
                    rtb.AppendText(Environment.NewLine);
                }
            }

            rtb.AppendText(Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Project"></param>
        /// <param name="rtb"></param>
        /// <param name="selectedItem"></param>
        /// <param name="columns"></param>
        /// <param name="digits"></param>
        /// <param name="comments"></param>
        /// <param name="conversion"></param>
        public static void GenerateTile(AcornProject Project, AcornPad.Controls.RichTextBox rtb, int selectedItem, ExportFormatType format, int columns, int digits, bool comments, ConversionType conversion)
        {
            rtb.AppendText(Environment.NewLine);
            rtb.AppendText(string.Format("{0}Tile_{1}", format == ExportFormatType.BeebASM ? "." : "", selectedItem));

            int width = Project.Tiles.Items[selectedItem].Width;
            int height = Project.Tiles.Items[selectedItem].Height;

            int[] test = Project.Tiles.Items[selectedItem].Data;

            int commentLine = 0;

            for (int i = 0; i < test.Length; i++)
            {
                if (i % columns == 0)
                {
                    if (i != 0 && comments)
                        PrintCommentTile(Project, rtb, selectedItem, commentLine++, width, height);

                    rtb.AppendText(Environment.NewLine);
                    rtb.AppendText(string.Format("    {0} ", format == ExportFormatType.BeebASM ? "EQUB" : ".byte"));
                }
                else
                {
                    rtb.AppendText(", ");
                }
                rtb.AppendText(Sys.Format(test[i], digits, conversion));
            }

            rtb.AppendText(Environment.NewLine);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="selectedItem"></param>
        /// <param name="columns"></param>
        /// <param name="digits"></param>
        /// <param name="comments"></param>
        /// <param name="conversion"></param>
        /// <param name="compression"></param>
        public static void GenerateMap(AcornProject Project, AcornPad.Controls.RichTextBox rtb, int selectedItem, ExportFormatType format, int columns, int digits, bool comments, ConversionType conversion, bool compression)
        {
            rtb.AppendText(Environment.NewLine);
            rtb.AppendText(string.Format("{0}Map_{1}", format == ExportFormatType.BeebASM ? "." : "", selectedItem));

            int width = Project.Maps.Items[selectedItem].Width;
            int height = Project.Maps.Items[selectedItem].Height;

            int[] test = (compression) ? Sys.Compress(width, height, Project.Maps.Items[0].Data) : Project.Maps.Items[selectedItem].Data;

            if (comments)
            {
                rtb.AppendText(Environment.NewLine);
                rtb.AppendText(string.Format("    {0} ", format == ExportFormatType.BeebASM ? "EQUB" : ".byte"));
                rtb.AppendText(Sys.Format(Project.Maps.Items[selectedItem].Width, digits, conversion));
                rtb.AppendText(", ");
                rtb.AppendText(Sys.Format(Project.Maps.Items[selectedItem].Height, digits, conversion));
                rtb.AppendText(", ");
                rtb.AppendText(Sys.Format(Project.Maps.Items[selectedItem].Count, digits, conversion));

                if (compression)
                {
                    rtb.AppendText(", ");
                    rtb.AppendText(Sys.Format(test.Length, digits, conversion));
                }

                rtb.AppendText(" ; Width, Height, Size");

                if (compression)
                {
                    rtb.AppendText(", Compressed");
                }
            }

            int commentLine = 0;

            for (int i = 0; i < test.Length; i++)
            {
                if (i % columns == 0)
                {
                    if (i != 0 && comments)
                        PrintCommentMap(Project, rtb, selectedItem, commentLine++, width, height);

                    rtb.AppendText(Environment.NewLine);
                    rtb.AppendText(string.Format("    {0} ", format == ExportFormatType.BeebASM ? "EQUB" : ".byte"));
                }
                else
                {
                    rtb.AppendText(", ");
                }
                rtb.AppendText(Sys.Format(test[i], digits, conversion));
            }

            rtb.AppendText(Environment.NewLine);
        }

        /// <summary>
        /// y is index into data start
        /// lineCounter
        /// </summary>
        /// <param name="y"></param>
        /// <param name="lineCounter"></param>
        public static void PrintCommentChar(AcornProject Project, AcornPad.Controls.RichTextBox rtb, int selectedItem, int lineCounter, int padding, bool first)
        {
            int width = 8;
            int height = 8;
            int y = lineCounter;

            padding = first ? 1 : padding;

            if (lineCounter < height)
            {
                // Print one line of comments
                rtb.AppendText(" ".PadLeft(padding));
                rtb.AppendText("; ");

                for (int x = 0; x < width; x++)
                {
                    int index = (y * width) + x;
                    if (index < Project.Chars.Items[selectedItem].Count)
                    {
                        string str = string.Format("{0}", Project.Chars.Items[selectedItem].Data[index] > 0 ? "#" : ".");
                        rtb.AppendText(str, Project.Palette.WinColours[Project.Chars.Items[selectedItem].Data[index]]);
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="lineCounter"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="selectedItem"></param>
        public static void PrintCommentTile(AcornProject Project, AcornPad.Controls.RichTextBox rtb, int selectedItem, int lineCounter, int width, int height)
        {
            int y = lineCounter;

            if (lineCounter < height)
            {
                // Print one line of comments
                rtb.AppendText("; ");

                for (int x = 0; x < width; x++)
                {
                    int index = (y * width) + x;
                    if (index < Project.Tiles.Items[selectedItem].Data.Length)
                    {
                        string str = string.Format("{0}", Project.Tiles.Items[selectedItem].Data[index] > 0 ? "#" : ".");
                        rtb.AppendText(str);
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="lineCounter"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="selectedItem"></param>
        public static void PrintCommentMap(AcornProject Project, AcornPad.Controls.RichTextBox rtb, int selectedItem, int lineCounter, int width, int height)
        {
            int y = lineCounter;

            if (lineCounter < height)
            {
                // Print one line of comments
                rtb.AppendText("; ");

                for (int x = 0; x < width; x++)
                {
                    int index = (y * width) + x;
                    if (index < Project.Maps.Items[selectedItem].Data.Length)
                    {
                        string str = string.Format("{0}", Project.Maps.Items[selectedItem].Data[index] > 0 ? "#" : ".");
                        rtb.AppendText(str);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitsPerPixel"></param>
        /// <param name="charByte"></param>
        /// <returns></returns>
        public static List<int> Unpack_BBC_Byte(int bitsPerPixel, int charByte)
        {
            List<int> bits = new List<int>();

            // 8 bits
            if (bitsPerPixel == 1)
            {
                bits.Add((charByte >> 7) & 0x01);
                bits.Add((charByte >> 6) & 0x01);
                bits.Add((charByte >> 5) & 0x01);
                bits.Add((charByte >> 4) & 0x01);
                bits.Add((charByte >> 3) & 0x01);
                bits.Add((charByte >> 2) & 0x01);
                bits.Add((charByte >> 1) & 0x01);
                bits.Add((charByte) & 0x01);
            }

            // 4 bits
            else if (bitsPerPixel == 2)
            {
                bits.Add(((charByte >> 6) & 0x02) | ((charByte >> 3) & 0x01));
                bits.Add(((charByte >> 5) & 0x02) | ((charByte >> 2) & 0x01));
                bits.Add(((charByte >> 4) & 0x02) | ((charByte >> 1) & 0x01));
                bits.Add(((charByte >> 3) & 0x02) | ((charByte) & 0x01));
            }

            // 2 bits
            else if (bitsPerPixel == 4)
            {
                bits.Add(((charByte >> 4) & 0x08) | ((charByte >> 3) & 0x04) | ((charByte >> 2) & 0x02) | ((charByte >> 1) & 0x01));
                bits.Add(((charByte >> 3) & 0x08) | ((charByte >> 2) & 0x04) | ((charByte >> 1) & 0x02) | (charByte & 0x01));
            }

            return bits;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitsPerPixel"></param>
        /// <param name="charByte"></param>
        /// <returns></returns>
        public static List<int> Unpack_Atom_Byte(int bitsPerPixel, int charByte)
        {
            List<int> bits = new List<int>();

            if (bitsPerPixel == 1)
            {
                bits.Add((charByte >> 7) & 0x01);
                bits.Add((charByte >> 6) & 0x01);
                bits.Add((charByte >> 5) & 0x01);
                bits.Add((charByte >> 4) & 0x01);
                bits.Add((charByte >> 3) & 0x01);
                bits.Add((charByte >> 2) & 0x01);
                bits.Add((charByte >> 1) & 0x01);
                bits.Add((charByte) & 0x01);
            }
            else if (bitsPerPixel == 2)
            {
                bits.Add((charByte >> 6) & 0x03);
                bits.Add((charByte >> 4) & 0x03);
                bits.Add((charByte >> 2) & 0x03);
                bits.Add((charByte) & 0x03);
            }

            return bits;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Color GetColour(int index)
        {
            return palette[index];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="charByte"></param>
        /// <returns></returns>
        public static List<int> Unpack_BBC_Byte(Machine machine, int charByte)
        {
            List<int> bits = new List<int>();
                        
            // 8 bits
            if (machine.PixelsBerByte == 8) //(machine.Mode == "0" || machine.Mode == "4")
            {
                bits.Add((charByte >> 7) & 0x01);
                bits.Add((charByte >> 6) & 0x01);
                bits.Add((charByte >> 5) & 0x01);
                bits.Add((charByte >> 4) & 0x01);
                bits.Add((charByte >> 3) & 0x01);
                bits.Add((charByte >> 2) & 0x01);
                bits.Add((charByte >> 1) & 0x01);
                bits.Add((charByte) & 0x01);
            }

            // 4 bits
            else if (machine.PixelsBerByte == 4) //(machine.Mode == "1" || machine.Mode == "5")
            {
                bits.Add(((charByte >> 6) & 0x02) | ((charByte >> 3) & 0x01));
                bits.Add(((charByte >> 5) & 0x02) | ((charByte >> 2) & 0x01));
                bits.Add(((charByte >> 4) & 0x02) | ((charByte >> 1) & 0x01));
                bits.Add(((charByte >> 3) & 0x02) | ((charByte) & 0x01));
            }

            // 2 bits
            else if (machine.PixelsBerByte == 2) //(machine.Mode == "2")
            {
                bits.Add(((charByte >> 4) & 0x08) | ((charByte >> 3) & 0x04) | ((charByte >> 2) & 0x02) | ((charByte >> 1) & 0x01));
                bits.Add(((charByte >> 3) & 0x08) | ((charByte >> 2) & 0x04) | ((charByte >> 1) & 0x02) | (charByte & 0x01));
            }

            return bits;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="charByte"></param>
        /// <returns></returns>
        public static List<int> Unpack_Atom_Byte(Machine machine, int charByte)
        {
            List<int> bits = new List<int>();

            if (machine.BitsPerPixel == 8)
            {
                bits.Add((charByte >> 7) & 0x01);
                bits.Add((charByte >> 6) & 0x01);
                bits.Add((charByte >> 5) & 0x01);
                bits.Add((charByte >> 4) & 0x01);
                bits.Add((charByte >> 3) & 0x01);
                bits.Add((charByte >> 2) & 0x01);
                bits.Add((charByte >> 1) & 0x01);
                bits.Add((charByte) & 0x01);
            }
            else if (machine.BitsPerPixel == 4)
            {
                bits.Add((charByte >> 6) & 0x03);
                bits.Add((charByte >> 4) & 0x03);
                bits.Add((charByte >> 2) & 0x03);
                bits.Add((charByte) & 0x03);
            }

            return bits;
        }

    }
}