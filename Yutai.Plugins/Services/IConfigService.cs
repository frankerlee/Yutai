using Yutai.Plugins.Concrete;

namespace Yutai.Plugins.Services
{
    public interface IConfigService
    {
        AppConfig Config { get; }

        string ConfigPath { get; }

        void LoadAll();

        bool LoadConfig();

        void SaveAll();

        bool SaveConfig();
    }
}