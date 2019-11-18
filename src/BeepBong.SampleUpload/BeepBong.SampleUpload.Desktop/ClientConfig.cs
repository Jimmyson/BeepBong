using System.Configuration;

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
            ConfigurationManager.AppSettings.Set("APIKey", key);
        }

        public void SetURL(string url)
        {
            ConfigurationManager.AppSettings.Set("URL", url);
        }
    }
}
