namespace Quvantic.ReqnrollBDD.Support;

public static class TestConfig
{
    public static string BaseUrl =>
        Environment.GetEnvironmentVariable("BASE_URL") ?? "https://www.saucedemo.com";

    public static string StandardUser =>
        Environment.GetEnvironmentVariable("STANDARD_USER") ?? "standard_user";

    public static string Password =>
        Environment.GetEnvironmentVariable("PASSWORD") ?? "secret_sauce";

    public static bool Headless =>
        bool.TryParse(Environment.GetEnvironmentVariable("HEADLESS"), out var h) ? h : true;

    /// <summary>Optional path to a Chrome/Chromium binary (for example Chrome for Testing).
    /// When unset, Selenium Manager uses the installed Chrome.</summary>
    public static string? ChromeBinary =>
        Environment.GetEnvironmentVariable("CHROME_BINARY") is { Length: > 0 } path ? path : null;

    public static int TimeoutSeconds => 10;
}
