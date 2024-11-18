using Microsoft.Playwright;

/// <summary>
/// Helper class for assertions in Playwright tests.
/// </summary>
public static class AssertionHelper
{
    /// <summary>
    /// Asserts that the given element is visible on the page.
    /// </summary>
    /// <param name="page">The Playwright page instance.</param>
    /// <param name="element">The locator for the element to check visibility.</param>
    public static async Task AssertElementIsVisibleAsync(IPage page, ILocator element)
    {
        await element.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        bool isVisible = await element.IsVisibleAsync();
        Assert.IsTrue(isVisible);
    }

    /// <summary>
    /// Asserts that the given element contains the expected text.
    /// </summary>
    /// <param name="element">The locator for the element.</param>
    /// <param name="expectedSubstring">The expected substring to verify in the element's text.</param>
    public static async Task AssertToHaveText(ILocator element, string expectedSubstring)
    {
        await element.WaitForAsync();
        string? actualText = await element.TextContentAsync();
        StringAssert.Contains(expectedSubstring, actualText);
    }

    /// <summary>
    /// Asserts that the given element is not present in the DOM.
    /// </summary>
    /// <param name="element">The locator for the element to check.</param>
    public static async Task AssertElementNotPresentAsync(ILocator element)
    {
        int elemCount = await element.CountAsync();
        Assert.That(elemCount, Is.EqualTo(0));
    }
}
