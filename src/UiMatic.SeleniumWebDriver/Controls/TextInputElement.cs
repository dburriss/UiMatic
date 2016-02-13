namespace UiMatic.SeleniumWebDriver.Controls
{
    public class TextInputElement : IInput
    {
        private readonly IDriver driver;

        public Selector Selector { get; set; }
        public SelectorType PreferredSelectorType { get { return SelectorType.Name; } }

        public string Value
        {
            get
            {
                var el = this.Selector.ReturnElement(this.driver);
                return el.GetAttribute("value");
            }
            set
            {
                var el = this.Selector.ReturnElement(this.driver);
                el.Text = value;
            }
        }
        
        public TextInputElement(IDriver driver)
        {
            this.driver = driver;
        }
    }
}