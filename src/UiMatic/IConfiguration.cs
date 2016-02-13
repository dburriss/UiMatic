namespace UiMatic
{
    public interface IConfiguration
    {
        string ChromeDriverLocation { get; }
        string EdgeDriverLocation { get; }
        string IEDriverLocation { get; }
        string SafariDriverLocation { get; }
        //string CurrentTestName { get; set; }
        TestTarget CurrentBrowser { get; set; }
        string GetConfigurationValue(string key);
        void SetConfigurationValue(string key, string value);
        string GetCustomValue(string key);
        void SetCustomValue(string key, string value);
        PageSetting GetPageSetting(string key);
        void SetPageSetting(string key, PageSetting pageSetting);
    }
}