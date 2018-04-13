using Persistence.Model;

namespace Persistence.Mapper
{
    public interface ISettingsDao
    {
        SettingsModel LoadSettings();
        void SaveSettings(SettingsModel settings);
    }
}