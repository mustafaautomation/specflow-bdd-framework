using FluentAssertions;
using OpenQA.Selenium;
using Quvantic.SpecFlowBDD.Pages;
using TechTalk.SpecFlow;

namespace Quvantic.SpecFlowBDD.StepDefinitions;

[Binding]
public class LoginSteps
{
    private readonly LoginPage _loginPage;

    public LoginSteps(ScenarioContext scenarioContext)
    {
        var driver = scenarioContext.Get<IWebDriver>("WebDriver");
        _loginPage = new LoginPage(driver);
    }

    [Given(@"I am on the login page")]
    public void GivenIAmOnTheLoginPage()
    {
        _loginPage.Navigate();
    }

    [When(@"I login with ""(.*)"" and ""(.*)""")]
    public void WhenILoginWith(string username, string password)
    {
        _loginPage.Login(username, password);
    }

    [Then(@"I should be on the inventory page")]
    public void ThenIShouldBeOnTheInventoryPage()
    {
        _loginPage.IsOnInventoryPage().Should().BeTrue();
    }

    [Then(@"I should see the error ""(.*)""")]
    public void ThenIShouldSeeTheError(string expectedError)
    {
        _loginPage.GetErrorMessage().Should().Contain(expectedError);
    }
}
