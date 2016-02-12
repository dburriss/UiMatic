using System.Collections.Generic;

namespace ChimpLab.UiMatic
{
    public interface IConfiguration
    {
        string ChromeDriverLocation { get; }
        string EdgeDriverLocation { get; }
        string IEDriverLocation { get; }
        string SafariDriverLocation { get; }
        string CurrentTestName { get; set; }
        TestTarget CurrentBrowser { get; }
        IDictionary<string, string> CustomSettings { get; }
    }
}