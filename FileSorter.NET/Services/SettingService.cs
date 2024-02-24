using System.Configuration;

namespace FileSorter.NET.Services
{
    internal class SettingService : ApplicationSettingsBase, ISettingService
    {
        [DefaultSettingValue(""), UserScopedSetting]
        public string FromDirectory
        {
            get => (string)this[nameof(FromDirectory)];
            set
            {
                this[nameof(FromDirectory)] = value;
            }
        }

        [DefaultSettingValue(""), UserScopedSetting]
        public string ToDirectory
        {
            get => (string)this[nameof(ToDirectory)];
            set
            {
                this[nameof(ToDirectory)] = value;
            }
        }
    }
}
