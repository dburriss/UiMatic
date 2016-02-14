using UiMatic.SeleniumWebDriver.IntegrationTests.Pages;
using System.IO;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace UiMatic.SeleniumWebDriver.IntegrationTests
{
    public class PageTests : TestBase
    {
        [Theory]
        [InlineData(TestTarget.Chrome)]
        public void Title_OnGoogleHomePageUsingConfig_IsGoogle(TestTarget target)
        {
            //build a custom config
            var config = GetDriverConfig(target);
            using (IDriver driver = GetDriver(target, config))
            {
                //create page model for test
                var homePage = Page.Create<GoogleHomePage>(driver);
                
                //tell browser to navigate to it
                homePage.Go<GoogleHomePage>();

                //fill a value into the text box
                homePage.SearchBox.Value = "TEST";

                //an example of interacting with the config if needed. This gets expected title from config. 
                var expectedTitle = config.GetPageSetting("home").Title;

                //check the titles match
                Assert.Equal(expectedTitle, homePage.Title);
            }
        }

        [Theory]
        [InlineData(TestTarget.Chrome)]
        public void Title_OnGoogleTermsPageNoConfig_IsGoogle(TestTarget target)
        {
            //create a driver with default configuration
            using (IDriver driver = GetDriver(target))
            {
                //create page model for test
                var termsPage = Page.Create<GoogleTermsPage>(driver).Go<GoogleTermsPage>();

                //check the titles match
                Assert.Equal("Google Terms of Service – Privacy & Terms – Google", termsPage.Title);
            }
        }

        [Theory]
        [InlineData(TestTarget.Chrome)]
        public void Navigate_FromHomeToTerms_TitleIsTerms(TestTarget target)
        {

            using (IDriver driver = GetDriver(target))
            {
                var homePage = Page.Create<GoogleHomePage>(driver).Go<GoogleHomePage>();
                var termsPage = homePage.TermsLink.Click();

                Assert.Equal("Google Terms of Service – Privacy & Terms – Google", termsPage.Title);
            }
        }

        public IConfiguration GetDriverConfig(TestTarget target)
        {
            IConfigurationRoot config = GetConfigurationRoot();
            //create a IConfiguration using DefaultConfig. Create your own if needed but first explore the options in Microsoft's ConfigurationBuilder
            var configModel = new DefaultConfig(target, config);
            return configModel;
        }

        private static IConfigurationRoot GetConfigurationRoot()
        {
            var builder = new ConfigurationBuilder();
            var testFolder = new DirectoryInfo(Directory.GetCurrentDirectory()).FullName;
            builder.SetBasePath(testFolder);
            //use json file to configure settings. See http://docs.asp.net/en/latest/fundamentals/configuration.html for more detail on CongifurationBuilder
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config;
        }

    }
}
