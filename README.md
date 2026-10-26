# Reqnroll BDD Framework (formerly SpecFlow)

[![BDD Tests](https://github.com/mustafaautomation/specflow-bdd-framework/actions/workflows/bdd-tests.yml/badge.svg)](https://github.com/mustafaautomation/specflow-bdd-framework/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10-512BD4.svg?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com)
[![Reqnroll](https://img.shields.io/badge/Reqnroll-3.3-6C3483.svg)](https://reqnroll.net)

BDD test automation framework using [Reqnroll](https://reqnroll.net) (Gherkin) and Selenium WebDriver on .NET 10, with Page Objects, scenario outlines, HTML reports and CI.

This repo started on SpecFlow. SpecFlow reached end of life on 31 December 2024, so it was migrated to Reqnroll, its open-source successor. The feature files and step bindings did not change; the namespace moved from `TechTalk.SpecFlow` to `Reqnroll`, `specflow.json` became `reqnroll.json`, and SpecFlow+ LivingDoc (discontinued with SpecFlow) was replaced by Reqnroll's built-in HTML formatter. The repository keeps its original name so existing links work.

---

## Scenarios

| Feature | Test cases | Tags |
|---------|-----------|------|
| Login | 6 (incl. Scenario Outline with 2 examples) | @smoke, @regression |
| Cart | 5 | @smoke, @regression |
| Inventory | 3 | @smoke, @regression |
| Checkout | 3 | @regression |
| **Total** | **17** | |

All scenarios run against the public [SauceDemo](https://www.saucedemo.com) site in headless Chrome. Undefined or pending steps fail the run (`reqnroll.json`), so a scenario can't silently pass as skipped.

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

# Use a specific Chrome/Chromium binary, e.g. Chrome for Testing (default: installed Chrome)
CHROME_BINARY="/path/to/chrome" dotnet test

# HTML and Cucumber Messages reports are written on every run
open bin/Debug/net10.0/reports/reqnroll_report.html
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
│  Reqnroll │ Selenium │ NUnit │ FluentAs  │
└─────────────────────────────────────────┘
```

---

## Key Patterns

### ScenarioContext Dependency Injection
Reqnroll injects `ScenarioContext` into steps and hooks. WebDriver is shared via context:

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

### Reports
`reqnroll.json` turns on Reqnroll's formatters: an HTML report (`reports/reqnroll_report.html`) and a Cucumber Messages file (`reports/reqnroll_report.ndjson`) in the test output folder. CI uploads both.

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
├── reqnroll.json               # Fail on undefined/pending steps; report formatters
├── ReqnrollBDD.csproj
└── .github/workflows/bdd-tests.yml
```

---

## Stack

| Tool | Purpose |
|------|---------|
| Reqnroll 3.3 | BDD framework with Gherkin parser and HTML reports |
| Selenium 4.51 | Browser automation |
| NUnit 5 | Test runner |
| FluentAssertions 7.2 | Readable assertions (kept on 7.x: 8.x needs a commercial licence) |
| Selenium Manager | Resolves a ChromeDriver matching the installed Chrome |
| .NET 10 (LTS) | Runtime |

---

## License

MIT

---

Built by [Quvantic](https://quvantic.com)
