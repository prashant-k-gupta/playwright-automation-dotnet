using Microsoft.Playwright;

/// <summary>
/// Represents the Login page of the application. This class provides methods to interact 
/// with elements on the Login page, including entering credentials and submitting the login form.
/// </summary>
public class LoginPage : BasePage
{
    
    private readonly ILocator usernameField;
    private readonly ILocator passwordField;
    private readonly ILocator loginButton;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginPage"/> class with the specified 
    /// Playwright page instance. This constructor also initializes locators for the username, 
    /// password fields, and login button.
    /// </summary>
    /// <param name="page">The Playwright page instance.</param>
    public LoginPage(IPage page) : base(page)
    {
        usernameField = page.Locator("#signInName");
        passwordField = page.Locator("#password");
        loginButton = page.Locator("#next");
    }

    /// <summary>
    /// Performs the login action by filling in the username and password fields with the 
    /// credentials fetched from the configuration and submitting the form.
    /// </summary>
    /// <returns>A task representing the asynchronous login operation.</returns>
    public async Task Login()
    {
        // Wait for the page to load before interacting with elements
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        // Fetch login credentials from the configuration
        var creds = ConfigurationManager.GetLoginCreds();
        string username = creds.Username;
        string password = creds.Password;

        await usernameField.FillAsync(username);
        await passwordField.FocusAsync();
        await passwordField.FillAsync(password);
        await loginButton.ClickAsync();
        await FinishedLoadingAsync();
    }
}
