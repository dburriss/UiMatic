using Microsoft.Extensions.Configuration;
using System.Linq;

namespace UiMatic
{
    public class DefaultConfig : IConfiguration
    {
        private readonly IConfigurationRoot _configRoot;

        public DefaultConfig()
        { }

        public DefaultConfig(TestTarget currentBrowser, IConfigurationRoot configRoot)
        {
            CurrentBrowser = currentBrowser;
            _configRoot = configRoot;
        }

        public string ChromeDriverLocation
        {
            get { return GetConfigurationValue("ChromeDriverLocation"); }
            set { SetConfigurationValue("ChromeDriverLocation", value); }
        }

        public string EdgeDriverLocation
        {
            get { return GetConfigurationValue("EdgeDriverLocation"); }
            set { SetConfigurationValue("EdgeDriverLocation", value); }
        }

        public string IEDriverLocation
        {
            get { return GetConfigurationValue("IEDriverLocation"); }
            set { SetConfigurationValue("IEDriverLocation", value); }
        }

        public string SafariDriverLocation
        {
            get { return GetConfigurationValue("SafariDriverLocation"); }
            set { SetConfigurationValue("SafariDriverLocation", value); }
        }

        public string OperaDriverLocation
        {
            get { return GetConfigurationValue("OperaDriverLocation"); }
            set { SetConfigurationValue("OperaDriverLocation", value); }
        }

        //public string CurrentTestName { get; set; }

        public TestTarget CurrentBrowser { get; set; }

        public string GetConfigurationValue(string key)
        {
            return GetValue(key, "configuration");
        }

        public void SetConfigurationValue(string key, string value)
        {
            SetValue(key, value, "configuration");
        }


        public string GetCustomValue(string key)
        {
            return GetValue(key, "custom");
        }

        public void SetCustomValue(string key, string value)
        {
            SetValue(key, value, "custom");
        }

        public PageSetting GetPageSetting(string key)
        {
            var pageSettings = _configRoot.GetSection("pages").GetChildren().FirstOrDefault(x => x.Key == key);
            var title = pageSettings.GetChildren().FirstOrDefault(x => x.Key == "title");
            var url = pageSettings.GetChildren().FirstOrDefault(x => x.Key == "url");

            return new PageSetting()
            {
                Title = title.Value,
                Url = url.Value
            };
        }

        public void SetPageSetting(string key, PageSetting pageSetting)
        {
            var pageSettings = _configRoot.GetSection("pages").GetChildren().FirstOrDefault(x => x.Key == key);
            var title = pageSettings.GetChildren().FirstOrDefault(x => x.Key == "title");
            var url = pageSettings.GetChildren().FirstOrDefault(x => x.Key == "url");

            title.Value = pageSetting.Title;
            url.Value = pageSetting.Url;
        }


        private string GetValue(string key, string section)
        {
            var el = _configRoot.GetSection(section).GetChildren().FirstOrDefault(x => x.Key == key);
            return el.Value;
        }

        private void SetValue(string key, string value, string section)
        {
            var el = _configRoot.GetSection(section).GetChildren().FirstOrDefault(x => x.Key == key);
            if (el != null)
                el.Value = value;
        }
    }
}