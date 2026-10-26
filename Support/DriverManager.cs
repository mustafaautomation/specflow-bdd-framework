using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Quvantic.SpecFlowBDD.Support;

public static class DriverManager
{
    public static IWebDriver CreateDriver()
    {
        // Selenium Manager (built into Selenium 4.6+) resolves a ChromeDriver that matches the
        // installed Chrome, so no separate driver download step is needed.
        var options = new ChromeOptions();
        if (TestConfig.Headless)
        {
            options.AddArguments("--headless", "--no-sandbox", "--disable-dev-shm-usage");
        }
        options.AddArguments("--window-size=1920,1080");
        // SauceDemo's public password is on Chrome's leaked-password list; without this Chrome
        // opens a password-manager bubble after login that swallows the next clicks.
        options.AddUserProfilePreference("credentials_enable_service", false);
        options.AddUserProfilePreference("profile.password_manager_enabled", false);
        options.AddUserProfilePreference("profile.password_manager_leak_detection", false);
        options.AddArgument("--disable-features=PasswordLeakDetection");

        var driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestConfig.TimeoutSeconds);
        return driver;
    }
}
