using OpenQA.Selenium;

namespace SauceDemoTests.Pages;

public class ProductDetailPage
{
    private readonly IWebDriver _driver;

    private IWebElement AddToCartButton => _driver.FindElement(By.CssSelector("button[data-test^='add-to-cart']"));
    private IWebElement BackToProductsButton => _driver.FindElement(By.Id("back-to-products"));

    public ProductDetailPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public bool HasAddToCartButton()
    {
        try { return AddToCartButton.Displayed; }
        catch (NoSuchElementException) { return false; }
    }

    public void ClickBackToProducts() => BackToProductsButton.Click();
}
