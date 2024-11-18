using System.Formats.Asn1;
using Microsoft.Playwright;

/*
This file contains the ManageTeamMemberPage class, which is a page object representing the 
Manage Team Member page of the application. It extends the BasePage class and includes 
locators for elements such as input fields for first name, last name, email, and username, 
as well as buttons for adding, editing, and deleting team members. The class provides methods 
to perform actions on the page, including adding a new team member, searching for users, 
editing user details, and deleting users.
*/

public class ManageTeamMemberPage : BasePage
{
    // Locator for the "Add Team Member" button
    private readonly ILocator addTeamButton;

    // Locator for the "Upload Photo" button
    public readonly ILocator uploadPhotoButton;

    // Locators for input fields
    private readonly ILocator firstNameInput;
    private readonly ILocator lastnameInput;
    private readonly ILocator emailInput;
    private readonly ILocator usernameInput;
    private readonly ILocator passwordInput;
    private readonly ILocator confirmPasswordInput;

    // Locator for the "Save and Close" button
    private readonly ILocator saveAndClosedButton;

    // Locators for search functionality
    public readonly ILocator searchTeamMembersInput;
    public readonly ILocator searchResultTableData;

    // Locators for edit and delete actions
    private readonly ILocator editButton;
    private readonly ILocator deleteButton;

    // Constructor to initialize the ManageTeamMemberPage with the page instance and locators
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
        editButton = page.GetByRole(AriaRole.Link, new () { NameString = "Edit" });
        deleteButton = page.Locator("//button[contains(text(),'Delete')]");
    }

    // Method to click on the "Add Team Member" button and wait for the page to finish loading
    public async Task ClickOnAddTeamMember()
    {
        await addTeamButton.ClickAsync();
        await FinishedLoadingAsync();
    }

    // Method to create a new user with random data and return the full name of the created user
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

    // Method to search for a user by their full name
    public async Task SearchUser(string fullName)
    {
        await searchTeamMembersInput.FillAsync(fullName);
        await searchTeamMembersInput.PressAsync("Enter");
        await FinishedLoadingAsync();
    }

    // Method to edit a user's email and return the new email
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

    // Method to delete a user
    public async Task DeleteUser()
    {
        await editButton.ClickAsync();
        await FinishedLoadingAsync();
        await deleteButton.ClickAsync();
        await deleteButton.Nth(1).ClickAsync();
    }
}
