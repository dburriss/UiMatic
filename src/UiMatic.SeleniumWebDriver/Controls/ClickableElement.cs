using System;

namespace ChimpLab.UiMatic.SeleniumWebDriver.Controls
{
    public class ClickableElement : IClickable
    {
        private IDriver driver;

        //public Action ClickAction { get; set; }
        public Selector Selector { get; set; }
        public SelectorType PreferredSelectorType { get { return SelectorType.Id; } }

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


        public ClickableElement(IDriver driver)
        {
            this.driver = driver;
        }

    }
}