using System;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Edge;
using System.Threading;
using UiMatic.SeleniumWebDriver.Controls;
using System.Collections.Generic;

namespace UiMatic.SeleniumWebDriver
{
    public class Page : ViewContainer
    {
        protected string baseUrl;
        public string Title { get { return driver.AsIWebDriver().Title; } }

        #region Element
        protected IDriver driver;
        public IConfiguration Configuration { get; protected set; }

        protected void WaitUntil(Predicate<IWebElement> breakWhen, IWebElement el, int timeout = 60)
        {
            for (int second = 0; ; second++)
            {
                if (second >= timeout)
                    throw new TimeoutException();
                try
                {
                    if (breakWhen.Invoke(el))
                        break;
                }
                catch (Exception)
                {
                }
                Thread.Sleep(1000);
            }
        }
        #endregion

        public Page(string baseUrl, IDriver driver)// : base(driver, configuration)
        {
            this.driver = driver;
            this.baseUrl = baseUrl;
            this.Configuration = driver.Configuration;
        }

        public Page(IDriver driver)// : base(driver, configuration)
        {
            this.driver = driver;
            this.baseUrl = driver.AsIWebDriver().Url;
            this.Configuration = driver.Configuration;
        }

        public static TPage Create<TPage>(string baseUrl, IDriver driver) where TPage : Page
        {
            //if(driver.GetType() == typeof(ChromeDriver))
            //    return new ChromeHomePage(baseUrl, driver);
            //TODO: look for driver specific page
            var page = (TPage)Activator.CreateInstance(typeof(TPage), driver);
            page.baseUrl = baseUrl;
            //page.Configuration = driver.Configuration;
            //populate IClickable, INavigate, and IInput, etc.
            PopulatePageProperties<TPage>(page, driver);

            return page;
        }
 
        public static TPage Create<TPage>(IDriver driver) where TPage : Page
        {
            var url = GetPageBaseUrl(typeof(TPage), driver.Configuration);
            TPage page = null;

            if(string.IsNullOrEmpty(url))
            {
                page = (TPage)Activator.CreateInstance(typeof(TPage), driver);
                page.Configuration = driver.Configuration;
                return PopulatePageProperties<TPage>(page, driver);
            }                
            else
            {
                return Create<TPage>(url, driver);
            }
                            
        }

        private static string GetPageBaseUrl(Type type, IConfiguration configuration)
        {
            var urlAttr = type.GetCustomAttribute<UrlAttribute>(true);

            if (urlAttr == null)
            {
                var key = type.Name;
                var page = configuration.GetPageSetting(key);
                if (page != null)
                {
                    return page.Url;
                }
                else return null;
            }

            if (!string.IsNullOrEmpty(urlAttr.Address))
            {
                return urlAttr.Address;
            }

            
            if (!string.IsNullOrEmpty(urlAttr.Key) && configuration != null)
            {
                var page = configuration.GetPageSetting(urlAttr.Key);
                if(page != null)
                {
                    return page.Url;
                }                
            }

            return null;
        }

        private static TPage PopulatePageProperties<TPage>(TPage page, IDriver driver)
        {
            //populate IClickable, INavigate, and IInput, etc.
            var properties = page.GetType().GetProperties();
            foreach (var prop in properties)
            {
                ProcessClickables<TPage>(prop, driver, page);
                ProcessNavigators<TPage>(prop, driver, page);
                ProcessInput<TPage>(prop, driver, page);
                ProcessDropDownSelect<TPage>(prop, driver, page);
                ProcessMultiSelect<TPage>(prop, driver, page);
                ProcessRadioButtonGroup<TPage>(prop, driver, page);
                ProcessCheckBox<TPage>(prop, driver, page);
            }
            return page;
        }

        private static void ProcessNavigators<TPage>(PropertyInfo prop, IDriver driver, TPage page)
        {
            Type propType = prop.PropertyType;

            if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(INavigate<>))//TODO: also need to cater for generic - whole other method?
            {
                var tToMake = typeof(NavigateElement<>);
                var genericArgs = propType.GetGenericArguments();
                Type constructed = tToMake.MakeGenericType(genericArgs);
                var el = Activator.CreateInstance(constructed, driver);
                Selector s = GetSelector(prop, el);
                var vObj = el as IHasSelector;
                if(vObj != null)
                    vObj.Selector = s;

                prop.SetValue(page, el);
            }
        }

        private static void ProcessInput<TPage>(PropertyInfo prop, IDriver driver, TPage page)
        {
            if (prop.PropertyType == typeof(IInput))
            {
                var el = new TextInputElement(driver);
                Selector s = GetSelector(prop, el);
                el.Selector = s;
                prop.SetValue(page, el);
            }
        }

        private static void ProcessDropDownSelect<TPage>(PropertyInfo prop, IDriver driver, TPage page)
        {
            if (prop.PropertyType == typeof(IDropDownSelect))
            {
                var el = new DropDownSelect(driver);
                Selector s = GetSelector(prop, el);
                el.Selector = s;
                prop.SetValue(page, el);
            }
        }

