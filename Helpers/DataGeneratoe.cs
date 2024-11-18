
/*
This file contains the DataGenerator class, which provides methods for generating 
random test data for use in automated tests. The class includes methods for generating 
random first names, last names, email addresses, and usernames, helping to create 
dynamic and unique test data for various scenarios.
*/

public static class DataGenerator
{
    // Method to generate a random first name
    public static string GenerateRandomFirstName()
    {
        var firstNames = new[] { "John", "Jane", "Alice", "Bob", "Charlie", "David", "Eve", "Grace" };
        var random = new Random();
        return firstNames[random.Next(firstNames.Length)];
    }

    // Method to generate a random last name
    public static string GenerateRandomLastName()
    {
        var lastNames = new[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Martinez" };
        var random = new Random();
        return lastNames[random.Next(lastNames.Length)];
    }

    // Method to generate a random email
    public static string GenerateRandomEmail()
    {
        return $"user{Guid.NewGuid().ToString().Substring(0, 8)}@test.com";
    }

    // Method to generate a random username
    public static string GenerateRandomUsername()
    {
        return $"User{Guid.NewGuid().ToString().Substring(0, 8)}";
    }
}