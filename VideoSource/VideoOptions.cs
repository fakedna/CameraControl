namespace VideoSource
{
    /// <summary>
    /// desired options
    /// </summary>
    public readonly struct VideoOptions
    {
        public readonly string Format;
        public readonly (int, int) Size;
        public readonly float FrameRate;

        /// 10 000 000 / FrameRate (100 ns)
        public long AvgTimePerFrame => 10000000 / (long) FrameRate;

        public bool IsNull => string.IsNullOrEmpty(Format) && FrameRate == 0 && (Size.Item1 == 0 || Size.Item2 == 0);

        public VideoOptions(string format, (int, int) size, float frameRate)
        {
            Format = format;
            Size = size;
            FrameRate = frameRate;
        }
    }
}