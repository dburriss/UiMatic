﻿using OpenQA.Selenium;

namespace UiMatic.SeleniumWebDriver
{
    public static class UiMaticWebDriverExtensions
    {
        public static IElement ToElement(this IWebElement el)
        {
            return new WebDriverElement(el);
        }

        public static IWebElement AsIWebElement(this IElement el)
        {
            return ((WebDriverElement)el)._WebElement;
        }

        public static IWebDriver AsIWebDriver(this IDriver driver)
        {
            return ((WebDriver)driver)._WebDriver;
        }

        public static IDriver ToIDriver(this IWebDriver driver, IConfiguration config)
        {
            return new WebDriver(driver)
            {
                Configuration = config
            };
        }
    }
}
