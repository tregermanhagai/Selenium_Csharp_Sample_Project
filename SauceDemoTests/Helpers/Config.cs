using DotNetEnv;

namespace SauceDemoTests.Helpers;

public static class Config
{
    static Config()
    {
        Env.Load();
    }

    public static string BaseUrl =>
        Environment.GetEnvironmentVariable("BASE_URL") ?? "https://www.saucedemo.com";

    public static string StandardUser => GetRequired("STANDARD_USER");

    public static string LockedOutUser => GetRequired("LOCKED_OUT_USER");

    public static string Password => GetRequired("PASSWORD");

    private static string GetRequired(string key) =>
        Environment.GetEnvironmentVariable(key)
        ?? throw new InvalidOperationException($"'{key}' is not set. Add it to your .env file.");
}
