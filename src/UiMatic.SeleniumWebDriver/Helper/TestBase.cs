using OpenQA.Selenium;
using System;
namespace ChimpLab.UiMatic.SeleniumWebDriver
{
    public class TestBase
    {
        protected IWebDriver VerifyToContinue(Func<TestTarget, IWebDriver> factoryFunc, TestTarget target)
        {
            var driver = factoryFunc.Invoke(target);
            if (driver == null)
                throw new NullReferenceException("driver not found for environment: " + target.ToString());
            return driver;
        }
    }
}