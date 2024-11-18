namespace RethinkEd;

[TestFixture]
public class UserManagementTests : BaseTest
{
    [SetUp]
    public async Task Setup()
    {
        await InitializeAsync();
        await homePage.NavigateTo("https://rta-edu-stg-web-03.azurewebsites.net/core");
        await loginPage.Login();
        await AssertionHelper.AssertToHaveText(homePage.h1Welcome, "Welcome");
        await homePage.NavigateToManageTeamMember();
        await page.WaitForURLAsync("https://rta-edu-stg-web-03.azurewebsites.net/core/setup/team-members");
    }

    [Test]
    public async Task TestCreateUser()
    {
        await manageTeamMemberPage.ClickOnAddTeamMember();
        await AssertionHelper.AssertElementIsVisibleAsync(page, manageTeamMemberPage.uploadPhotoButton);
        string fullName = await manageTeamMemberPage.CreateUser();
        await AssertionHelper.AssertElementIsVisibleAsync(page, manageTeamMemberPage.searchTeamMembersInput);
        await manageTeamMemberPage.SearchUser(fullName);
        await AssertionHelper.AssertToHaveText(manageTeamMemberPage.searchResultTableData.Nth(0), fullName);
    }

    [Test]
    public async Task TestEditUser()
    {
        string firstName = "Balistreri, Gregorio";
        await manageTeamMemberPage.SearchUser(firstName);
        await AssertionHelper.AssertToHaveText(manageTeamMemberPage.searchResultTableData.Nth(0), firstName);
        string email = await manageTeamMemberPage.EditUserEmail();
        await manageTeamMemberPage.SearchUser(firstName);
        await AssertionHelper.AssertToHaveText(manageTeamMemberPage.searchResultTableData.Nth(2), email);
    }

    [Test]
    public async Task TestDeleteUser()
    {
        await manageTeamMemberPage.SearchUser("Balistreri, Lenora");
        await manageTeamMemberPage.DeleteUser();
        await AssertionHelper.AssertElementNotPresentAsync(manageTeamMemberPage.searchResultTableData);
    }

    [TearDown]
    public void Cleanup()
    {
        Dispose();
    }
}
