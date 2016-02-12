# UiMatic
I create page models for writing maintainable automated UI tests.

The UiMatic framework allows you to create models that represent your pages. The Type of the property and the SelectorAttribute are used to map the UI elements and provide functionality to easily interact with them in your automated tests.

## Basic Usage

Here is a test that navigates to the Google home page (using Xunit test framework):

    [Theory]
    [InlineData(TestTarget.Chrome)]
    public void Google(TestTarget target)
    {
        var config = GetDefaultConfig(target);
        using (IDriver driver = GetDriver(target, config))
        {
            var homePage = Page.Create<GoogleHomePage>("http://www.google.com/", driver, config);
            homePage.Go<GoogleHomePage>();

            homePage.SearchBox.Value = "TEST";

            Assert.Equal("Google", homePage.Title);
        }
    }
    
Where the GoogleHomePage model looks like this:

    [Url(key: "pages:home:url")]
    public class GoogleHomePage : Page
    {
        [Selector(name: "q")]
        public IInput SearchBox { get; set; }

        public GoogleHomePage(IDriver driver, IConfiguration config) : base(driver, config)
        {}
    }
    
## Configuration
The `GetDefaultConfig` method looks like this:

> NOTE: The configuration is still under development and will likely simplify greatly.

    public IConfiguration GetDefaultConfig(TestTarget target)
    {
        var configModel = new DefaultConfig(target);
        var dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        var testFolder = new DirectoryInfo(Directory.GetCurrentDirectory()).FullName;
        var provider = new JsonConfigurationProvider(Path.Combine(testFolder, "appsettings.json"));
        var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
        builder.Add(provider, true);
        var config = builder.Build();

        configModel.ChromeDriverLocation = config.GetSection("configuration")["ChromeDriverLocation"];
        configModel.CustomSettings = GetData(provider);
        return configModel;
    }

    private IDictionary<string, string> GetData(Microsoft.Extensions.Configuration.ConfigurationProvider provider)
    {
        var type = typeof(Microsoft.Extensions.Configuration.ConfigurationProvider);
        var pi = type.GetProperty("Data", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        IDictionary<string, string> data = pi.GetValue(provider, null) as IDictionary<string, string>;
        return data;
    }

    private IDriver GetDriver(TestTarget target, IConfiguration configuration)
    {
        return VerifyToContinue((t) => DriverFactory.Create(configuration), target).ToIDriver();
    }
    
And the **appsettings.json** file is setup like so:

    {
        "configuration": {
            "IEDriverLocation": "",
            "ChromeDriverLocation": "D:\\devtools\\Selenium\\chromedriver_win32"
        },
        "pages": {
            "home": {
                "title": "Search",
                "url": "http://www.google.com/"
            }
        }
    }
    
## Available Control Types

Above you saw the usage of `IInput` but UiMatic has plenty other controls.

* `IInput` - your basic input textbox
* `IClickable - for any element you want to be able to click on and do something
* `INavigate<T>` - like IClickable but returns the page model of the generic type
* `IRadioGroup` - to represent radio buttons
* `ICheckBox` - to represent a checkbox
* `IDropDownSelect` - to represent dropdowns with a single select option
* `IMultiSelect` - to represent dropdowns with a multiple select option
* `IElement` - a general purpose element you want represented in your model