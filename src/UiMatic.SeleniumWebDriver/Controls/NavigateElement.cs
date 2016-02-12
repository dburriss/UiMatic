using System;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace ChimpLab.UiMatic.SeleniumWebDriver.Controls
{ 
    public class NavigateElement<TPage> : INavigate<TPage> where TPage : Page
    {
        protected readonly IDriver driver;
        protected readonly IConfiguration Configuration;

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
                return Page.Create<TPage>(this.driver, this.Configuration);

            throw new Exception("Not sure what to do.");
        }


        //public NavigateElement(IWebDriver driver)
        //    : base(driver, null)
        //{

        //}

        public NavigateElement(IDriver driver, IConfiguration configuration)
        {
            this.driver = driver;
            this.Configuration = configuration;
        }


    }
}