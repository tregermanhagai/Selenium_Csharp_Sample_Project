using OpenQA.Selenium;
using SauceDemoTests.Helpers;

namespace SauceDemoTests.Pages;

public class InventoryPage
{
    private readonly IWebDriver _driver;
    private string ExpectedUrl => $"{Config.BaseUrl}/inventory.html";

    private IWebElement PageTitle => _driver.FindElement(By.CssSelector(".title"));
    private IReadOnlyList<IWebElement> Items => _driver.FindElements(By.CssSelector(".inventory_item"));

    public InventoryPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public bool IsLoaded() => _driver.Url == ExpectedUrl;

    public string GetPageTitle() => PageTitle.Text;

    public int GetItemCount() => Items.Count;

    public void ClickProduct(string productName) =>
        _driver.FindElement(By.LinkText(productName)).Click();
}
