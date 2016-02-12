using ChimpLab.UiMatic.SeleniumWebDriver.IntegrationTests.Pages;
using System.IO;
using Xunit;

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
            var provider = new Microsoft.Extensions.Configuration.Json.JsonConfigurationProvider(Path.Combine(testFolder, "appsettings.json"));
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            builder.Add(provider, true);
            var config = builder.Build();
            //TODO: check Microsoft.Extensions.Configuration.ConfigurationBinder
            configModel.ChromeDriverLocation = config.GetSection("configuration")["ChromeDriverLocation"];
            //config.EdgeDriverLocation = "C:\\Program Files (x86)\\Microsoft Web Driver";
            return configModel;
        }

        private IDriver GetDriver(TestTarget target, IConfiguration configuration)
        {
            return VerifyToContinue((t) => DriverFactory.Create(configuration), target).ToIDriver();
        }
    }
}
