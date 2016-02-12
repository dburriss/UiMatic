namespace ChimpLab.UiMatic.SeleniumWebDriver.IntegrationTests.Pages
{
    [Url(key: "pages:home:url")]
    public class GoogleHomePage : Page
    {
        [Selector(name: "q")]
        public IInput SearchBox { get; set; }

        public GoogleHomePage(IDriver driver, IConfiguration config) : base(driver, config)
        {}
    }
}
