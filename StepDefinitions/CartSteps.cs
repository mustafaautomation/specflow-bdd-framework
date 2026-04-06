using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Quvantic.SpecFlowBDD.StepDefinitions;

[Binding]
public class CartSteps
{
    private readonly IWebDriver _driver;

    public CartSteps(ScenarioContext context)
    {
        _driver = (IWebDriver)context["driver"];
    }

    [When(@"I add ""(.*)"" to the cart")]
    public void WhenIAddToTheCart(string productName)
    {
        var buttonId = $"add-to-cart-{productName.ToLower().Replace(" ", "-")}";
        _driver.FindElement(By.CssSelector($"[data-test='{buttonId}']")).Click();
    }

    [When(@"I remove ""(.*)"" from the cart")]
    public void WhenIRemoveFromTheCart(string productName)
    {
        var buttonId = $"remove-{productName.ToLower().Replace(" ", "-")}";
        _driver.FindElement(By.CssSelector($"[data-test='{buttonId}']")).Click();
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

    [Then(@"the cart badge should show ""(.*)""")]
    public void ThenTheCartBadgeShouldShow(string expected)
    {
        var badge = _driver.FindElement(By.CssSelector(".shopping_cart_badge"));
        Assert.That(badge.Text, Is.EqualTo(expected));
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
