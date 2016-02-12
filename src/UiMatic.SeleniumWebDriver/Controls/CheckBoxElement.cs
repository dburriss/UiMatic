using System;

namespace ChimpLab.UiMatic.SeleniumWebDriver.Controls
{
    public class CheckBoxElement : ICheckBox
    {
        private readonly IDriver driver;

        public Selector Selector { get; set; }
        public SelectorType PreferredSelectorType { get { return SelectorType.Name; } }
        public string Value {
            get
            {
                var el = this.Selector.ReturnElement(this.driver);
                return el.GetAttribute("value");
            }
        }

        public bool IsChecked
        {
            get
            {
                var el = this.Selector.ReturnElement(this.driver);
                return el.GetAttribute("checked") != null && el.GetAttribute("checked").ToLower() == "true";
            }
        }

        public void Click()
        {
            if(Selector.SelectorType == SelectorType.Name)
            {
                var el = this.driver.FindByName(Selector.SelectorValue);
                el.Click();
                return;
            }

            if(Selector.SelectorType == SelectorType.Id)
            {
                var el = this.driver.FindById(Selector.SelectorValue);
                el.Click();
                return;
            }

            if(Selector.SelectorType == SelectorType.ClassName)
            {
                var el = this.driver.FindByCss(Selector.SelectorValue);
                el.Click();
                return;
            }

            if (Selector.SelectorType == SelectorType.XPath)
            {
                var el = this.driver.FindByXpath(Selector.SelectorValue);
                el.Click();
                return;
            }

            throw new Exception("Not sure what selector to use.");
        }

        public void Click(Action action)
        {
            Click();
            action();
        }


        public CheckBoxElement(IDriver driver)
        {
            this.driver = driver;
        }

    }
}