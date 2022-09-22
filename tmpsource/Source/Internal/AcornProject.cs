using AcornPad.Common;
using AcornPad.Internal;
using Newtonsoft.Json;

namespace AcornPad
{
    public class AcornProject
    {
        private const int MAX_ATOM_COLOURS = 4;
        private const int MAX_BEEB_COLOURS = 16;

        /// <summary>
        ///
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///
        /// </summary>
        private readonly UndoRedo UndoRedoObject = new UndoRedo();

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public UndoRedo Stack => UndoRedoObject;

        /// <summary>
        ///
        /// </summary>
        public Machine Machine { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public MachineType MachineType { get; set; }

        /// <summary>
        ///
        /// </summary>
        public FormStore PaletteForm { get; set; }

        /// <summary>
        ///
        /// </summary>
        public FormStore CharSetForm { get; set; }

        /// <summary>
        ///
        /// </summary>
        public FormStore CharEditForm { get; set; }

        /// <summary>
        ///
        /// </summary>
        public FormStore TileSetForm { get; set; }

        /// <summary>
        ///
        /// </summary>
        public FormStore TileEditForm { get; set; }

        /// <summary>
        ///
        /// </summary>
        public FormStore MapForm { get; set; }

        public Palette Palette { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ImageDataArray Chars = null;

        /// <summary>
        ///
        /// </summary>
        public bool TilesOnline { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ImageDataArray Tiles = null;

        /// <summary>
        ///
        /// </summary>
        public bool MultipleMaps { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int NumberOfMaps { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ImageDataArray Maps = null;

        public AcornProject()
        {
            Initialize(MachineType.BBC, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        /// <summary>
        ///
        /// </summary>
        public AcornProject(MachineType machineType)
        {
            Initialize(machineType, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nChars"></param>
        public AcornProject(MachineType machineType, int nChars)
        {
            Initialize(machineType, nChars, 8, 8, 0, 0, 0, 0, 0, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nChars"></param>
        public AcornProject(MachineType machineType, int nChars, int cWidth, int cHeight)
        {
            Initialize(machineType, nChars, cWidth, cHeight, 0, 0, 0, 0, 0, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nChars"></param>
        /// <param name="cWidth"></param>
        /// <param name="cHeight"></param>
        /// <param name="nTiles"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="nMaps"></param>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        public AcornProject(MachineType machineType, int nChars, int cWidth, int cHeight, int nTiles, int tileWidth, int tileHeight, int nMaps, int mapWidth, int mapHeight)
        {
            Initialize(machineType, nChars, cWidth, cHeight, nTiles, tileWidth, tileHeight, nMaps, mapWidth, mapHeight);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nChars"></param>
        /// <param name="charWidth"></param>
        /// <param name="charHeight"></param>
        private void Initialize(MachineType machineType, int nChars, int charWidth, int charHeight, int nTiles, int tileWidth, int tileHeight, int nMaps, int mapWidth, int mapHeight)
        {
            // Set project version to assembly version
            Version = Sys.AssemblyVersion;

            // Clear Undo / Redo
            Stack.Clear();

            // Palette
            Palette = new Palette(machineType, machineType == MachineType.Atom ? MAX_ATOM_COLOURS : MAX_BEEB_COLOURS);
            PaletteForm = new FormStore(0, 10, 10, 332, 440);

            // Chars
            Chars = new ImageDataArray(DataType.Char, "char_sprite", nChars, charWidth, charHeight);
            CharSetForm = new FormStore(2, 350, 10);
            CharEditForm = new FormStore(28, 350, 420);

            Chars.SelectedItem = Chars.Count >= 1 ? 1 : 0;

            TilesOnline = false;
            Tiles = null;

            if (nTiles > 0)
            {
                TilesOnline = true;
                Tiles = new ImageDataArray(DataType.Tile, "tile", nTiles, tileWidth, tileHeight);
            }

            // Maps
            MultipleMaps = false;
            NumberOfMaps = nMaps;
            Maps = new ImageDataArray(DataType.Map, "map", NumberOfMaps, mapWidth, mapHeight);
            MapForm = new FormStore(3, 760, 10, 600, 400);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="numColours"></param>
        public void ReduceColours(int numColours)
        {
            for (int index = 0; index < Chars.Count; index++)
            {
                for (int i = 0; i < Chars.Items[index].Count; i++)
                {
                    Chars.Items[index].Data[i] = Chars.Items[index].Data[i] % numColours;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="imgArray"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Usage(ImageDataArray imgArray)
        {
            int usage = 0;

            usage += (imgArray.ImageDataType == DataType.Char && TilesOnline) ? Tiles.Usage(imgArray.SelectedItem) : Maps.Usage(imgArray.SelectedItem);

            return usage;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool CanUndo()
        {
            return UndoRedoObject.CanUndo;
        }

        /// <summary>
        ///
        /// </summary>
        public void Undo()
        {
            if (CanUndo())
            {
                if (Stack.UndoPeek() is Snapshot snapshot)
                {
                    string description = snapshot.Description;

                    Snapshot current = new Snapshot
                    {
                        Description = description,
                        Chars = (ImageDataArray)Chars.Clone(),
                        Tiles = Tiles != null ? (ImageDataArray)Tiles.Clone() : null,
                        Maps = (ImageDataArray)Maps.Clone()
                    };

                    snapshot = (Snapshot)Stack.Undo(current);
                    Chars = snapshot.Chars;
                    Tiles = snapshot.Tiles;
                    Maps = snapshot.Maps;

                    TilesOnline = Tiles != null;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool CanRedo()
        {
            return UndoRedoObject.CanRedo;
        }

        /// <summary>
        ///
        /// </summary>
        public void Redo()
        {
            if (CanRedo())
            {
                if (Stack.RedoPeek() is Snapshot snapshot)
                {
                    string description = snapshot.Description;

                    Snapshot current = new Snapshot
                    {
                        Description = description,
                        Chars = (ImageDataArray)Chars.Clone(),
                        Tiles = Tiles != null ? (ImageDataArray)Tiles.Clone() : null,
                        Maps = (ImageDataArray)Maps.Clone()
                    };

                    snapshot = (Snapshot)Stack.Redo(current);
                    Chars = snapshot.Chars;
                    Tiles = snapshot.Tiles;
                    Maps = snapshot.Maps;

                    TilesOnline = Tiles != null;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="objType"></param>
        public void Cut(DataType objType)
        {
            switch (objType)
            {
                case DataType.Char:
                    AddHistory("Cut Character");
                    RemapData(TilesOnline ? Tiles : Maps, Chars.Items[Chars.SelectedItem].Id, Chars.Items[0].Id);
                    Chars.Cut("CharImage");
                    ResyncMap(Chars, TilesOnline);
                    Chars.SelectedItem = 0;
                    break;

                case DataType.Tile:
                    AddHistory("Cut Tile");
                    RemapData(Tiles, Tiles.Items[Tiles.SelectedItem].Id, Tiles.Items[0].Id);
                    Tiles.Cut("TileImage");
                    ResyncMap(Tiles, TilesOnline);
                    Tiles.SelectedItem = 0;
                    break;

                case DataType.Map:
                    //AddHistory("Cut Map");
                    //Maps.Cut("MapImage");
                    break;

                default: throw new System.Exception("Unknown image data type.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="objType"></param>
        public void Copy(DataType objType)
        {
            switch (objType)
            {
                case DataType.Char:
                    AddHistory("Copy Character");
                    Chars.Copy("CharImage");
                    break;

                case DataType.Tile:
                    AddHistory("Copy Tile");
                    Tiles.Copy("TileImage");
                    break;

                case DataType.Map:
                    //AddHistory("Copy Map");
                    //Maps.Copy("MapImage");
                    break;

                default: throw new System.Exception("Unknown image data type.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="objType"></param>
        public void Paste(DataType objType)
        {
            switch (objType)
            {
                case DataType.Char:
                    AddHistory("Paste Character");
                    Chars.Paste("CharImage");
                    RemapSetToMap(Chars);
                    break;

                case DataType.Tile:
                    AddHistory("Paste Tile");
                    Tiles.Paste("TileImage");
                    RemapSetToMap(Tiles);
                    break;

                case DataType.Map:
                    //AddHistory("Paste Map");
                    //Maps.Paste("MapImage");
                    break;

                default: throw new System.Exception("Unknown image data type.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="imgArray"></param>
        public void RemapSetToMap(ImageDataArray imgArray)
        {
            for (int i = 0; i < imgArray.Count; i++)
            {
                imgArray.Items[i].Id = i;
            }

            for (int i = 0; i < Maps.Area; i++)
            {
                int value = Maps.Items[Maps.SelectedItem].Data[i];

                if (value >= imgArray.SelectedItem)
                {
                    Maps.Items[Maps.SelectedItem].Data[i] = value + 1;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void AddHistory(string description)
        {
            Snapshot snapshot = new Snapshot
            {
                Description = description,
                Chars = (ImageDataArray)Chars.Clone(),
                Tiles = Tiles != null ? (ImageDataArray)Tiles.Clone() : null,
                Maps = (ImageDataArray)Maps.Clone()
            };

            Stack.AddHistory(snapshot);
        }

        /// <summary>
        /// Compress character or tile set into the least amount of items
        /// </summary>
        /// <param name="imgArray"></param>
        public void CompressData(ImageDataArray imgArray, bool usingTiles)
        {
            if (imgArray.Count < 2) return;

            // Compress character/tile set and remap the map
            for (int i = 0; i < imgArray.Count; i++)
            {
                for (int j = i + 1; j < imgArray.Count; j++)
                {
                    // Are characters / tiles at i and j the same?
                    if (Helper.IntArrayCompare(imgArray.Items[i].Data, imgArray.Items[j].Data) == true)
                    {
                        int newValue = imgArray.Items[i].Id;
                        int oldValue = imgArray.Items[j].Id;

                        RemapData(imgArray.ImageDataType == DataType.Char && usingTiles ? Tiles : Maps, oldValue, newValue);
                  
                        imgArray.Items.RemoveAt(j);
                        j--;
                    }
                }
            }

            // Reset the id to the index
            ResyncMap(imgArray, usingTiles);

            // set the selectors
            if (imgArray.SelectedItem >= imgArray.Count) imgArray.SelectedItem = imgArray.Count - 1;
            if (imgArray.SelectedItemTile >= imgArray.Count) imgArray.SelectedItemTile = imgArray.Count - 1;
        }

        /// <summary>
        /// Convert map of tiles to a map of chars
        /// </summary>
        public void ConvertTileMapToChars()
        {
            int newMapWidth = Tiles.Width * Maps.Width;
            int newMapHeight = Tiles.Height * Maps.Height;

            ImageDataArray newMap = new ImageDataArray(DataType.Map, "map", 1, newMapWidth, newMapHeight);

            for (int mapY = 0; mapY < Maps.Height; mapY++)
            {
                for (int mapX = 0; mapX < Maps.Width; mapX++)
                {
                    int index = Maps.Items[Maps.SelectedItem].GetCellValue(mapX, mapY);

                    for (int tileY = 0; tileY < Tiles.Height; tileY++)
                    {
                        for (int tileX = 0; tileX < Tiles.Width; tileX++)
                        {
                            int value = Tiles.Items[index].GetCellValue(tileX, tileY);
                            newMap.Items[newMap.SelectedItem].SetCellValue((mapX * Tiles.Width) + tileX, (mapY * Tiles.Height) + tileY, value);
                        }
                    }
                }
            }

            Maps = newMap;

            CompressData(Chars, false);
        }

        /// <summary>
        /// Convert map of chars to a map of tiles
        /// </summary>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        public void ConvertCharMapToTiles(int tileWidth, int tileHeight)
        {
            TileSetForm = new FormStore(2, 760, 420);
            TileEditForm = new FormStore(8, 1170, 420);

            int newMapWidth = Maps.Width / tileWidth;
            int newMapHeight = Maps.Height / tileHeight;

            int qty = newMapWidth * newMapHeight;

            Tiles = new ImageDataArray(DataType.Tile, "tile", qty, tileWidth, tileHeight);

            for (int mapY = 0; mapY < newMapHeight; mapY++)
            {
                for (int mapX = 0; mapX < newMapWidth; mapX++)
                {
                    int index = mapY * newMapWidth + mapX;

                    for (int tileY = 0; tileY < tileHeight; tileY++)
                    {
                        for (int tileX = 0; tileX < tileWidth; tileX++)
                        {
                            int tileData = tileY * tileWidth + tileX;
                            int mapData = (mapY * tileHeight + tileY) * Maps.Width + (mapX * tileWidth + tileX);
                            Tiles.Items[index].Data[tileData] = Maps.Items[Maps.SelectedItem].Data[mapData];
                        }
                    }
                }
            }

            Maps = new ImageDataArray(DataType.Map, "map", 1, newMapWidth, newMapHeight);
            for (int i = 0; i < Maps.Items[Maps.SelectedItem].Data.Length; i++)
            {
                Maps.Items[Maps.SelectedItem].Data[i] = i;
            }

            Tiles.SelectedItem = Tiles.Count >= 1 ? 1 : 0;

            CompressData(Tiles, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="imgArray"></param>
        /// <param name="usingTiles"></param>
        public void ResyncMap(ImageDataArray imgArray, bool usingTiles)
        {
            // Reset the id to the index
            for (int i = 0; i < imgArray.Count; i++)
            {
                int oldValue = imgArray.Items[i].Id;
                if (oldValue != i)
                {
                    RemapData(imgArray.ImageDataType == DataType.Char && usingTiles ? Tiles : Maps, oldValue, i);
                    imgArray.Items[i].Id = i;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="imgArray"></param>
        private void RemapData(ImageDataArray imgArray, int oldValue, int newValue)
        {
            for (int i = 0; i < imgArray.Count; i++)
            {
                imgArray.Items[i].RemapData(oldValue, newValue);
            }
        }
    }
}
