using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Quvantic.SpecFlowBDD.Support;

public static class DriverManager
{
    public static IWebDriver CreateDriver()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());

        var options = new ChromeOptions();
        if (TestConfig.Headless)
        {
            options.AddArguments("--headless", "--no-sandbox", "--disable-dev-shm-usage");
        }
        options.AddArguments("--window-size=1920,1080");

        var driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestConfig.TimeoutSeconds);
        return driver;
    }
}
