using System;

namespace UiMatic.SeleniumWebDriver.Controls
{
    public class NavigateElement<TPage> : INavigate<TPage> where TPage : Page
    {
        protected readonly IDriver driver;

        //public Action ClickAction { get; set; }
        public Selector Selector { get; set; }
        public SelectorType PreferredSelectorType { get { return SelectorType.Id; } }

        public TPage Click()
        {
            bool clicked = false;
            if (Selector.SelectorType == SelectorType.Name)
            {
                var el = this.driver.FindByName(this.Selector.SelectorValue);
                el.Click();
                clicked = true;
            }

            if (Selector.SelectorType == SelectorType.Id)
            {
                var el = this.driver.FindById(this.Selector.SelectorValue);
                el.Click();
                clicked = true;
            }

            if (Selector.SelectorType == SelectorType.ClassName)
            {
                var el = this.driver.FindByCss(this.Selector.SelectorValue);
                el.Click();
                clicked = true;
            }

            if (Selector.SelectorType == SelectorType.XPath)
            {
                var el = this.driver.FindByXpath(this.Selector.SelectorValue);
                el.Click();
                clicked = true;
            }

            if(clicked)
                return Page.Create<TPage>(this.driver);

            throw new Exception("Not sure what to do.");
        }


        //public NavigateElement(IWebDriver driver)
        //    : base(driver, null)
        //{

        //}

        public NavigateElement(IDriver driver)
        {
            this.driver = driver;
        }


    }
}