
/*
This file contains the TestSettings and Credentials classes which represent 
the structure of the configuration settings used in the application. 
TestSettings contains key properties such as environment details, 
base URL, timeout values, and login credentials, while the Credentials 
class holds the username and password for authentication. 
*/

public class TestSettings
{
    // Represents the environment (e.g., development, staging, production)
    public string Environment { get; set; }

    // Base URL for the application
    public string BaseUrl { get; set; }

    // Timeout value for operations in milliseconds
    public int Timeout { get; set; }

    // Indicates whether the browser should run in headless mode
    public bool Headless { get; set; }

    // Slow-motion delay for browser actions, used in debugging scenarios
    public int SlowMo { get; set; }

    // Stores login credentials (username and password)
    public Credentials Credentials { get; set; }
}

public class Credentials
{
    // Username for login authentication
    public string Username { get; set; }

    // Password for login authentication
    public string Password { get; set; }
}
