using System;
using System.Collections.Generic;

namespace ChimpLab.UiMatic
{
    public class DefaultConfig : IConfiguration
    {
        public DefaultConfig()
        { }

        public DefaultConfig(TestTarget currentBrowser)
        {
            CurrentBrowser = currentBrowser;
            CustomSettings = new Dictionary<string, string>();
        }

        public string ChromeDriverLocation { get; set; }
        public string EdgeDriverLocation { get; set; }
        public string IEDriverLocation { get; set; }
        public string SafariDriverLocation { get; set; }

        public string CurrentTestName { get; set; }

        public TestTarget CurrentBrowser { get; set; }

        public IDictionary<string, string> CustomSettings { get; set; }
    }
}