using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ChimpLab.UiMatic.SeleniumWebDriver
{
    public class WebDriver : IDriver
    {
        public IWebDriver _WebDriver { get; private set; }
        public WebDriver(IWebDriver driver)
        {
            this._WebDriver = driver;
        }

        public IElement FindById(string selector)
        {
            return _WebDriver.FindElement(By.Id(selector)).ToElement();
        }

        public IElement FindByName(string selector)
        {
            return _WebDriver.FindElement(By.Name(selector)).ToElement();
        }

        public IElement FindByCss(string selector)
        {
            return _WebDriver.FindElement(By.ClassName(selector)).ToElement();
        }

        public IElement FindByXpath(string selector)
        {
            return _WebDriver.FindElement(By.XPath(selector)).ToElement();
        }

        public IEnumerable<IElement> FindElementsById(string selector)
        {
            return _WebDriver.FindElements(By.Id(selector)).Select(x => x.ToElement());
        }

        public IEnumerable<IElement> FindElementsByName(string selector)
        {
            return _WebDriver.FindElements(By.Name(selector)).Select(x => x.ToElement());
        }

        public IEnumerable<IElement> FindElementsByCss(string selector)
        {
            return _WebDriver.FindElements(By.ClassName(selector)).Select(x => x.ToElement());
        }

        public IEnumerable<IElement> FindElementsByXpath(string selector)
        {
            return _WebDriver.FindElements(By.XPath(selector)).Select(x => x.ToElement());
        }

        public IEnumerable<IElement> FindElementsByTagName(string selector)
        {
            return _WebDriver.FindElements(By.TagName(selector)).Select(x => x.ToElement());
        }

        public void Navigate(ViewContainer viewContainer)
        {
            throw new NotImplementedException();
        }

        public void Navigate(string path)
        {
            _WebDriver.Navigate().GoToUrl(path);
        }

        public void Dispose()
        {
            _WebDriver.Dispose();
        }
    }
}