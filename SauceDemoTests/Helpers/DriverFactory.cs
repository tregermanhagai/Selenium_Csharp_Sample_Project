using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoTests.Helpers;

public static class DriverFactory
{
    public static IWebDriver CreateChromeDriver(bool headless = false)
    {
        var options = new ChromeOptions();
        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
        }
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        return new ChromeDriver(options);
    }
}
