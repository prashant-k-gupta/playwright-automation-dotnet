using Microsoft.Playwright;

/// <summary>
/// Represents the Manage Team Member page of the application. This class provides methods 
/// to interact with elements on the page, including adding, searching, editing, and deleting 
/// team members.
/// </summary>
public class ManageTeamMemberPage : BasePage
{
    // Locator for the "Add Team Member" button
    private readonly ILocator addTeamButton;

    // Locator for the "Upload Photo" button
    public readonly ILocator uploadPhotoButton;

    // Locators for user input fields (First Name, Last Name, etc.)
    private readonly ILocator firstNameInput;
    private readonly ILocator lastnameInput;
    private readonly ILocator emailInput;
    private readonly ILocator usernameInput;
    private readonly ILocator passwordInput;
    private readonly ILocator confirmPasswordInput;

    // Locator for the "Save and Close" button
    private readonly ILocator saveAndClosedButton;

    // Locator for the search input field to search team members
    public readonly ILocator searchTeamMembersInput;

    // Locator for the table data in the search results
    public readonly ILocator searchResultTableData;

    // Locators for the Edit and Delete buttons
    private readonly ILocator editButton;
    private readonly ILocator deleteButton;

    /// <summary>
    /// Initializes a new instance of the <see cref="ManageTeamMemberPage"/> class with the specified 
    /// Playwright page instance. This constructor also initializes locators for the elements on the page 
    /// that interact with team members.
    /// </summary>
    /// <param name="page">The Playwright page instance.</param>
    public ManageTeamMemberPage(IPage page) : base(page)
    {
        addTeamButton = page.GetByRole(AriaRole.Button, new () { NameString = "Add Team Member" });
        uploadPhotoButton = page.GetByText("Photo Upload a photo");
        firstNameInput = page.GetByLabel("First Name Required");
        lastnameInput = page.GetByLabel("Last Name Required");
        emailInput = page.GetByLabel("Email Required");
        usernameInput = page.GetByLabel("Username Required");
        passwordInput = page.GetByRole(AriaRole.Textbox, new () { NameString = "Password Required" });
        confirmPasswordInput = page.GetByLabel("Confirm Password Required");
        saveAndClosedButton = page.GetByRole(AriaRole.Button, new () { NameString = "  Save and Close" });
        searchTeamMembersInput = page.GetByRole(AriaRole.Textbox, new () { NameString = "Search team members" });
        searchResultTableData = page.Locator("td");
        editButton = page.GetByRole(AriaRole.Link, new () { NameString = "Edit" }).Nth(0);
        deleteButton = page.Locator("//button[contains(text(),'Delete')]");
    }

    /// <summary>
    /// Clicks on the "Add Team Member" button to open the form for adding a new team member.
    /// </summary>
    /// <returns>A task representing the asynchronous operation of clicking the button.</returns>
    public async Task ClickOnAddTeamMember()
    {
        await addTeamButton.ClickAsync();
        await FinishedLoadingAsync();
    }

    /// <summary>
    /// Creates a new user by filling in the required fields with randomly generated data 
    /// and submitting the form.
    /// </summary>
    /// <returns>A string representing the full name of the created user.</returns>
    public async Task<string> CreateUser()
    {
        string fName = DataGenerator.GenerateRandomFirstName();
        string lName = DataGenerator.GenerateRandomLastName();
        await firstNameInput.FillAsync(fName);
        await lastnameInput.FillAsync(lName);
        await emailInput.FillAsync(DataGenerator.GenerateRandomEmail());
        await usernameInput.FillAsync(DataGenerator.GenerateRandomUsername());
        await passwordInput.FillAsync(ConfigurationManager.GetLoginCreds().Password);
        await confirmPasswordInput.FillAsync(ConfigurationManager.GetLoginCreds().Password);
        await saveAndClosedButton.ClickAsync();
        await FinishedLoadingAsync();
        return lName + ", " + fName;
    }

    /// <summary>
    /// Searches for a user by their full name in the search input field and submits the search.
    /// </summary>
    /// <param name="fullName">The full name of the user to search for.</param>
    /// <returns>A task representing the asynchronous operation of searching for a user.</returns>
    public async Task SearchUser(string fullName)
    {
        await searchTeamMembersInput.FillAsync(fullName);
        await searchTeamMembersInput.PressAsync("Enter");
        await FinishedLoadingAsync();
    }

    /// <summary>
    /// Edits the email address of an existing user by first clicking the edit button, 
    /// filling in a new email, and saving the changes.
    /// </summary>
    /// <returns>A string representing the new email address of the user.</returns>
    public async Task<string> EditUserEmail()
    {
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        string email = DataGenerator.GenerateRandomEmail();
        await editButton.ClickAsync();
        await emailInput.FillAsync(email);
        await saveAndClosedButton.ClickAsync();
        await FinishedLoadingAsync();
        return email;
    }

    /// <summary>
    /// Deletes an existing user by first clicking the edit button and then clicking the delete button.
    /// </summary>
    /// <returns>A task representing the asynchronous operation of deleting a user.</returns>
    public async Task DeleteUser()
    {
        await editButton.ClickAsync();
        await FinishedLoadingAsync();
        await deleteButton.ClickAsync();
        await deleteButton.Nth(1).ClickAsync();
    }
}