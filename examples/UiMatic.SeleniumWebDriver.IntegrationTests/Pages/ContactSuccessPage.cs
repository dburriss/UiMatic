namespace UiMatic.SeleniumWebDriver.IntegrationTests.Pages
{
    [Url(key: "contact-success")]
    public class ContactSuccessPage : Page
    {
        public string GetMessage()
        {
            //example of manually interacting rather than using property types
            return FindById("message").Text;
        }

        public ContactSuccessPage(IDriver driver) : base(driver)
        {}
    }
}
