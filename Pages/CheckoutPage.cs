using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Quvantic.ReqnrollBDD.Support;

namespace Quvantic.ReqnrollBDD.Pages;

public class CheckoutPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private IWebElement FirstNameInput => _driver.FindElement(By.Id("first-name"));
    private IWebElement LastNameInput => _driver.FindElement(By.Id("last-name"));
    private IWebElement PostalCodeInput => _driver.FindElement(By.Id("postal-code"));
    private IWebElement ContinueButton => _driver.FindElement(By.Id("continue"));
    private IWebElement FinishButton => _driver.FindElement(By.Id("finish"));
    private IWebElement CompleteHeader => _driver.FindElement(By.CssSelector(".complete-header"));
    private IWebElement TotalLabel => _driver.FindElement(By.CssSelector("[data-test='total-label']"));
    private IWebElement ErrorMessage => _driver.FindElement(By.CssSelector("[data-test='error']"));

    public CheckoutPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TestConfig.TimeoutSeconds));
    }

    public void FillInfo(string firstName, string lastName, string postalCode)
    {
        FirstNameInput.Clear(); FirstNameInput.SendKeys(firstName);
        LastNameInput.Clear(); LastNameInput.SendKeys(lastName);
        PostalCodeInput.Clear(); PostalCodeInput.SendKeys(postalCode);
    }

    public void Continue() => ContinueButton.Click();
    public void Finish() => FinishButton.Click();

    public string GetTotal() => TotalLabel.Text;

    public bool IsOrderComplete()
    {
        _wait.Until(d => CompleteHeader.Displayed);
        return CompleteHeader.Text.Contains("Thank you");
    }

    public string GetError()
    {
        _wait.Until(d => ErrorMessage.Displayed);
        return ErrorMessage.Text;
    }
}
