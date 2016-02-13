using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace UiMatic.SeleniumWebDriver.Controls
{
    public class DropDownSelect : IDropDownSelect
    {
        private IDriver driver;

        public Selector Selector { get; set; }
        public SelectorType PreferredSelectorType { get { return SelectorType.Name; } }
        public DropDownSelect(IDriver driver)
        {
            this.driver = driver;
        }

        public IEnumerable<ISelectionOption> Options
        {
            get
            {
                var el = this.Selector.ReturnElement(driver);
                return ExtractOptions(el);
            }
        }

        private IEnumerable<ISelectionOption> ExtractOptions(IElement el)
        {
            IList<IElement> options = el.FindElementsByTagName("option").ToList();
            foreach (var option in options)
            {                
                yield return new SelectionOption(option);
            }
        }

        public bool SelectWhere(Expression<Func<ISelectionOption, bool>> filter)
        {
            var filtered = Options.AsQueryable().Where(filter);
            if(filtered.Any())
            {
                filtered.First().Click();
                return true;
            }
            return false;
        }

        public ISelectionOption SelectedValue
        {
            get
            {
                var el = Selector.ReturnElement(this.driver).FindElementsByXpath("./option[@selected]").ToArray()[0];
                return new SelectionOption(el);
            }
        }
    }

    
}
