using Microsoft.Playwright;
using System;

/// <summary>
/// Base class for tests that handles Playwright initialization, browser setup, 
/// and page navigation.
/// </summary>
public abstract class BaseTest : IDisposable
{
    protected IPlaywright playwright;
    protected IBrowser browser;
    protected IPage page;
    protected LoginPage loginPage;
    protected HomePage homePage;
    protected ManageTeamMemberPage manageTeamMemberPage;

    /// <summary>
    /// Initializes Playwright, opens a Chromium browser instance, and sets up page objects.
    /// </summary>
    public async Task InitializeAsync()
    {
        playwright = await Playwright.CreateAsync();
        browser = await playwright.Chromium.LaunchAsync(new()
        {
            Headless = ConfigurationManager.GetHeadless(),
            SlowMo = ConfigurationManager.GetSloMo(),
        });
        page = await browser.NewPageAsync();
        page.SetDefaultTimeout(ConfigurationManager.GetTimeout());
        
        // Initialize page objects
        loginPage = new(page);
        homePage = new(page);
        manageTeamMemberPage = new(page);
    }

    /// <summary>
    /// Disposes the Playwright browser and resources.
    /// </summary>
    public void Dispose()
    {
        browser.CloseAsync();
        playwright.Dispose();
    }
}
