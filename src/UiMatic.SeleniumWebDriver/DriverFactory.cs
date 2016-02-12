using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Edge;

namespace ChimpLab.UiMatic.SeleniumWebDriver
{
    public class DriverFactory
    {
        //TODO: replace this IConfiguration with Microsoft.Extensions.Configuration.IConfigurationRoot
        public static IWebDriver Create(IConfiguration config)
        {
            if (config.CurrentBrowser == TestTarget.Chrome)
            {
                return new ChromeDriver(config.ChromeDriverLocation);
            }

            if (config.CurrentBrowser == TestTarget.Edge)
            {
                return new EdgeDriver(config.EdgeDriverLocation);
            }

            if (config.CurrentBrowser == TestTarget.Firefox)
            {
                return new FirefoxDriver();
            }

            if (config.CurrentBrowser == TestTarget.IE)
            {
                return new InternetExplorerDriver(config.IEDriverLocation);
            }

            if (config.CurrentBrowser == TestTarget.Safari)
            {
                return new SafariDriver();
            }

            if (config.CurrentBrowser == TestTarget.SauceLabsFirefox)
            {
                if(!SauceLabsConstants.IsActive)
                    return null;
                return SauceLabsDriverInstance("Windows 7", "firefox", "25" , config.CurrentTestName);
            }

            throw new InvalidOperationException("Not a recognised web driver.");
        }
        public static IWebDriver Create(TestTarget browser, string testName)
        {
            var config = new EnvironmentVariableConfig(browser);
            return Create(config);
        }

        private static IWebDriver SauceLabsDriverInstance(string platform, string browser, string version, string testName)
        {
            // construct the url to sauce labs
            Uri commandExecutorUri = new Uri(SauceLabsConstants.Url);

            // set up the desired capabilities
            DesiredCapabilities desiredCapabilites = new DesiredCapabilities(browser, version, Platform.CurrentPlatform); // set the desired browser
            desiredCapabilites.SetCapability("platform", platform); // operating system to use
            desiredCapabilites.SetCapability("username", SauceLabsConstants.AccountName); // supply sauce labs username
            desiredCapabilites.SetCapability("accessKey", SauceLabsConstants.AccountKey);  // supply sauce labs account key
            //desiredCapabilites.SetCapability("name", TestContext.CurrentContext.Test.Name); // give the test a name
            desiredCapabilites.SetCapability("name", testName);

            // start a new remote web driver session on sauce labs
            var driver = new RemoteWebDriver(commandExecutorUri, desiredCapabilites);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));

            return driver;
        }
    }
}
