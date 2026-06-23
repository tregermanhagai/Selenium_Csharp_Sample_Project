using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemoTests.Helpers;
using SauceDemoTests.Pages;

namespace SauceDemoTests.Tests;

[TestFixture]
public class LoginTests
{
    private IWebDriver _driver = null!;
    private LoginPage _loginPage = null!;
    private InventoryPage _inventoryPage = null!;

    [SetUp]
    public void SetUp()
    {
        _driver = DriverFactory.CreateChromeDriver();
        _driver.Manage().Window.Maximize();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        _loginPage = new LoginPage(_driver);
        _inventoryPage = new InventoryPage(_driver);

        _loginPage.NavigateTo();
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Quit();
    }

    [Test]
    [Description("Valid credentials redirect to the inventory page with products listed")]
    public void Login_WithValidCredentials_RedirectsToInventoryPage()
    {
        _loginPage.Login(Config.StandardUser, Config.Password);

        Assert.Multiple(() =>
        {
            Assert.That(_inventoryPage.IsLoaded(), Is.True, "Should land on inventory page");
            Assert.That(_inventoryPage.GetPageTitle(), Is.EqualTo("Products"));
            Assert.That(_inventoryPage.GetItemCount(), Is.GreaterThan(0), "Inventory should contain items");
        });
    }

    [Test]
    [Description("Wrong password shows an error message")]
    public void Login_WithInvalidPassword_ShowsErrorMessage()
    {
        _loginPage.Login(Config.StandardUser, "wrong_password");

        Assert.That(_loginPage.HasError(), Is.True);
        Assert.That(_loginPage.GetErrorText(), Does.Contain("Username and password do not match"));
    }

    [Test]
    [Description("Locked-out user sees a specific lock message")]
    public void Login_WithLockedOutUser_ShowsLockedMessage()
    {
        _loginPage.Login(Config.LockedOutUser, Config.Password);

        Assert.That(_loginPage.HasError(), Is.True);
        Assert.That(_loginPage.GetErrorText(), Does.Contain("locked out"));
    }

    [Test]
    [Description("Submitting empty form prompts for username")]
    public void Login_WithEmptyCredentials_ShowsUsernameRequiredError()
    {
        _loginPage.Login(string.Empty, string.Empty);

        Assert.That(_loginPage.HasError(), Is.True);
        Assert.That(_loginPage.GetErrorText(), Does.Contain("Username is required"));
    }

    [Test]
    [Description("Username filled but password empty prompts for password")]
    public void Login_WithMissingPassword_ShowsPasswordRequiredError()
    {
        _loginPage.Login(Config.StandardUser, string.Empty);

        Assert.That(_loginPage.HasError(), Is.True);
        Assert.That(_loginPage.GetErrorText(), Does.Contain("Password is required"));
    }
}
