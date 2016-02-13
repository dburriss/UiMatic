using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Edge;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;

namespace UiMatic.SeleniumWebDriver
{
    public class DriverFactory
    {
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

            //if (config.CurrentBrowser == TestTarget.SauceLabsFirefox)
            //{
            //    if(!SauceLabsConstants.IsActive)
            //        return null;
            //    return SauceLabsDriverInstance("Windows 7", "firefox", "25" , config.CurrentTestName);
            //}

            throw new InvalidOperationException("Not a recognised web driver.");
        }
        public static IWebDriver Create(TestTarget browser, out IConfiguration defaultConfig)
        {
            var config = GetDriverConfig(browser);
            config.CurrentBrowser = browser;
            defaultConfig = config;
            return Create(config);
        }

        private static IConfiguration GetDriverConfig(TestTarget target)
        {
            IConfigurationRoot config = GetConfigurationRoot();
            var configModel = new DefaultConfig(target, config);
            return configModel;
        }

        private static IConfigurationRoot GetConfigurationRoot()
        {
            var builder = new ConfigurationBuilder();

            //in-memory
            SetConfigurationDefaults(builder);

            //json
            var dir = new DirectoryInfo(Directory.GetCurrentDirectory()).FullName;
            var settingsFile = Path.Combine(dir, "appsettings.json");
            builder.SetBasePath(dir);
            if (File.Exists(settingsFile))
            {                
                builder.AddJsonFile("appsettings.json");
            }
                        
            var config = builder.Build();
            return config;
        }

        private static void SetConfigurationDefaults(ConfigurationBuilder builder)
        {
            var defaults = new Dictionary<string, string>
            {
                {"configuration:ChromeDriverLocation", "C:\\Selenium\\chromedriver_win32"},
                {"configuration:IEDriverLocation", "C:\\Selenium\\iedriver_win32"}
            };
            builder.AddInMemoryCollection(defaults);
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
