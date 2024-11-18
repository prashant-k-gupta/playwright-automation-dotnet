using Microsoft.Playwright;

/// <summary>
/// Base class for page objects, providing methods for navigation and common actions.
/// </summary>
public abstract class BasePage
{
    protected IPage page;
    protected IElementHandle loading;

    /// <summary>
    /// Initializes a new instance of the BasePage class.
    /// </summary>
    /// <param name="page">The Playwright page instance.</param>
    public BasePage(IPage page)
    {
        this.page = page;
    }

    /// <summary>
    /// Navigates to the specified URL and waits for the page to load.
    /// </summary>
    /// <param name="url">The URL to navigate to.</param>
    public async Task NavigateTo(string url)
    {
        await page.GotoAsync(url);
        await page.WaitForLoadStateAsync();
    }

    /// <summary>
    /// Waits for the page to finish loading, including waiting for any loading elements to disappear.
    /// </summary>
    public async Task FinishedLoadingAsync()
    {
        try
        {
            var loading = await page.WaitForSelectorAsync(".ajax-loader", new() { Timeout = 3000 });
            await loading.WaitForElementStateAsync(ElementState.Hidden);
        }
        catch { };
        await page.WaitForLoadStateAsync();
        await page.WaitForTimeoutAsync(5000);
    }
}
