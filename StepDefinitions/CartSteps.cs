using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Quvantic.SpecFlowBDD.StepDefinitions;

/// <summary>
/// Cart-specific steps. Adding/removing products and the badge count are shared with the
/// inventory feature and live in <see cref="InventorySteps"/>.
/// </summary>
[Binding]
public class CartSteps
{
    private readonly IWebDriver _driver;

    public CartSteps(ScenarioContext scenarioContext)
    {
        _driver = scenarioContext.Get<IWebDriver>("WebDriver");
    }

    [When(@"I navigate to the cart")]
    public void WhenINavigateToTheCart()
    {
        _driver.FindElement(By.CssSelector(".shopping_cart_link")).Click();
    }

    [When(@"I click checkout")]
    public void WhenIClickCheckout()
    {
        _driver.FindElement(By.Id("checkout")).Click();
    }

    [Then(@"the cart badge should not be visible")]
    public void ThenTheCartBadgeShouldNotBeVisible()
    {
        var badges = _driver.FindElements(By.CssSelector(".shopping_cart_badge"));
        Assert.That(badges.Count, Is.EqualTo(0));
    }

    [Then(@"the cart should contain ""(.*)""")]
    public void ThenTheCartShouldContain(string productName)
    {
        var cartText = _driver.FindElement(By.CssSelector(".cart_list")).Text;
        Assert.That(cartText, Does.Contain(productName));
    }

    [Then(@"I should be on the checkout step one page")]
    public void ThenIShouldBeOnCheckoutStepOne()
    {
        Assert.That(_driver.Url, Does.Contain("checkout-step-one"));
    }
}
