# UiMatic
I create page models for writing maintainable automated UI tests.

The UiMatic framework allows you to create models that represent your pages. The Type of the property and the SelectorAttribute are used to map the UI elements and provide functionality to easily interact with them in your automated tests.

## Basic Example

This example navigates to Google's Terms of Service page and checks the title matches what is expected.

First lets create a class that represents the Terms of Service web page:

```csharp
[Url(address: "https://www.google.co.za/intl/en/policies/terms/regional.html")]
public class GoogleTermsPage : Page
{
    public GoogleTermsPage(IDriver driver, IConfiguration config) : base(driver, config)
    { }
}
```

And here is the test:

```csharp
public class PageTests : TestBase
{
    [Theory]
    [InlineData(TestTarget.Chrome)]
    public void Title_OnGoogleTermsPageNoConfig_IsGoogle(TestTarget target)
    {
        //create a driver with default configuration
        using (IDriver driver = GetDriver(target))
        {
            //create page model for test
            var termsPage = Page.Create<GoogleTermsPage>(driver).Go<GoogleTermsPage>();

            //check the titles match
            Assert.Equal("Google Terms of Service – Privacy & Terms – Google", termsPage.Title);
        }
    }
}
```

The test assumes you have the Chrome selenium driver located at *C:\Selenium\chromedriver_win32*
> This needs to be updated for cross platform handling. 

Or you can use the **appsettings.json** file in the root of your test project:

```json
{
    "configuration": {
        "ChromeDriverLocation": "D:\\devtools\\Selenium\\chromedriver_win32"
    }
}
```

## Deeper Example
Here is a test that navigates to the Google home page (using Xunit test framework):

```csharp
public class PageTests : TestBase
{
    [Theory]
    [InlineData(TestTarget.Chrome)]
    public void Title_OnGoogleHomePageUsingConfig_IsGoogle(TestTarget target)
    {
        //build a custom config
        var config = GetDriverConfig(target);
        using (IDriver driver = GetDriver(target, config))
        {
            //create page model for test
            var homePage = Page.Create<GoogleHomePage>(driver);
            
            //tell browser to navigate to it
            homePage.Go<GoogleHomePage>();

            //fill a value into the text box
            homePage.SearchBox.Value = "TEST";

            //an example of interacting with the config if needed. This gets expected title from config. 
            var expectedTitle = config.GetPageSetting("home").Title;

            //check the titles match
            Assert.Equal(expectedTitle, homePage.Title);
        }
    }
    
    public static IConfiguration GetDriverConfig(TestTarget target)
    {
        IConfigurationRoot config = GetConfigurationRoot();
        //create a IConfiguration using DefaultConfig. Create your own if needed but first explore the options in Microsoft's ConfigurationBuilder
        var configModel = new DefaultConfig(target, config);
        return configModel;
    }

    private static IConfigurationRoot GetConfigurationRoot()
    {
        var builder = new ConfigurationBuilder();
        var testFolder = new DirectoryInfo(Directory.GetCurrentDirectory()).FullName;
        builder.SetBasePath(testFolder);
        //use json file to configure settings. See http://docs.asp.net/en/latest/fundamentals/configuration.html for more detail on CongifurationBuilder
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        return config;
    } 
}
```
    
Where the GoogleHomePage model looks like this:

```csharp
[Url(key: "home")]
public class GoogleHomePage : Page
{
    [Selector(name: "q")]
    public IInput SearchBox { get; set; }

    public GoogleHomePage(IDriver driver, IConfiguration config) : base(driver, config)
    {}
}
```

> Note the use of a **key** to identify the Url rather than an explicit url address. This will allow your tests to be run against different environments.

If a url is specified in `UrlAttribute` that will be used. If a **key** is specified, that will be used. If **no** `UrlAttribute` is present the page Type name will be used if it is present in the configuration.
`GoogleTermsPage` below is an example of looking up via type name.

And the **appsettings.json** file is setup like so:

```json
{
    "configuration": {
        "IEDriverLocation": "",
        "ChromeDriverLocation": "D:\\devtools\\Selenium\\chromedriver_win32"
    },
    "pages": {
        "home": {
            "title": "Search",
            "url": "http://www.google.com/"
        },
        "GoogleTermsPage": {
            "title": "Google Terms of Service – Privacy & Terms – Google",
            "url": "https://www.google.co.za/intl/en/policies/terms/regional.html"
        }
    }
}
```

## Available Control Types

Above you saw the usage of `IInput` but UiMatic has plenty other controls.

* `IInput` - your basic input textbox
* `IClickable` - for any element you want to be able to click on and do something
* `INavigate<T>` - like IClickable but returns the page model of the generic type
* `IRadioGroup` - to represent radio buttons
* `ICheckBox` - to represent a checkbox
* `IDropDownSelect` - to represent dropdowns with a single select option
* `IMultiSelect` - to represent dropdowns with a multiple select option
* `IElement` - a general purpose element you want represented in your model

## Selectors
The selector attribute allows you to select an element based on id, name, css style, or xpath.

In the Google homepage example we saw an example of a name selector `[Selector(name: "q")]` placed on the search box.
Simply placing the `SelectorAttribute` on the property representing the element on the UI being tested will bind it using the chosen selection criteria.