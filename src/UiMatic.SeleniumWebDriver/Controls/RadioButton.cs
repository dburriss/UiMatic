namespace UiMatic.SeleniumWebDriver.Controls
{
    public class RadioButton : IRadioButton
    {
        private IElement element;
        public string Name { get; private set; }
        public string Value { get; private set; }

        public RadioButton(IElement el)
        {
            element = el;
            var name = el.GetAttribute("name");
            var value = el.GetAttribute("value");
            Name = Name;
            Value = value;
        }

        public void Click()
        {
            if (element != null)
                element.Click();
        }
    }
}
