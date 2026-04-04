using FluentAssertions;
using OpenQA.Selenium;
using Quvantic.SpecFlowBDD.Pages;
using Quvantic.SpecFlowBDD.Support;
using TechTalk.SpecFlow;

namespace Quvantic.SpecFlowBDD.StepDefinitions;

[Binding]
public class InventorySteps
{
    private readonly LoginPage _loginPage;
    private readonly InventoryPage _inventoryPage;

    public InventorySteps(ScenarioContext scenarioContext)
    {
        var driver = scenarioContext.Get<IWebDriver>("WebDriver");
        _loginPage = new LoginPage(driver);
        _inventoryPage = new InventoryPage(driver);
    }

    [Given(@"I am logged in as a standard user")]
    public void GivenIAmLoggedInAsAStandardUser()
    {
        _loginPage.Navigate();
        _loginPage.Login(TestConfig.StandardUser, TestConfig.Password);
        _loginPage.IsOnInventoryPage().Should().BeTrue();
    }

    [Then(@"I should see (\d+) products on the inventory page")]
    public void ThenIShouldSeeProducts(int count)
    {
        _inventoryPage.GetItemCount().Should().Be(count);
    }

    [When(@"I add ""(.*)"" to the cart")]
    public void WhenIAddToTheCart(string productName)
    {
        _inventoryPage.AddToCartByName(productName);
    }

    [When(@"I remove ""(.*)"" from the cart")]
    public void WhenIRemoveFromTheCart(string productName)
    {
        _inventoryPage.RemoveFromCartByName(productName);
    }

    [Then(@"the cart badge should show ""(.*)""")]
    public void ThenTheCartBadgeShouldShow(string expected)
    {
        _inventoryPage.GetCartBadgeCount().Should().Be(expected);
    }

    [When(@"I open the cart")]
    public void WhenIOpenTheCart()
    {
        _inventoryPage.OpenCart();
    }
}
