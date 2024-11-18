
# Playwright Automation Framework

This repository contains an automation testing framework built using Playwright for end-to-end testing of web applications. The framework uses C# and Playwright libraries to interact with the browser and execute tests on web pages, including login, team management, and user interactions.

## Prerequisites

Before running the tests, make sure you have the following installed:

- [.NET SDK](https://download.visualstudio.microsoft.com/download/pr/35b0fb29-cadc-4083-aa26-6cecd2e7ffa1/1a9972a435b73ffdd0b462f979ea5b23/dotnet-sdk-8.0.403-osx-arm64.pkg) (version 8.0.403)
- [Playwright](https://playwright.dev/dotnet/docs/intro)
- [VS Code](https://code.visualstudio.com/docs/csharp/get-started) (Getting Started with C# in VS Code)
- [Powershell](https://learn.microsoft.com/en-gb/powershell/scripting/install/installing-powershell-on-macos?view=powershell-7.4)

## Setup

1. Clone the repository:

   ```bash
   git clone <repo_url>
   cd <Playwright-Automation>
   ```

2. Install the necessary dependencies:

   - Run `dotnet build` to build the project.

3. Install Playwright dependencies:

   ```bash
   pwsh bin/Debug/net8.0/playwright.ps1 install
   ```

## Configuration

The framework uses an `appsettings.json` file for configuration. It contains parameters for the environment, URL, timeouts, and login credentials.

### Example `appsettings.json`:

```json
{
    "Environment": "QA",
    "BaseUrl": "https://rta-edu-stg-web-03.azurewebsites.net/core",
    "Timeout": 70000,
    "Headless": false,
    "SlowMo": 500,
    "Credentials": {
        "Username": "YOUR_USERNAME",
        "Password": "YOUR_PASSWORD"
    }
}
```

### Configuration Details:
- `Environment`: Specifies the environment (e.g., QA, Staging).
- `BaseUrl`: The base URL of the application being tested.
- `Timeout`: The timeout duration (in milliseconds) for page loads and actions.
- `Headless`: Defines whether the browser runs in headless mode (`true` or `false`).
- `SlowMo`: Slows down actions by a specified number of milliseconds (helpful for debugging).
- `Credentials`: Contains login credentials for automated login during tests.

## Framework Overview

### Core Structure
The framework is built around Playwright's core functionality and follows the Page Object Model (POM) design pattern. This ensures that the page elements and interactions are abstracted into separate classes, which makes the tests more maintainable and scalable.

1. **BaseTest**: Contains the setup and teardown logic for each test run, including browser initialization and cleanup.
2. **BasePage**: A base class for all page objects that provides common functionality for interacting with web pages.
3. **Page Objects**: Specific classes for pages in the application such as `LoginPage`, `HomePage`, `ManageTeamMemberPage`.
4. **Helpers**:
    - **DataGenerator**: Generates random test data such as usernames, emails, and first/last names.
    - **AssertionHelper**: Provides common assertion methods for validating element visibility and text.

### Key Pages:
- **LoginPage**: Handles login operations with credentials from `appsettings.json`.
- **HomePage**: Contains actions on the home page (e.g., navigating to manage team members).
- **ManageTeamMemberPage**: Provides functionalities to manage team members, including adding, editing, and deleting users.

### AssertionHelper:
Contains static methods for verifying elements' visibility and text, and ensuring elements are not present on the page.

### Data Generation:
The `DataGenerator` class contains methods to generate random first and last names, usernames, emails, which helps in creating users dynamically during test execution.

## Running Tests

1. **Run Tests in VS Code:**
   - Open the solution in VS Code.
   - Build the solution and run the tests using the Test Explorer.

2. **Run Tests from Command Line:**

   You can use the .NET CLI to run tests:

   ```bash
   dotnet test
   ```

3. **Configure the Browser to Run Headless (Optional):**
   In the `appsettings.json` file, set `"Headless": true` if you want to run the tests in headless mode.

## Directory Structure

```plaintext
├── Pages
│   ├── BasePage.cs
│   ├── HomePage.cs
│   ├── LoginPage.cs
│   └── ManageTeamMemberPage.cs
├── Helpers
│   ├── AssertionHelper.cs
│   └── DataGenerator.cs
├── Config
│   ├── ConfigurationManager.cs.
│   ├── appsettings.json
│   ├── BaseTest.cs
│   └── Test.Settings.cs
├── Tests
│   └── UserManagement.cs
└── Playwright-Automation.csproj
└── Playwright-Automation.sln
└── .gitignore

```