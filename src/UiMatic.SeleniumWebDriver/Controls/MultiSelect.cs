using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq.Expressions;
using System;
using System.Linq;
using OpenQA.Selenium.Interactions;

namespace ChimpLab.UiMatic.SeleniumWebDriver.Controls
{
    public class MultiSelect : IMultiSelect
    {
        private readonly IDriver driver;

        public Selector Selector { get; set; }
        public SelectorType PreferredSelectorType { get { return SelectorType.Name; } }
        public MultiSelect(IDriver driver) 
        {
            this.driver = driver;
        }

        public IEnumerable<IMultiSelectionOption> Options
        {
            get
            {
                var el = this.Selector.ReturnElement(this.driver);
                return ExtractOptions(el);
            }
        }

        private IEnumerable<IMultiSelectionOption> ExtractOptions(IElement el)
        {

            IList<IElement> options = el.FindElementsByTagName("option").ToList();
            foreach (var option in options)
            {
                yield return new MultiSelectionOption(this.driver, option);
            }
        }

        public bool SelectWhere(Expression<Func<IMultiSelectionOption, bool>> filter)
        {
            var filtered = Options.AsQueryable().Where(filter);

            if (!filtered.Any())
                return true;

            //Actions actions = new Actions(this.driver);
            var count = 0;
            foreach (var item in filtered)
            {
                if (count == 0)
                    item.Click();
                else
                    item.ControlClick();

                count++;
            }
            return count > 0;
        }

        public IEnumerable<IMultiSelectionOption> SelectedValues
        {
            get
            {
                var els = Selector.ReturnElement(this.driver).FindElementsByXpath("./option[@selected]");
                foreach (var item in els)
                {
                    yield return new MultiSelectionOption(this.driver, item);
                }                
            }
        }
    }

    public class MultiSelectionOption : SelectionOption, IMultiSelectionOption
    {
        private WebDriver driver;
        public MultiSelectionOption(IDriver driver, IElement el) : base(el)
        {
            this.driver = (WebDriver)driver;
        }

        public void ControlClick()
        {
            if (element != null)
            {
                IWebElement webElement = ((WebDriverElement)element)._WebElement;
                Actions actions = new Actions(this.driver._WebDriver);
                actions.MoveToElement(webElement).KeyDown(Keys.Control).Click().KeyUp(Keys.Control);
                actions.Build().Perform();
            }
        }
    }
}
