using Microsoft.Playwright;

/// <summary>
/// Page object model for the HomePage of the application, with methods to interact with elements on the page.
/// </summary>
public class HomePage : BasePage
{
    public readonly ILocator h1Welcome;
    private readonly ILocator setupMenu;
    public readonly ILocator accountSetupHeader;
    private readonly ILocator manageTeamMemberButton;

    /// <summary>
    /// Initializes a new instance of the HomePage class.
    /// </summary>
    /// <param name="page">The Playwright page instance.</param>
    public HomePage(IPage page) : base(page)
    {
        h1Welcome = page.Locator("//h2[contains(text(),'Welcome')]");
        setupMenu = page.GetByRole(AriaRole.Link, new() { NameString = "Setup" });
        accountSetupHeader = page.GetByRole(AriaRole.Heading, new() { NameString = "Account Setup" });
        manageTeamMemberButton = page.GetByRole(AriaRole.Link, new() { NameString = "Manage Team Members" });
    }

    /// <summary>
    /// Navigates to the Manage Team Member page.
    /// </summary>
    public async Task NavigateToManageTeamMember()
    {
        await setupMenu.ClickAsync();
        await manageTeamMemberButton.ClickAsync();
    }
}
