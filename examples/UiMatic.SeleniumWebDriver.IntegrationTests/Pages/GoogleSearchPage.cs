namespace UiMatic.SeleniumWebDriver.IntegrationTests.Pages
{
    [Url(key: "search")]
    public class GoogleSearchPage : Page
    {
        [Selector(name: "q")]
        public IInput SearchBox { get; set; }

        [Selector(name: "btnG")]
        public IClickable SearchBtn { get; set; }

        [Selector(xpath: "//*[@id='fsr']/a[2]")]
        public INavigate<GoogleTermsPage> TermsLink { get; set; }

        public GoogleSearchPage(IDriver driver) : base(driver)
        {}
    }
}
