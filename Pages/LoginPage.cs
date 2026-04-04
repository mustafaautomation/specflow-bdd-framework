using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Quvantic.SpecFlowBDD.Support;

namespace Quvantic.SpecFlowBDD.Pages;

public class LoginPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private IWebElement UsernameInput => _driver.FindElement(By.Id("user-name"));
    private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
    private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));
    private IWebElement ErrorMessage => _driver.FindElement(By.CssSelector("[data-test='error']"));

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TestConfig.TimeoutSeconds));
    }

    public void Navigate()
    {
        _driver.Navigate().GoToUrl(TestConfig.BaseUrl);
        _wait.Until(d => LoginButton.Displayed);
    }

    public void Login(string username, string password)
    {
        UsernameInput.Clear();
        UsernameInput.SendKeys(username);
        PasswordInput.Clear();
        PasswordInput.SendKeys(password);
        LoginButton.Click();
    }

    public string GetErrorMessage()
    {
        _wait.Until(d => ErrorMessage.Displayed);
        return ErrorMessage.Text;
    }

    public bool IsOnInventoryPage()
    {
        try
        {
            _wait.Until(d => d.Url.Contains("inventory"));
            return true;
        }
        catch
        {
            return false;
        }
    }
}
