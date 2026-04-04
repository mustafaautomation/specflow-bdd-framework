using FluentAssertions;
using OpenQA.Selenium;
using Quvantic.SpecFlowBDD.Pages;
using TechTalk.SpecFlow;

namespace Quvantic.SpecFlowBDD.StepDefinitions;

[Binding]
public class CheckoutSteps
{
    private readonly CartPage _cartPage;
    private readonly CheckoutPage _checkoutPage;

    public CheckoutSteps(ScenarioContext scenarioContext)
    {
        var driver = scenarioContext.Get<IWebDriver>("WebDriver");
        _cartPage = new CartPage(driver);
        _checkoutPage = new CheckoutPage(driver);
    }

    [When(@"I proceed to checkout")]
    public void WhenIProceedToCheckout()
    {
        _cartPage.Checkout();
    }

    [When(@"I fill checkout info with ""(.*)"" ""(.*)"" ""(.*)""")]
    public void WhenIFillCheckoutInfo(string firstName, string lastName, string postalCode)
    {
        _checkoutPage.FillInfo(firstName, lastName, postalCode);
    }

    [When(@"I continue checkout")]
    public void WhenIContinueCheckout()
    {
        _checkoutPage.Continue();
    }

    [When(@"I finish the order")]
    public void WhenIFinishTheOrder()
    {
        _checkoutPage.Finish();
    }

    [Then(@"the order should be complete")]
    public void ThenTheOrderShouldBeComplete()
    {
        _checkoutPage.IsOrderComplete().Should().BeTrue();
    }

    [Then(@"I should see checkout error ""(.*)""")]
    public void ThenIShouldSeeCheckoutError(string expectedError)
    {
        _checkoutPage.GetError().Should().Contain(expectedError);
    }
}
