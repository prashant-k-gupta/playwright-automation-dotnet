using Microsoft.Playwright;

/*
This file contains the BaseTest class which provides the foundation for Playwright-based tests.
It initializes Playwright, launches a browser instance, and sets up default page objects 
(e.g., login, home, manage team member pages) for use in tests. The class also implements IDisposable 
to properly clean up resources by closing the browser and disposing of Playwright instances.
*/

public abstract class BaseTest : IDisposable
{
    // Fields for Playwright instances and page objects
    protected IPlaywright? playwright;
    protected IBrowser? browser;
    protected IPage? page;
    protected LoginPage? loginPage;
    protected HomePage? homePage;
    protected ManageTeamMemberPage? manageTeamMemberPage;

    // Asynchronous initialization method to set up Playwright, browser, and page instances
    public async Task InitializeAsync()
    {
        // Create Playwright instance
        playwright = await Playwright.CreateAsync();
        
        // Launch a new Chromium browser instance with configuration settings
        browser = await playwright.Chromium.LaunchAsync(new()
        {
            Headless = ConfigurationManager.GetHeadless(), // Headless mode based on config
            SlowMo = ConfigurationManager.GetSloMo(),      // Slow motion delay based on config
        });
        
        // Create a new page in the browser and set default timeout
        page = await browser.NewPageAsync();
        page.SetDefaultTimeout(ConfigurationManager.GetTimeout());

        // Initialize page object instances for test pages
        loginPage = new(page);
        homePage = new(page);
        manageTeamMemberPage = new(page);
    }

    // Dispose method to clean up browser and Playwright resources
    public void Dispose()
    {
        // Close the browser and dispose of Playwright instance
        browser?.CloseAsync();
        playwright?.Dispose();
    }
}