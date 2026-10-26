using OpenQA.Selenium;

namespace Quvantic.ReqnrollBDD.Pages;

public class InventoryPage
{
    private readonly IWebDriver _driver;

    private IReadOnlyCollection<IWebElement> Items => _driver.FindElements(By.CssSelector(".inventory_item"));
    private IWebElement CartBadge => _driver.FindElement(By.CssSelector(".shopping_cart_badge"));
    private IWebElement CartLink => _driver.FindElement(By.CssSelector(".shopping_cart_link"));

    public InventoryPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public int GetItemCount() => Items.Count;

    public void AddToCartByName(string productName)
    {
        var buttonId = $"add-to-cart-{productName.ToLower().Replace(" ", "-")}";
        _driver.FindElement(By.CssSelector($"[data-test='{buttonId}']")).Click();
    }

    public void RemoveFromCartByName(string productName)
    {
        var buttonId = $"remove-{productName.ToLower().Replace(" ", "-")}";
        _driver.FindElement(By.CssSelector($"[data-test='{buttonId}']")).Click();
    }

    public string GetCartBadgeCount()
    {
        try { return CartBadge.Text; }
        catch { return "0"; }
    }

    public void OpenCart() => CartLink.Click();
}
