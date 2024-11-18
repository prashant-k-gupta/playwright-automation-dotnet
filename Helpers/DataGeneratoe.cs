/// <summary>
/// Utility class for generating random test data like names, emails, and usernames.
/// </summary>
public static class DataGenerator
{
    /// <summary>
    /// Generates a random first name.
    /// </summary>
    /// <returns>A randomly selected first name.</returns>
    public static string GenerateRandomFirstName()
    {
        var firstNames = new[] { "John", "Jane", "Alice", "Bob", "Charlie", "David", "Eve", "Grace" };
        var random = new Random();
        return firstNames[random.Next(firstNames.Length)];
    }

    /// <summary>
    /// Generates a random last name.
    /// </summary>
    /// <returns>A randomly selected last name.</returns>
    public static string GenerateRandomLastName()
    {
        var lastNames = new[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Martinez" };
        var random = new Random();
        return lastNames[random.Next(lastNames.Length)];
    }

    /// <summary>
    /// Generates a random email address.
    /// </summary>
    /// <returns>A randomly generated email address.</returns>
    public static string GenerateRandomEmail()
    {
        return $"user{Guid.NewGuid().ToString().Substring(0, 8)}@test.com";
    }

    /// <summary>
    /// Generates a random username.
    /// </summary>
    /// <returns>A randomly generated username.</returns>
    public static string GenerateRandomUsername()
    {
        return $"User{Guid.NewGuid().ToString().Substring(0, 8)}";
    }
}
