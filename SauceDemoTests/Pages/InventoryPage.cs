using OpenQA.Selenium;

namespace SauceDemoTests.Pages;

public class InventoryPage
{
    private readonly IWebDriver _driver;
    private const string ExpectedUrl = "https://www.saucedemo.com/inventory.html";

    private IWebElement PageTitle => _driver.FindElement(By.CssSelector(".title"));
    private IReadOnlyList<IWebElement> Items => _driver.FindElements(By.CssSelector(".inventory_item"));

    public InventoryPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public bool IsLoaded() => _driver.Url == ExpectedUrl;

    public string GetPageTitle() => PageTitle.Text;

    public int GetItemCount() => Items.Count;
}
