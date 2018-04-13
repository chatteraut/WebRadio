using Persistence.Model;

namespace Persistence.Mapper
{
    /// <exception cref=""
    public interface ISettingsDao
    {
        SettingsModel LoadSettings();
        void SaveSettings(SettingsModel settings);
    }
}