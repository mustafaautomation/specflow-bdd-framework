using OpenQA.Selenium;
using Quvantic.SpecFlowBDD.Support;
using TechTalk.SpecFlow;

namespace Quvantic.SpecFlowBDD.Hooks;

[Binding]
public class WebDriverHooks
{
    private readonly ScenarioContext _scenarioContext;

    public WebDriverHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        var driver = DriverManager.CreateDriver();
        _scenarioContext.Set(driver, "WebDriver");
    }

    [AfterScenario]
    public void AfterScenario()
    {
        if (_scenarioContext.TryGetValue("WebDriver", out IWebDriver driver))
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
