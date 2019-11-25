using System.Collections;
using System.Configuration;
using System.Linq;

namespace BeepBong.SampleUpload.Desktop
{
    // @TODO: Config is persistent to the active session. Issues connecting between forms. Enable the saving to a file storage.
    class ClientConfig : IConfig
    {
        public string GetAPI()
        {
            return ConfigurationManager.AppSettings.Get("APIKey");
        }

        public string GetURL()
        {
            return ConfigurationManager.AppSettings.Get("URL");
        }

        public void SetAPI(string key)
        {
            SetConfigValue("APIKey", key);
        }

        public void SetURL(string url)
        {
            SetConfigValue("URL", url);
        }

        public bool IsConfigSetup()
        {
            return ConfigurationManager.AppSettings.HasKeys() && ConfigurationManager.AppSettings.AllKeys.Contains("URL");
        }

        private void SetConfigValue(string key, string value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;

            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}
