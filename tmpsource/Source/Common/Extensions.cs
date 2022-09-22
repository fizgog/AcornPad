using AcornPad.Internal;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;

namespace AcornPad
{
    internal static class Extensions
    {
        

        public static bool IsUserScoped(this Properties.Settings settings, string name)
        {
            PropertyInfo pi = settings.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public);

            return pi.GetCustomAttribute<UserScopedSettingAttribute>() != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size"></param>
        public static Bitmap ResizeImage(this Bitmap imgToResize, Size size)
        {
            int width = imgToResize.Width < size.Width ? imgToResize.Width : size.Width;
            int height = imgToResize.Height < size.Height ? imgToResize.Height : size.Height;

            var destinationRect = new Rectangle(0, 0, width, height);
            var destinationImage = new Bitmap(size.Width, size.Height);

            using (var graphics = Graphics.FromImage(destinationImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.DrawImage(imgToResize, destinationRect, 0, 0, imgToResize.Width, imgToResize.Height, GraphicsUnit.Pixel);
            }

            return destinationImage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="palette"></param>
        /// <param name="conv"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <returns></returns>
        public static int[] CountPalette(this Bitmap Image, Palette palette, int conv, int xOffset, int yOffset, int saturation, int brightness)
        {
            int[] colourCount = new int[16];

            RectangleF cloneRect = new RectangleF(xOffset, yOffset, Image.Width - xOffset, Image.Height - yOffset);

            //Bitmap newImage = Image.Clone(cloneRect, Image.PixelFormat);
            Bitmap cloneImage = Image.Clone(cloneRect, Image.PixelFormat);

            Bitmap newImage = new Bitmap(cloneImage);

            Rectangle rect = new Rectangle(0, 0, newImage.Width, newImage.Height);
            BitmapData bmpData = newImage.LockBits(rect, ImageLockMode.ReadWrite, newImage.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * newImage.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int counter = 0; counter < rgbValues.Length; counter += 4)
            {
                int b = rgbValues[counter];
                int g = rgbValues[counter + 1];
                int r = rgbValues[counter + 2];
                int a = rgbValues[counter + 3];

                Color tst = Color.FromArgb(a, r, g, b);

                Color col;

                switch (conv)
                {
                    case 1: col = palette.FindClosestRGBColour(tst); break;
                    case 2: col = palette.FindClosestHueColour(tst); break;
                    case 3: col = palette.FindClosestSatBrightColour(tst, saturation,brightness); break;
                    default: throw new Exception("Invalid conversion type");
                }

                int index = palette.GetAcornColour(col);

                colourCount[index]++;
            }

            // Unlock the bits.
            newImage.UnlockBits(bmpData);
            newImage.Dispose();

            return colourCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="palette"></param>
        /// <param name="conv"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <returns></returns>
        public static Bitmap ImageToAcorn(this Bitmap Image, Palette palette, int conv, int xOffset, int yOffset, int saturation, int brightness)
        {
            RectangleF cloneRect = new RectangleF(xOffset, yOffset, Image.Width - xOffset, Image.Height - yOffset);

            Bitmap cloneImage = Image.Clone(cloneRect, Image.PixelFormat);

            Bitmap newImage = new Bitmap(cloneImage);

            Rectangle rect = new Rectangle(0, 0, newImage.Width, newImage.Height);
            BitmapData bmpData = newImage.LockBits(rect, ImageLockMode.ReadWrite, newImage.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * newImage.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int counter = 0; counter < rgbValues.Length; counter += 4)
            {
                int b = rgbValues[counter];
                int g = rgbValues[counter + 1];
                int r = rgbValues[counter + 2];
                int a = rgbValues[counter + 3];

                Color tst = Color.FromArgb(a, r, g, b);

                Color col;

                switch (conv)
                {
                    case 1: col = palette.FindClosestRGBColour(tst); break;
                    case 2: col = palette.FindClosestHueColour(tst); break;
                    case 3: col = palette.FindClosestSatBrightColour(tst, saturation,brightness); break;
                    default: throw new Exception("Invalid conversion type");
                }

                rgbValues[counter] = col.B;
                rgbValues[counter + 1] = col.G;
                rgbValues[counter + 2] = col.R;
                rgbValues[counter + 3] = col.A;
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            newImage.UnlockBits(bmpData);

            return newImage;
        }
    }
}