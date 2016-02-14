namespace UiMatic.SeleniumWebDriver.IntegrationTests.Pages
{
    [Url(key: "home")]
    public class GoogleHomePage : Page
    {
        [Selector(name: "q")]
        public IInput SearchBox { get; set; }

        [Selector(name: "btnG")]
        public IClickable SearchBtn { get; set; }

        [Selector(xpath: "//*[@id='fsr']/a[2]")]
        public INavigate<GoogleTermsPage> TermsLink { get; set; }

        public GoogleHomePage(IDriver driver, IConfiguration config) : base(driver, config)
        {}
    }
}
