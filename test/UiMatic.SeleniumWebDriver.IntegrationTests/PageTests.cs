using ChimpLab.UiMatic.SeleniumWebDriver.IntegrationTests.Pages;
using System.IO;
using Xunit;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ChimpLab.UiMatic.SeleniumWebDriver.IntegrationTests
{
    public class PageTests : TestBase
    {
        [Theory]
        [InlineData(TestTarget.Chrome)]
        public void Google(TestTarget target)
        {
            var config = GetDefaultConfig(target);
            using (IDriver driver = GetDriver(target, config))
            {
                var homePage = Page.Create<GoogleHomePage>("http://www.google.com/", driver, config);
                homePage.Go<GoogleHomePage>();

                homePage.SearchBox.Value = "TEST";

                Assert.Equal("Google", homePage.Title);
            }
        }

        public IConfiguration GetDefaultConfig(TestTarget target)
        {
            var configModel = new DefaultConfig(target);
            var dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var testFolder = new DirectoryInfo(Directory.GetCurrentDirectory()).FullName;
            var provider = new JsonConfigurationProvider(Path.Combine(testFolder, "appsettings.json"));
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            builder.Add(provider, true);
            var config = builder.Build();

            configModel.ChromeDriverLocation = config.GetSection("configuration")["ChromeDriverLocation"];
            configModel.CustomSettings = GetData(provider);
            return configModel;
        }

        private IDictionary<string, string> GetData(Microsoft.Extensions.Configuration.ConfigurationProvider provider)
        {
            var type = typeof(Microsoft.Extensions.Configuration.ConfigurationProvider);
            var pi = type.GetProperty("Data", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            IDictionary<string, string> data = pi.GetValue(provider, null) as IDictionary<string, string>;
            return data;
        }

        private IDriver GetDriver(TestTarget target, IConfiguration configuration)
        {
            return VerifyToContinue((t) => DriverFactory.Create(configuration), target).ToIDriver();
        }
    }
}
