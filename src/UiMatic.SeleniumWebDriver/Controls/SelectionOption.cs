namespace UiMatic.SeleniumWebDriver.Controls
{
    public class SelectionOption : ISelectionOption
    {
        protected IElement element;
        public string Text { get; private set; }
        public string Value { get; private set; }

        public SelectionOption(IElement el)
        {
            element = el;
            var text = el.Text;
            var value = el.GetAttribute("value");
            Text = text;
            Value = value;
        }

        public void Click()
        {
            if (element != null)
                element.Click();
        }
    }
}
