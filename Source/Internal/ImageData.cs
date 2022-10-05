using Newtonsoft.Json;
using System;

namespace AcornPad
{
    [Serializable]
    public class ImageData : ICloneable
    {
        /// <summary>
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public string DisplayName => Name + (Id + 1).ToString();

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
        public int Count => Data.Length;

        /// <summary>
        ///
        /// </summary>
        public int[] Data;

        /// <summary>
        ///
        /// </summary>
        public ImageData()
        {
            Initialize(0, "Unknown", 1, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        public ImageData(int id, string name)
        {
            Initialize(id, name, 1, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public ImageData(int id, string name, int width, int height)
        {
            Initialize(id, name, width, height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="imageData"></param>
        protected ImageData(ImageData imageData)
        {
            Id = imageData.Id;
            Name = imageData.Name;
            Width = imageData.Width;
            Height = imageData.Height;
            Data = new int[Width * Height];

            for (int i = 0; i < Count; i++)
            {
                Data[i] = imageData.Data[i];
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void Initialize(int id, string name, int width, int height)
        {
            Id = id;
            Name = name;
            Width = width > 0 ? width : 1;
            Height = height > 0 ? height : 1;

            Data = new int[Width * Height];
            Clear();
        }

        /// <summary>
        /// Deep copy Clone Image
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new ImageData(this);
        }

        /// <summary>
        /// Clear image
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            if (Data != null)
            {
                for (int i = 0; i < Count; i++)
                {
                    Data[i] = 0;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Usage(int value)
        {
            int usage = 0;
            foreach (int i in Data)
            {
                if (i == value) usage++;
            }

            return usage;
        }

        /// <summary>
        /// Check if image is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            foreach (int i in Data)
            {
                if (i != 0) return false;
            }

            return true;
        }

        /// <summary>
        /// Copy cell data
        /// </summary>
        /// <param name="imageData"></param>
        public void Copy(ImageData imageData)
        {
            for (int i = 0; i < Count; i++)
            {
                Data[i] = imageData.Data[i];
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Resize(int width, int height)
        {
            // Clone existing image data
            ImageData clonedImage = (ImageData)Clone();

            Initialize(clonedImage.Id, clonedImage.Name, width, height);

            for (int y = 0; y < Height && y < clonedImage.Height; y++)
            {
                for (int x = 0; x < Width && x < clonedImage.Width; x++)
                {
                    int index = y * clonedImage.Width + x;
                    int index2 = y * Width + x;

                    Data[index2] = clonedImage.Data[index];
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="count"></param>
        public void ValidateChars(int size)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Data[i] > size) Data[i] = 0;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void RemapData(int oldValue, int newValue)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Data[i] == oldValue)
                {
                    Data[i] = newValue;
                }
            }
        }

        /// <summary>
        /// Get Cell value at x,y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetCellValue(int x, int y)
        {
            int index = y * Width + x;

            if (Data != null && index >= 0 && index < Count)
            {
                return Data[y * Width + x];
            }

            return -1;
        }

        /// <summary>
        /// Set cell value at x,y,
        /// calls function Pencil
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetCellValue(int x, int y, int value)
        {
            Pencil(x, y, value);
        }

        /// <summary>
        /// Pencil
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void Pencil(int x, int y, int value)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;

            int index = y * Width + x;

            if (Data != null && index >= 0 && index < Count)
            {
                Data[index] = value;
            }
        }

        /// <summary>
        /// Brush
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void Brush(int x, int y, int value)
        {
            Pencil(x, y, value);
            Pencil(x + 1, y, value);
            Pencil(x - 1, y, value);
            Pencil(x, y + 1, value);
            Pencil(x, y - 1, value);
        }

        /// <summary>
        /// Floddfill Image
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void FloodFill(int x, int y, int value)
        {
            int index = y * Width + x;

            if (Data != null && (index >= 0 && index < Count) && Data[index] != (byte)value)
            {
                int oldValue = Data[index];
                if (oldValue != value)
                    FloodFillExt(x, y, oldValue, value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private void FloodFillExt(int x, int y, int oldValue, int newValue)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;

            int index = y * Width + x;

            if (Data[index] != oldValue)
                return;

            Data[index] = newValue;

            FloodFillExt(x + 1, y, oldValue, newValue);
            FloodFillExt(x - 1, y, oldValue, newValue);
            FloodFillExt(x, y + 1, oldValue, newValue);
            FloodFillExt(x, y - 1, oldValue, newValue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oldColour"></param>
        /// <param name="newColour"></param>
        public void ReplaceColour(int oldColour, int newColour)
        {
            for (int index = 0; index < Count; index++)
            {
                if (Data[index] == oldColour) Data[index] = newColour;
            }
        }

        /// <summary>
        /// 2, 4 and 16 colour depth
        /// </summary>
        /// <param name="colourDepth"></param>
        public void Negative(int colourDepth)
        {
            for (int index = 0; index < Count; index++)
            {
                Data[index] ^= (byte)(colourDepth - 1);
            }
        }

        /// <summary>
        /// Rotate Image Clockwise 90
        /// </summary>
        public void RotateClockwise()
        {
            ImageData clonedImage = (ImageData)Clone();

            int index = 0;
            for (int x = 0; x < Width; x++)
            {
                for (int y = Height - 1; y >= 0; y--)
                {
                    Data[index++] = clonedImage.Data[y * Width + x];
                }
            }
        }

        /// <summary>
        /// Flip Image Horizontally
        /// </summary>
        public void FlipX()
        {
            ImageData clonedImage = (ImageData)Clone();

            int index = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = Width - 1; x >= 0; x--)
                {
                    Data[index++] = clonedImage.Data[y * Width + x];
                }
            }
        }

        /// <summary>
        /// Flip Image Vertically
        /// </summary>
        public void FlipY()
        {
            ImageData clonedImage = (ImageData)Clone();

            int index = 0;
            for (int y = Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    Data[index++] = clonedImage.Data[y * Width + x];
                }
            }
        }

        /// <summary>
        /// Shift Image Left
        /// </summary>
        public void ShiftLeft()
        {
            ImageData clonedImage = (ImageData)Clone();

            int index = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 1; x <= Width; x++)
                {
                    Data[index++] = clonedImage.Data[y * Width + (x % Width)];
                }
            }
        }

        /// <summary>
        /// Shift Image Left
        /// </summary>
        /// <param name="offset"></param>
        public void ShiftLeft(int offset)
        {
            ImageData clonedImage = (ImageData)Clone();

            int index = 0;
            for (int y = 0; y < Height; y++)
            {
                index += offset;
                for (int x = offset + 1; x <= Width; x++)
                {
                    int x2 = (x % Width);

                    if (x2 < offset) x2 += offset;

                    Data[index++] = clonedImage.Data[y * Width + x2];
                }
            }
        }

        /// <summary>
        /// Shift Image Right
        /// </summary>
        public void ShiftRight()
        {
            ImageData clonedImage = (ImageData)Clone();

            int index = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Data[index++] = clonedImage.Data[y * Width + ((Width - 1 + x) % Width)];
                }
            }
        }

        /// <summary>
        /// Shift Image Right
        /// </summary>
        /// <param name="offset"></param>
        public void ShiftRight(int offset)
        {
            ImageData clonedImage = (ImageData)Clone();

            int index = 0;

            for (int y = 0; y < Height; y++)
            {
                index += offset;
                for (int x = offset; x < Width; x++)
                {
                    int x2 = (Width - 1 + x - offset) % Width;

                    if (x2 < Width - offset) x2 += offset;

                    Data[index++] = clonedImage.Data[y * Width + x2];
                }
            }
        }

        /// <summary>
        /// Shift Image Up
        /// </summary>
        public void ShiftUp()
        {
            ImageData clonedImage = (ImageData)Clone();

            int index = 0;
            for (int y = 1; y <= Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Data[index++] = clonedImage.Data[(y % Height) * Width + x];
                }
            }
        }

        /// <summary>
        /// Shift Image Up
        /// </summary>
        /// <param name="offset"></param>
        public void ShiftUp(int offset)
        {
            ImageData clonedImage = (ImageData)Clone();

            int index = offset * Width;

            for (int y = offset + 1; y <= Height; y++)
            {
                int y2 = y % Height;

                if (y2 < offset) y2 += offset;

                for (int x = 0; x < Width; x++)
                {
                    //Data[index++] = clonedImage.Data[(y % Height) * Width + x];
                    Data[index++] = clonedImage.Data[y2 * Width + x];
                }
            }
        }

        /// <summary>
        /// Shift Image Down
        /// </summary>
        public void ShiftDown()
        {
            ImageData clonedImage = (ImageData)Clone();

            int index = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Data[index++] = clonedImage.Data[((Height - 1 + y) % Height) * Width + x];
                }
            }
        }

        /// <summary>
        /// Shift Image Down
        /// </summary>
        /// <param name="offset"></param>
        public void ShiftDown(int offset)
        {
            ImageData clonedImage = (ImageData)Clone();

            int index = offset * Width;

            for (int y = offset; y < Height; y++)
            {
                int y2 = (Height - 1 + y - offset) % Height;

                if (y2 < Height - offset) y2 += offset;

                for (int x = 0; x < Width; x++)
                {
                    Data[index++] = clonedImage.Data[y2 * Width + x];
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="row"></param>
        public void InsertRow(int row)
        {
            Resize(Width, Height + 1);
            ShiftDown(row);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="row"></param>
        public void DeleteRow(int row)
        {
            ShiftUp(row);
            Resize(Width, Height - 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="col"></param>
        public void InsertColumn(int col)
        {
            Resize(Width + 1, Height);
            ShiftRight(col);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="col"></param>
        public void DeleteColumn(int col)
        {
            ShiftLeft(col);
            Resize(Width - 1, Height);
        }
    }
}