        private static void ProcessMultiSelect<TPage>(PropertyInfo prop, IDriver driver, TPage page)
        {
            if (prop.PropertyType == typeof(IMultiSelect))
            {
                var el = new MultiSelect(driver);
                Selector s = GetSelector(prop, el);
                el.Selector = s;
                prop.SetValue(page, el);
            }
        }

        private static void ProcessRadioButtonGroup<TPage>(PropertyInfo prop, IDriver driver, TPage page)
        {
            if (prop.PropertyType == typeof(IRadioGroup))
            {
                var el = new RadioGroup(driver);
                Selector s = GetSelector(prop, el);
                el.Selector = s;
                prop.SetValue(page, el);
            }
        }

        private static void ProcessCheckBox<TPage>(PropertyInfo prop, IDriver driver, TPage page)
        {
            if (prop.PropertyType == typeof(ICheckBox))
            {
                var el = new CheckBoxElement(driver);
                Selector s = GetSelector(prop, el);
                el.Selector = s;
                prop.SetValue(page, el);
            }
        }

        private static void ProcessClickables<TPage>(PropertyInfo prop, IDriver driver, TPage page)
        {
            if (prop.PropertyType == typeof(IClickable))
            {
                var el = new ClickableElement(driver);
                Selector s = GetSelector(prop, el);
                el.Selector = s;
                prop.SetValue(page, el);
            }
        }

        private static Selector GetSelector(PropertyInfo prop, object el)
        {
            Selector selector = null;
            if (TryGetSelector(prop, out selector))
                return selector;

            selector = new Selector();
            var opinionatedElement = el as IHavePreferredSelectorType;
            if(opinionatedElement != null)
            {
                selector.SelectorType = opinionatedElement.PreferredSelectorType;
                selector.SelectorValue = prop.Name;
                return selector;
            }

            selector.SelectorType = SelectorType.Id;
            selector.SelectorValue = prop.Name;
            return selector;
        }

        private static bool TryGetSelector(PropertyInfo prop, out Selector selector)
        {
            selector = new Selector();

            var attribute = prop.GetCustomAttribute<SelectorAttribute>();
            if (attribute == null)
                return false;            
            
            if (!string.IsNullOrEmpty(attribute.Css))
            {
                selector.SelectorType = SelectorType.ClassName;
                selector.SelectorValue = attribute.Css;
                return true;
            }

            if(!string.IsNullOrEmpty(attribute.Name))
            {
                selector.SelectorType = SelectorType.Name;
                selector.SelectorValue = attribute.Name;
                return true;
            }

            if(!string.IsNullOrEmpty(attribute.Id))
            {
                selector.SelectorType = SelectorType.Id;
                selector.SelectorValue = attribute.Id;
                return true;
            }

            if (!string.IsNullOrEmpty(attribute.XPath))
            {
                selector.SelectorType = SelectorType.XPath;
                selector.SelectorValue = attribute.XPath;
                return true;
            }

            return false;
        }

        //Selenium specific

        protected TestTarget CurrentTestTarget
        {
            get
            {
                if (typeof(ChromeDriver) == driver.GetType())
                    return TestTarget.Chrome;

                if (typeof(EdgeDriver) == driver.GetType())
                    return TestTarget.Edge;

                if (typeof(FirefoxDriver) == driver.GetType())
                    return TestTarget.Firefox;

                if (typeof(InternetExplorerDriver) == driver.GetType())
                    return TestTarget.IE;

                if (typeof(SafariDriver) == driver.GetType())
                    return TestTarget.Safari;

                //TODO: check what remote browser is running
                ICapabilities capabilities = ((RemoteWebDriver)driver).Capabilities;

                return TestTarget.SauceLabsFirefox;
            }
        }

        public TPage Go<TPage>() where TPage : Page
        {
            driver.Navigate(this.baseUrl);
            return (TPage)this;
        }

        
        public TPage Go<TPage>(TPage page) where TPage : Page
        {
            return Go<TPage>();
        }

        public void Go()
        {
            driver.Navigate(this.baseUrl);
        }

        #region Search
        public override IElement FindById(string selector)
        {
            return driver.FindById(selector);
        }
        public override IElement FindByName(string selector)
        {
            return driver.FindByName(selector);
        }
        public override IElement FindByCss(string selector)
        {
            return driver.FindByCss(selector);
        }
        public override IElement FindByXpath(string selector)
        {
            return driver.FindByXpath(selector);
        }
        public override IEnumerable<IElement> FindElementsByCss(string selector)
        {
            return driver.FindElementsByCss(selector);
        }

        public override IEnumerable<IElement> FindElementsById(string selector)
        {
            return driver.FindElementsById(selector);
        }

        public override IEnumerable<IElement> FindElementsByName(string selector)
        {
            return driver.FindElementsByName(selector);
        }

        public override IEnumerable<IElement> FindElementsByTagName(string selector)
        {
            return driver.FindElementsByTagName(selector);
        }

        public override IEnumerable<IElement> FindElementsByXpath(string selector)
        {
            return driver.FindElementsByXpath(selector);
        }

        #endregion

    }
}
