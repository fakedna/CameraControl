namespace VideoSource
{
    public readonly struct VideoDeviceInfo
    {
        public readonly int Index;
        public readonly string Name;
        public readonly string Description;
        public readonly string DevicePath;

        public VideoDeviceInfo(int index, string name, string description, string devicePath)
        {
            Index = index;
            Name = name;
            Description = description;
            DevicePath = devicePath;
        }
    }
}