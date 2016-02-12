using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;

namespace ChimpLab.UiMatic.SeleniumWebDriver
{
    public class WebDriverElement : IElement
    {
        public IWebElement _WebElement { get; private set; }
        

        public WebDriverElement(IWebElement el)
        {
            _WebElement = el;
        }

        public bool IsVisible
        {
            get
            {
                return _WebElement.Displayed;
            }
        }

        public string Text
        {
            get
            {
                return _WebElement.Text;
            }
            set
            {
                _WebElement.SendKeys(value);
            }
        }

        public void Click()
        {
            _WebElement.Click();
        }

        public string GetAttribute(string name)
        {
            return _WebElement.GetAttribute(name);
        }

        public IElement FindById(string selector)
        {
            return _WebElement.FindElement(By.Id(selector)).ToElement();
        }

        public IElement FindByName(string selector)
        {
            return _WebElement.FindElement(By.Name(selector)).ToElement();
        }

        public IElement FindByCss(string selector)
        {
            return _WebElement.FindElement(By.ClassName(selector)).ToElement();
        }

        public IElement FindByXpath(string selector)
        {
            return _WebElement.FindElement(By.XPath(selector)).ToElement();
        }

        public IEnumerable<IElement> FindElementsById(string selector)
        {
            return _WebElement.FindElements(By.Id(selector)).Select(x => x.ToElement());
        }

        public IEnumerable<IElement> FindElementsByName(string selector)
        {
            return _WebElement.FindElements(By.Name(selector)).Select(x => x.ToElement());
        }

        public IEnumerable<IElement> FindElementsByCss(string selector)
        {
            return _WebElement.FindElements(By.ClassName(selector)).Select(x => x.ToElement());
        }

        public IEnumerable<IElement> FindElementsByXpath(string selector)
        {
            return _WebElement.FindElements(By.XPath(selector)).Select(x => x.ToElement());
        }

        public IEnumerable<IElement> FindElementsByTagName(string selector)
        {
            return _WebElement.FindElements(By.TagName(selector)).Select(x => x.ToElement());
        }
    }
}
