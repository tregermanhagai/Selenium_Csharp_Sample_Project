using OpenQA.Selenium;

namespace SauceDemoTests.Pages;

public class LoginPage
{
    private readonly IWebDriver _driver;
    private const string Url = "https://www.saucedemo.com/";

    private IWebElement UsernameInput => _driver.FindElement(By.Id("user-name"));
    private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
    private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));
    private IWebElement ErrorContainer => _driver.FindElement(By.CssSelector("[data-test='error']"));

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void NavigateTo() => _driver.Navigate().GoToUrl(Url);

    public string Title => _driver.Title;

    public void Login(string username, string password)
    {
        UsernameInput.Clear();
        UsernameInput.SendKeys(username);
        PasswordInput.Clear();
        PasswordInput.SendKeys(password);
        LoginButton.Click();
    }

    public bool HasError()
    {
        try { return ErrorContainer.Displayed; }
        catch (NoSuchElementException) { return false; }
    }

    public string GetErrorText() => ErrorContainer.Text;
}
