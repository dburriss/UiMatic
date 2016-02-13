using System;
using System.Collections.Generic;

namespace UiMatic
{
    public class Selector
    {
        public SelectorType SelectorType { get; set; }
        public string SelectorValue { get; set; }

        public IElement ReturnElement(IDriver driver)
        {
            IElement el = null;
            if(SelectorType == SelectorType.Name)
            {
                el = driver.FindByName(SelectorValue);
            }

            if(SelectorType == SelectorType.Id)
            {
                el = driver.FindById(SelectorValue);
            }

            if(SelectorType == SelectorType.ClassName)
            {
                el = driver.FindByCss(SelectorValue);
            }

            if (SelectorType == SelectorType.XPath)
            {
                el = driver.FindByXpath(SelectorValue);
            }

            if (el == null)
                throw new InvalidOperationException(this.SelectorValue);

            return el;
        }

        public IEnumerable<IElement> ReturnElements(IDriver driver)
        {
            IEnumerable<IElement> elements = null;
            if (SelectorType == SelectorType.Name)
            {
                elements = driver.FindElementsByXpath(SelectorValue);
            }

            if (SelectorType == SelectorType.Id)
            {
                elements = driver.FindElementsById(SelectorValue);

            }

            if (SelectorType == SelectorType.ClassName)
            {
                elements = driver.FindElementsByCss(SelectorValue);
            }

            if (SelectorType == SelectorType.XPath)
            {
                elements = driver.FindElementsByXpath(SelectorValue);
            }

            if (elements == null)
                throw new InvalidOperationException(this.SelectorValue);

            return elements;
        }
    }
}
