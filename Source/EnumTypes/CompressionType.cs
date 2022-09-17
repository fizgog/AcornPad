using System;

namespace AcornPad.Common
{
    [Serializable]
    public enum CompressionType
    {
        None = 0,
        Run_length = 1,
        Four_Four = 2,
        Five_Three = 3
    }
}