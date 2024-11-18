using Microsoft.Playwright;

/*
This file contains the BasePage class, which serves as a foundational class for page objects 
in Playwright-based tests. The class includes methods for navigating to a URL, waiting for page 
load, and ensuring that any loading indicators are finished before proceeding with the test steps. 
It provides reusable functionality for common page interactions and waits, reducing redundancy in test code.
*/

public abstract class BasePage
{
    // Page instance and loading element
    protected IPage page;
    protected IElementHandle loading;

    // Constructor to initialize the BasePage with a Playwright page instance
    public BasePage(IPage page)
    {
        this.page = page;
    }

    // Method to navigate to a specified URL and wait for the page to fully load
    public async Task NavigateTo(string url)
    {
        await page.GotoAsync(url);
        await page.WaitForLoadStateAsync();
    }

    // Common method to wait for the page to finish loading by detecting and handling the loading spinner
    public async Task FinishedLoadingAsync()
    {
        // Wait for the element with selector ".ajax-loader" to appear on the page
        var loading = await page.WaitForSelectorAsync(".ajax-loader");

        if (loading != null)
        {
            // Wait for the loading element to be hidden or detached
            await loading.WaitForElementStateAsync(ElementState.Hidden);
        }

        // Wait for the page to reach the load state and allow additional time for complete loading
        await page.WaitForLoadStateAsync();
        await page.WaitForTimeoutAsync(5000);
    }
}
