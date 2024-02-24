namespace FileSorter.NET.Services
{
    internal interface ISettingService
    {
        string FromDirectory { get; set; }
        string ToDirectory { get; set; }

        public void Save();
    }
}