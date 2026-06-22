using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemoTests.Helpers;
using SauceDemoTests.Pages;

namespace SauceDemoTests.Tests;

[TestFixture]
public class ClickProductsTests
{
    private IWebDriver _driver = null!;
    private LoginPage _loginPage = null!;
    private InventoryPage _inventoryPage = null!;
    private ProductDetailPage _productDetailPage = null!;

    [SetUp]
    public void SetUp()
    {
        _driver = DriverFactory.CreateChromeDriver();
        _driver.Manage().Window.Maximize();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        _loginPage = new LoginPage(_driver);
        _inventoryPage = new InventoryPage(_driver);
        _productDetailPage = new ProductDetailPage(_driver);

        _loginPage.NavigateTo();
        _loginPage.Login("standard_user", "secret_sauce");
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Quit();
    }

    [TestCase("Sauce Labs Backpack")]
    [TestCase("Sauce Labs Bike Light")]
    [TestCase("Sauce Labs Bolt T-Shirt")]
    public void ClickProduct_AddToCartButtonExistsAndBackToProductsWorks(string productName)
    {
        _inventoryPage.ClickProduct(productName);

        Assert.That(_productDetailPage.HasAddToCartButton(), Is.True,
            $"Add to cart button should be visible on '{productName}' detail page");

        _productDetailPage.ClickBackToProducts();

        Assert.That(_inventoryPage.IsLoaded(), Is.True,
            "Should return to inventory page after clicking Back to products");
    }
}
