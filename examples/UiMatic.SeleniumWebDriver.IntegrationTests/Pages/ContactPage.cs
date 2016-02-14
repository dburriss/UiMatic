namespace UiMatic.SeleniumWebDriver.IntegrationTests.Pages
{
    [Url(key: "contact")]
    public class ContactPage : Page
    {
        [Selector(name: "firstName")]
        public IInput FirstName { get; set; }

        [Selector(name: "lastName")]
        public IInput LastName { get; set; }

        [Selector(name: "email")]
        public IInput Email { get; set; }

        [Selector(name: "message")]
        public IInput Message { get; set; }

        [Selector(name: "gender")]
        public IRadioGroup Gender { get; set; }

        [Selector(name: "leadSource")]
        public IDropDownSelect LeadSource { get; set; }

        [Selector(name: "Skills")]
        public IMultiSelect Skills { get; set; }

        [Selector(name: "newsletter")]
        public ICheckBox Newsletter { get; set; }

        [Selector(css: "submit-btn")]
        public INavigate<ContactSuccessPage> SaveBtn { get; set; }

        public void WaitTillLoaded()
        {
            //1st arg is test to run
            //2nd arg is what to run it against
            this.WaitUntil((t) => t == "Uimatic Contact Test", () => this.Title);
        }

        public ContactPage(IDriver driver) : base(driver)
        {}
    }
}
