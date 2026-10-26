# SpecFlow BDD Framework

[![BDD Tests](https://github.com/mustafaautomation/specflow-bdd-framework/actions/workflows/bdd-tests.yml/badge.svg)](https://github.com/mustafaautomation/specflow-bdd-framework/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4.svg?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com)
[![SpecFlow](https://img.shields.io/badge/SpecFlow-3.9-6C3483.svg)](https://specflow.org)

BDD test automation framework using SpecFlow (Gherkin) + Selenium WebDriver on .NET 8. Page Object Model, scenario outlines, living documentation, and CI/CD integration.

---

## Scenarios

| Feature | Test cases | Tags |
|---------|-----------|------|
| Login | 6 (incl. Scenario Outline with 2 examples) | @smoke, @regression |
| Cart | 5 | @smoke, @regression |
| Inventory | 3 | @smoke, @regression |
| Checkout | 3 | @regression |
| **Total** | **17** | |

All scenarios run against the public [SauceDemo](https://www.saucedemo.com) site in headless Chrome. Undefined or pending steps fail the run (`specflow.json`), so a scenario can't silently pass as skipped.

---

## Quick Start

```bash
git clone https://github.com/mustafaautomation/specflow-bdd-framework.git
cd specflow-bdd-framework

dotnet restore
dotnet build
dotnet test

# Run smoke scenarios only
dotnet test --filter "Category=smoke"

# Watch the browser (headless by default)
HEADLESS=false dotnet test

# Generate living documentation
dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
livingdoc test-assembly bin/Debug/net8.0/SpecFlowBDD.dll -t bin/Debug/net8.0/TestExecution.json
```

---

## Feature File Example

```gherkin
@login
Feature: Login

    Background:
        Given I am on the login page

    @smoke
    Scenario: Successful login with standard user
        When I login with "standard_user" and "secret_sauce"
        Then I should be on the inventory page

    @regression
    Scenario Outline: Login fails with invalid credentials
        When I login with "<username>" and "<password>"
        Then I should see the error "do not match"

        Examples:
            | username  | password |
            | bad_user  | bad_pass |
            | invalid   | wrong    |
```

---

## Architecture

```
┌───────────────────���─────────────────────┐
│        Feature Files (Gherkin)           │
│  Login │ Cart │ Inventory │ Checkout     │
├─────────────────────────────────────────┤
│        Step Definitions                  │
│  LoginSteps │ InventorySteps │ ...       │
├─────────────────────────────────────────┤
│        Page Objects                      │
│  LoginPage │ InventoryPage │ CartPage    │
│  CheckoutPage                            │
├─────────────────────────────────────────┤
│        Hooks + Support                   │
│  WebDriverHooks │ DriverManager │ Config │
├─────────────────────────────────────────┤
│  SpecFlow │ Selenium │ NUnit │ FluentAs  │
└─────────────────────────────────────────┘
```

---

## Key Patterns

### ScenarioContext Dependency Injection
SpecFlow injects `ScenarioContext` into steps and hooks. WebDriver is shared via context:

```csharp
[BeforeScenario]
public void BeforeScenario()
{
    var driver = DriverManager.CreateDriver();
    _scenarioContext.Set(driver, "WebDriver");
}
```

### Step Definitions with Regex Bindings
```csharp
[When(@"I login with ""(.*)"" and ""(.*)""")]
public void WhenILoginWith(string username, string password)
{
    _loginPage.Login(username, password);
}
```

### Living Documentation
SpecFlow.Plus.LivingDoc generates browsable HTML from feature files + test results.

---

## Project Structure

```
specflow-bdd-framework/
├── Features/
│   ├── Login.feature          # 6 test cases (incl. outline)
│   ├── Cart.feature           # 5 scenarios
│   ├── Inventory.feature      # 3 scenarios
│   └── Checkout.feature       # 3 scenarios
├── StepDefinitions/
│   ├── LoginSteps.cs
│   ├── InventorySteps.cs
│   ├── CartSteps.cs
│   └── CheckoutSteps.cs
├── Pages/
│   ├── LoginPage.cs
│   ├── InventoryPage.cs
│   ├── CartPage.cs
│   └── CheckoutPage.cs
├── Hooks/
│   └── WebDriverHooks.cs      # Before/After scenario driver lifecycle
├── Support/
│   ├── TestConfig.cs           # Env-based config
│   └── DriverManager.cs        # Chrome WebDriver factory
├── specflow.json               # Fail on undefined/pending steps
├── SpecFlowBDD.csproj
└── .github/workflows/bdd-tests.yml
```

---

## Stack

| Tool | Purpose |
|------|---------|
| SpecFlow 3.9 | BDD framework with Gherkin parser |
| Selenium 4.27 | Browser automation |
| NUnit 4.3 | Test runner |
| FluentAssertions 7.0 | Readable assertions |
| Selenium Manager | Resolves a ChromeDriver matching the installed Chrome |
| .NET 8 | Runtime |
| LivingDoc | Generated documentation from features |

---

## License

MIT

---

Built by [Quvantic](https://quvantic.com)
