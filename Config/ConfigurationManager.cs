using System.Text.Json;

/* 
This file contains the ConfigurationManager class responsible for reading
and managing application configuration settings from a JSON file.
It loads the settings at initialization and provides methods to retrieve
specific configuration values like the base URL, timeout, headless mode, 
slow-motion delay, and login credentials for the application. 
*/

public static class ConfigurationManager 
{
    // Property to hold the deserialized settings from the config file
    public static TestSettings Settings { get; private set; }

    // Static constructor to load the configuration settings when the class is first accessed
    static ConfigurationManager()
    {
        // Read the JSON file containing configuration settings
        var json = File.ReadAllText("../../../Config/appsettings.json");
        
        // Deserialize the JSON into a TestSettings object
        Settings = JsonSerializer.Deserialize<TestSettings>(json);
    }

    // Methods to retrieve specific settings from the configuration

    // Get the base URL for the application
    public static string GetBaseUrl() => Settings.BaseUrl;

    // Get the timeout value for operations
    public static int GetTimeout() => Settings.Timeout;

    // Get the headless browser mode setting (true or false)
    public static bool GetHeadless() => Settings.Headless;

    // Get the slow-motion delay for browser actions
    public static int GetSloMo() => Settings.SlowMo;

    // Get the login credentials stored in the settings
    public static Credentials GetLoginCreds() => Settings.Credentials;
}
