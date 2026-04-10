using System.Runtime.CompilerServices;

public class User : iData
{
    public int ID { get; private set; }
    public UserRole Role { get; set; }
    public string? ShippingAddress { get; set; }
    public string? BillingAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } // this needs to be changed later for hashing
    public string? Phone { get; set; }
    public string? Country { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }
    public int? CreateUserID { get; set; }
    public int? UpdateUserID { get; set; }

    int iData.ID { get => ID; set => ID = value; }

    public User(int id, string firstName, string lastName, string email, string password)
    {
        ID = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    // later we can add more constructors for different use cases, but for now this is fine

    public static string ReadAllSQL()
    {
        throw new NotImplementedException();
    }

    public string DeleteSQL()
    {
        throw new NotImplementedException();
    }

    public string InsertSQL()
    {
        // SECURITY: Escape single quotes in user input to prevent SQL Injection attacks
        // Example: If a user enters "O'Brien" as their name and we don't escape it,
        // the SQL would break: INSERT INTO Users ... VALUES ('O'Brien', ...)
        // MySQL would think the quote ends the string, breaking the SQL.
        // By replacing ' with '', we tell MySQL "this is a literal quote, not SQL code"
        // So "O'Brien" becomes "O''Brien" in the database, which is safe.
        // This protects against malicious input like: Robert'); DROP TABLE Users; --
        
        string escapedFirstName = FirstName.Replace("'", "''");
        string escapedLastName = LastName.Replace("'", "''");
        string escapedEmail = Email.Replace("'", "''");
        string escapedPassword = Password.Replace("'", "''");
        return $"INSERT INTO Users (FirstName, LastName, Email, Password, CreateDateTime) VALUES ('{escapedFirstName}', '{escapedLastName}', '{escapedEmail}', '{escapedPassword}', '{CreateDateTime}');";
    }

    public string ReadSQL()
    {
        throw new NotImplementedException();
    }

    public string ReadSQL(int id)
    {
        throw new NotImplementedException();
    }

    public string UpdateSQL()
    {
        throw new NotImplementedException();
    }
}