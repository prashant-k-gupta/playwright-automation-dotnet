using Microsoft.Playwright;

/*
This file contains the AssertionHelper class, which provides helper methods for performing 
common assertions in Playwright-based tests. These assertions include checking if an element is 
visible, verifying if an element contains a specific text, and confirming if an element is absent 
from the page. The methods use Playwright’s locator functionality to interact with elements on the page 
and assert expected conditions.
*/

public static class AssertionHelper
{
    // Timeout value for waiting for elements in milliseconds
    const int timeoutValue = 10000;

    // Asserts that an element is visible on the page within the specified timeout
    public static async Task AssertElementIsVisibleAsync(IPage page, ILocator element)
    {
        await element.WaitForAsync(new() { Timeout = timeoutValue, State = WaitForSelectorState.Visible });
        bool IsVisible = await element.IsVisibleAsync();
        Assert.IsTrue(IsVisible);
    }

    // Asserts that an element contains the expected text
    public static async Task AssertToHaveText(ILocator element, string expectedSubstring)
    {
        await element.WaitForAsync(new() { Timeout = timeoutValue });
        string? actualText = await element.TextContentAsync();
        StringAssert.Contains(expectedSubstring, actualText);
    }

    // Asserts that an element is not present on the page (count of the element is 0)
    public static async Task AssertElementNotPresentAsync(ILocator element)
    {
        int elemCount = await element.CountAsync();
        Assert.That(elemCount, Is.EqualTo(0));
    }
}
