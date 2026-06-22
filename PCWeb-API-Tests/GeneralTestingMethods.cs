public static class GeneralTestingMethods
{
    public static string random_string(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string random_email()
    {
        return random_string(10) + "@example.com";
    }

    public static string random_phone()
    {
        Random random = new Random();
        return new string(Enumerable.Repeat("0123456789", 10)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string random_country()
    {
        string[] countries = {"UK", "Germany", "France", "Netherlands", "Spain", "Italy", "Belgium", "Sweden", "Norway", "Denmark"};
        Random random = new Random();
        return countries[random.Next(countries.Length)];
    }

    public static string random_name()
    {
        string[] names = {"John", "Jane", "Alex", "Emily", "Michael", "Sarah", "David", "Laura", "Chris", "Anna", "James", 
                          "Olivia", "Robert", "Sophia", "Daniel", "Isabella", "William", "Mia", "Joseph", "Charlotte",
                          "Charles", "Amelia", "Thomas", "Evelyn", "Matthew", "Abigail", "Anthony", "Harper", "Mark", "Emily",
                          "Paul", "Elizabeth", "Steven", "Sofia", "Andrew", "Avery", "Kevin", "Ella", "Brian", "Madison"};
        Random random = new Random();
        return names[random.Next(names.Length)];
    }
    public static UserRole random_userRole()
    {
        Random random = new Random();
        return new UserRole(
            null,
            "Role_" + random_string(6),
            "Description for " + random_string(8),
            random.Next(0, 2),    // GlobalReadWriteUser
            random.Next(0, 2),    // GlobalReadWriteAddress
            random.Next(0, 2),    // GlobalReadWriteProduct
            random.Next(0, 2),    // GlobalReadWriteCategory
            random.Next(0, 2),    // GlobalReadWriteRole
            random.Next(0, 2),    // ReadWriteUser
            random.Next(0, 2),    // ReadWriteAddress
            DateTime.Now,
            null,
            1,                    // CreateUserID
            null                  // UpdateUserID
        );
    }}