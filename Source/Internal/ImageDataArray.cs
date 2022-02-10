using AcornPad.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AcornPad
{
    public class ImageDataArray : ICloneable
    {
        [JsonIgnore]
        public DataType ImageDataType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<ImageData> Items;

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public int Width => Items[SelectedItem].Width;

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public int Height => Items[SelectedItem].Height;

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public int Area => Width * Height;

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public int TotalBytes => Count * Area;

        /// <summary>
        ///
        /// </summary>
        public int Count => Items.Count;

        /// <summary>
        ///
        /// </summary>
        public int SelectedItem { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int SelectedItemTile { get; set; }

        public ImageDataArray()
        {
            Initialize(DataType.Unknown, "Unknown", 0, 0, 0);
        }

        /// <summary>
        ///
        /// </summary>
        public ImageDataArray(DataType imageDataType, string name)
        {
            Initialize(imageDataType, name, 1, 1, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="qty"></param>
        public ImageDataArray(DataType imageDataType, string name, int qty)
        {
            Initialize(imageDataType, name, qty, 1, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="qty"></param>
        public ImageDataArray(DataType imageDataType, string name, int qty, int width, int height)
        {
            Initialize(imageDataType, name, qty, width, height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="imageDataArray"></param>
        protected ImageDataArray(ImageDataArray imageDataArray)
        {
            ImageDataType = imageDataArray.ImageDataType;
            SelectedItem = imageDataArray.SelectedItem;
            SelectedItemTile = imageDataArray.SelectedItemTile;

            Items = new List<ImageData>();
            for (int i = 0; i < imageDataArray.Items.Count; i++)
                Items.Add((ImageData)imageDataArray.Items[i].Clone());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="qty"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void Initialize(DataType imageDataType, string name, int qty, int width, int height)
        {
            ImageDataType = imageDataType;
            Items = new List<ImageData>();

            for (int i = 0; i < qty; i++)
            {
                Items.Add(new ImageData(i, name, width, height));
            }

            SelectedItem = 0;
            SelectedItemTile = 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new ImageDataArray(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Resize(int qty)
        {
            // Clone existing image data
            ImageDataArray clonedImage = (ImageDataArray)Clone();
            Initialize(clonedImage.ImageDataType, clonedImage.Items[0].Name, qty, clonedImage.Width, clonedImage.Height);

            for (int i = 0; i < qty && i < clonedImage.Count; i++)
            {
                Items[i] = clonedImage.Items[i];
            }

            SelectedItem = clonedImage.SelectedItem < Count ? clonedImage.SelectedItem : Count - 1;
            SelectedItemTile = clonedImage.SelectedItemTile < Count ? clonedImage.SelectedItemTile : Count - 1;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <param name="bitsPerByte"></param>
        /// <returns></returns>
        public List<int> GetBitsFromByte(int index, int bitsPerByte)
        {
            List<int> bits = new List<int>();

            for (int i = 0; i < Items[index].Count; i += bitsPerByte)
            {
                int beeb = 0;

                for (int j = 0; j < bitsPerByte; j++)
                {
                    beeb |= Items[index].Data[i + j] << (bitsPerByte - 1 - j);
                }
                bits.Add(beeb);
            }

            return bits;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [JsonIgnore]
        public bool CanCut => SelectedItem != -1;

        /// <summary>
        ///
        /// </summary>
        public void Cut(string format)
        {
            if (CanCut)
            {
                Clipboard.SetData(format, Items[SelectedItem]);
                Items.RemoveAt(SelectedItem);
            }
        }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public bool CanCopy => SelectedItem != -1;

        /// <summary>
        ///
        /// </summary>
        public void Copy(string format)
        {
            if (CanCopy)
            {
                Clipboard.SetData(format, Items[SelectedItem]);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool CanPaste(string format) => Clipboard.ContainsData(format);

        /// <summary>
        ///
        /// </summary>
        public void Paste(string format)
        {
            if (CanPaste(format))
            {
                ImageData item = (ImageData)Clipboard.GetData(format);
                Items.Insert(SelectedItem, item);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Negative(int numColours)
        {
            Items[SelectedItem].Negative(numColours);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rotation"></param>
        public void Rotate(RotateType rotation)
        {
            switch (rotation)
            {
                case RotateType.Rotate270:
                    Items[SelectedItem].RotateClockwise();
                    // Fall through
                    goto case RotateType.Rotate180;

                case RotateType.Rotate180:
                    Items[SelectedItem].RotateClockwise();
                    // Fall through
                    goto case RotateType.Rotate90;

                case RotateType.Rotate90:
                    Items[SelectedItem].RotateClockwise();
                    break;

                default: throw new System.Exception("Unknown rotation.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flip"></param>
        public void Flip(FlipType flip)
        {
            switch (flip)
            {
                case FlipType.FlipX:
                    Items[SelectedItem].FlipX();
                    break;

                case FlipType.FlipY:
                    Items[SelectedItem].FlipY();
                    break;

                default: throw new System.Exception("Unknown flip.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="shift"></param>
        public void Shift(ShiftType shift)
        {
            switch (shift)
            {
                case ShiftType.ShiftLeft:
                    Items[SelectedItem].ShiftLeft();
                    break;

                case ShiftType.ShiftRight:
                    Items[SelectedItem].ShiftRight();
                    break;

                case ShiftType.ShiftUp:
                    Items[SelectedItem].ShiftUp();
                    break;

                case ShiftType.ShiftDown:
                    Items[SelectedItem].ShiftDown();
                    break;

                default: throw new System.Exception("Unknown shift.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void RemapCharsToTiles(int width, int height)
        {
            // Clone existing image data
            ImageDataArray clonedImage = (ImageDataArray)Clone();

            for (int i = 0; i < Count; i++)
            {
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void RemapTilesToChars(int width, int height)
        {
            // Clone existing image data
            ImageDataArray clonedImage = (ImageDataArray)Clone();

            for (int i = 0; i < Count; i++)
            {
            }
        }
    }
}