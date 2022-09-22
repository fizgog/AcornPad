using Newtonsoft.Json;

namespace AcornPad.Internal
{
    public class Snapshot
    {
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public ImageDataArray Chars { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public ImageDataArray Tiles { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public ImageDataArray Maps { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Snapshot()
        {
        }
    }
}