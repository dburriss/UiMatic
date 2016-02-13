using OpenQA.Selenium;
using System;
namespace UiMatic.SeleniumWebDriver
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

        protected IDriver GetDriver(TestTarget target, IConfiguration configuration)
        {
            //here we creating an IDriver using Selenium as the implementation with configuration
            return VerifyToContinue((t) => DriverFactory.Create(configuration), target).ToIDriver(configuration);
        }

        protected IDriver GetDriver(TestTarget target)
        {
            IConfiguration defaultConfig = null;
            //here we creating an IDriver using Selenium as the implementation
            return VerifyToContinue((t) => DriverFactory.Create(target, out defaultConfig), target).ToIDriver(defaultConfig);
        }
    }
}