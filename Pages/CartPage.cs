using OpenQA.Selenium;

namespace Quvantic.SpecFlowBDD.Pages;

public class CartPage
{
    private readonly IWebDriver _driver;

    private IWebElement CartList => _driver.FindElement(By.CssSelector(".cart_list"));
    private IWebElement CheckoutButton => _driver.FindElement(By.Id("checkout"));

    public CartPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public bool ContainsProduct(string productName) => CartList.Text.Contains(productName);

    public void Checkout() => CheckoutButton.Click();
}
