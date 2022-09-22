using System;
using System.Drawing.Imaging;

namespace AcornPad
{
    public static class Helper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(this object obj)
        {
            bool result = false;

            if (obj != null && !bool.TryParse(obj.ToString(), out result))
                result = false;

            return result;
        }

        /// <summary>
        /// Converts the object value of this instance to its equivalent integer representation.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Converted object or default value of 0</returns>
        public static int ToInteger(this object obj)
        {
            int result = 0;

            if (obj != null && !int.TryParse(obj.ToString(), out result))
                result = 0;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this object obj)
        {
            Type enumType = typeof(T);

            return (T)Enum.Parse(enumType, obj.ToString());
        }

        /// <summary>
        /// Converts the object value of this instance to its equivalent binary representation.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Converted object or default value of 00000000</returns>
        public static string ToBinary(this Object obj)
        {
            string result = string.Empty;

            if (obj != null)
            {
                result = Convert.ToString(obj.ToInteger(), 2).PadLeft(8, '0');
            }

            return result;
        }

        /// <summary>
        /// Converts the object value of this instance to its equivalent hex representation.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Converted object or default value of 0</returns>
        public static string ToDecimal(this Object obj, int digits)
        {
            string result = string.Empty;

            if (obj != null)
            {
                result = Convert.ToString(obj.ToInteger()).PadLeft(digits, ' ');
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int FromBinary(this object obj)
        {
            int result = 0;

            if (obj != null)
            {
                result = Convert.ToInt32(obj.ToString().Replace("%", ""), 2);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int FromHex(this object obj)
        {
            int result = 0;

            if (obj != null)
            {
                result = Convert.ToInt32(obj.ToString().Replace("$", "").Replace("&", ""), 16);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string ToHex(this Object obj)
        {
            string result = string.Empty;

            if (obj != null)
            {
                result = string.Format("{0:X2}", obj.ToInteger());
            }

            return result;
        }

        /// <summary>
        /// byte[] is implicitly convertible to ReadOnlySpan<byte>
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        public static bool IntArrayCompare(ReadOnlySpan<int> a1, ReadOnlySpan<int> a2)
        {
            return a1.SequenceEqual(a2);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static string GetImageFilter()
        {
            string result = string.Empty;

            string sep = string.Empty;
            foreach (var codec in ImageCodecInfo.GetImageEncoders())
            {
                string codecName = codec.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                result = string.Format("{0}{1}{2} ({3})|{3}", result, sep, codecName, codec.FilenameExtension);
                sep = "|";
            }
            result = string.Format("{0}{1}{2} ({3})|{3}", result, sep, "All Files", "*.*");

            result = "All images|*.bmp;*.dib;*.rle;*.jpg;*.jpeg;*.jpe;*.jfif;*.gif;*.tif;*.png|" + result;

            return result;
        }
    }
}