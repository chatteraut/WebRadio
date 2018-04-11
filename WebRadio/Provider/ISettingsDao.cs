using WebRadio.Model;

namespace WebRadio.Mapper
{
    public interface ISettingsDao
    {
        SettingsModel LoadSettings();
        void SaveSettings(SettingsModel settings);
    }
}