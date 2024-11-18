namespace RethinkEd;

[TestFixture]
public class UserManagementTests : BaseTest
{
    string User = "Barton";
    /// <summary>
    /// Set up the test environment by navigating to the application, logging in, 
    /// and ensuring that the home page and team member management page are properly loaded.
    /// </summary>
    [SetUp]
    public async Task Setup()
    {
        await InitializeAsync();
        await homePage.NavigateTo(ConfigurationManager.GetBaseUrl());
        await loginPage.Login();
        await AssertionHelper.AssertToHaveText(homePage.h1Welcome, "Welcome");
        await homePage.NavigateToManageTeamMember();
        await page.WaitForURLAsync("https://rta-edu-stg-web-03.azurewebsites.net/core/setup/team-members");
    }

    /// <summary>
    /// Tests the creation of a new user by adding a team member, verifying that the 
    /// user is searchable, and confirming the new user's details in the search results.
    /// </summary>
    [Test]
    [Order(1)] // Ensure Create runs first
    public async Task TestCreateUser()
    {
        // Click on the "Add Team Member" button and verify the photo upload button is visible
        await manageTeamMemberPage.ClickOnAddTeamMember();
        await AssertionHelper.AssertElementIsVisibleAsync(page, manageTeamMemberPage.uploadPhotoButton);

        // Create a user and verify the user can be found by searching for their full name
        User = await manageTeamMemberPage.CreateUser();
        await AssertionHelper.AssertElementIsVisibleAsync(page, manageTeamMemberPage.searchTeamMembersInput);
        await manageTeamMemberPage.SearchUser(User);
        await AssertionHelper.AssertToHaveText(manageTeamMemberPage.searchResultTableData.Nth(0), User);
    }

    /// <summary>
    /// Tests editing the user's email address and verifying the update is reflected in the 
    /// search results after editing.
    /// </summary>
    [Test]
    [Order(2)] // Ensure Update runs second
    public async Task TestEditUser()
    {
        // Search for a user by their first name and verify that the user is found
        await manageTeamMemberPage.SearchUser(User);
        await AssertionHelper.AssertToHaveText(manageTeamMemberPage.searchResultTableData.Nth(0), User);

        // Edit the user's email and verify that the new email is reflected in the search results
        string email = await manageTeamMemberPage.EditUserEmail();
        await manageTeamMemberPage.SearchUser(User);
        await AssertionHelper.AssertToHaveText(manageTeamMemberPage.searchResultTableData.Nth(2), email);
    }

    /// <summary>
    /// Tests deleting a user by searching for them, deleting them, and verifying that 
    /// the user no longer appears in the search results.
    /// </summary>
    [Test]
    [Order(3)] // Ensure Delete runs last
    public async Task TestDeleteUser()
    {
        // Search for a user and delete them
        await manageTeamMemberPage.SearchUser(User);
        await manageTeamMemberPage.DeleteUser();

        // Verify that the user no longer appears in the search results
        await AssertionHelper.AssertElementNotPresentAsync(manageTeamMemberPage.searchResultTableData);
    }

    /// <summary>
    /// Cleans up after each test by disposing of any resources.
    /// </summary>
    [TearDown]
    public void Cleanup()
    {
        Dispose();
    }
}
