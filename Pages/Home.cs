using Microsoft.Playwright;

/*
This file contains the HomePage class, which is a page object representing the Home page 
of the application. It extends the BasePage class and includes locators for elements on 
the Home page, such as the welcome message, setup menu, account setup header, and 
manage team member button. Additionally, it includes methods for interacting with 
elements on the page, such as navigating to the Manage Team Member section.
*/

public class HomePage : BasePage
{
    // Locator for the welcome message on the Home page
    public readonly ILocator h1Welcome;

    // Locator for the setup menu link
    private readonly ILocator setupMenu;

    // Locator for the account setup header
    public readonly ILocator accountSetupHeader;

    // Locator for the manage team member button
    private readonly ILocator manageTeamMemberButton;

    // Constructor to initialize the HomePage with the page instance and locators
    public HomePage(IPage page) : base(page)
    {
        h1Welcome = page.Locator("//h2[contains(text(),'Welcome')]");
        setupMenu = page.GetByRole(AriaRole.Link, new () { NameString = "Setup" });
        accountSetupHeader = page.GetByRole(AriaRole.Heading, new () { NameString = "Account Setup" });
        manageTeamMemberButton = page.GetByRole(AriaRole.Link, new () { NameString = "Manage Team Members" });
    }

    // Method to navigate to the Manage Team Member page by clicking relevant menu items
    public async Task NavigateToManageTeamMember()
    {
        await setupMenu.ClickAsync();
        await manageTeamMemberButton.ClickAsync();
    }
}
