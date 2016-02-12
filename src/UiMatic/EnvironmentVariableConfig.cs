using System;
using System.Collections.Generic;

namespace ChimpLab.UiMatic
{
    public class EnvironmentVariableConfig : IConfiguration
    {
        private TestTarget browser;

        public EnvironmentVariableConfig(TestTarget browser)
        {
            this.browser = browser;
        }

        public string ChromeDriverLocation
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }

        public string EdgeDriverLocation
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string IEDriverLocation
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }

        public string SafariDriverLocation
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }

        public string CurrentTestName
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
            set
            {
                // TODO: Implement this property setter
                throw new NotImplementedException();
            }
        }

        public TestTarget CurrentBrowser
        {
            get
            {
                return this.browser;
            }
        }

        public IDictionary<string, string> CustomSettings
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}