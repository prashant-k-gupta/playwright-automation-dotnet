using Microsoft.Playwright;

/*
This file contains the LoginPage class, which is a page object representing the Login page 
of the application. It extends the BasePage class and includes locators for elements on 
the Login page, such as the username field, password field, and login button. The class 
also includes a method for performing the login action by filling in credentials and 
submitting the form, ensuring that the page finishes loading after login.
*/

public class LoginPage : BasePage
{
    // Locator for the username field
    private readonly ILocator usernameField;

    // Locator for the password field
    private readonly ILocator passwordField;

    // Locator for the login button
    private readonly ILocator loginButton;

    // Constructor to initialize the LoginPage with the page instance and locators
    public LoginPage(IPage page) : base(page)
    {
        usernameField = page.Locator("#signInName");
        passwordField = page.Locator("#password");
        loginButton = page.Locator("#next");
    }

    // Method to perform the login action using credentials from the configuration
    public async Task Login()
    {
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        // Fetch login credentials from the configuration
        var creds = ConfigurationManager.GetLoginCreds();
        string username = creds.Username;
        string password = creds.Password;

        // Fill in the username and password fields
        await usernameField.FillAsync(username);
        // Focus on the password field in case the password is filled incorrectly due to app slowness
        await passwordField.FocusAsync();  
        await passwordField.FillAsync(password);
        await loginButton.ClickAsync();

        // Wait for the page to finish loading after login
        await FinishedLoadingAsync();
    }
}