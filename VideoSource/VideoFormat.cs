namespace VideoSource
{
    /// <summary>
    /// from VideoInfoHeader class
    /// </summary>
    public struct VideoFormat
    {
        public string MajorType; // e.g. Video
        public string SubType; // e.g. YUY2, MJPG
        public string HeaderType; // VideoInfo or VideoInfo2
        public (int, int) BitmapSize;
        public int BitmapStride;
        public int BitRate;
        public int BitErrorRate;
        /// = 10 000 000 / frame duration. (ex: 333333 in case 30fps).
        public long AvgTimePerFrame;

        public int Width => BitmapSize.Item1;
        public int Height => BitmapSize.Item2;

        public override string ToString()
        {
            return $"Type={MajorType}, Format={SubType}, Header={HeaderType}, BitmapSize={BitmapSize}, BitmapStride={BitmapStride}, " +
                   $"BitRate={BitRate} BitErrorRate={BitErrorRate}, AvgTimePerFrame={AvgTimePerFrame}";
        }
    }

    /// <summary>
    /// from VideoStreamConfigCaps class
    /// </summary>
    public struct VideoCapabilities
    {
        public string MajorType; // ex. Video
        public string SubType; // ex. YUY2, MJPG
        public string HeaderType;
        public uint VideoStandard;
        public (int, int) InputSize;
        public (int, int) MinOutputSize;
        public (int, int) MaxOutputSize;
        public int OutputGranularityX;
        public int OutputGranularityY;
        public long MinFrameInterval;
        public long MaxFrameInterval;
        public int MinBitsPerSecond;
        public int MaxBitsPerSecond;

        public override string ToString()
        {
            return $"Type={MajorType}, Format={SubType}, Header={HeaderType}, Standard={VideoStandard}, Input={InputSize}," +
                   $" OutputRange={MinOutputSize}-{MaxOutputSize}, Step={OutputGranularityX},{OutputGranularityY}," +
                   $" FrameInterval={MinFrameInterval}-{MaxFrameInterval}, BitsPerSecond={MinBitsPerSecond}-{MaxBitsPerSecond}";
        }
    }
}