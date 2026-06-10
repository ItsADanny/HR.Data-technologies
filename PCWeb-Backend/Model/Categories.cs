using MySql.Data.MySqlClient;
public class Categories : iData
{
    public int ID { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }
    public int? CreateUserID { get; set; }
    public int? UpdateUserID { get; set; }

    public Categories(int id, string categoryName, string description)
    {
        ID = id;
        CategoryName = categoryName;
        Description = description;
    }

    public static string ReadAllSQL()
    {
        return "SELECT * FROM Categories";
    }

    public string DeleteSQL()
    {
        throw new NotImplementedException();
    }

    public string InsertSQL()
    {
        throw new NotImplementedException();
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

    public static List<Categories> ReadAllCategories()
    {
        List<Categories> categories = new List<Categories>();

        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(ReadAllSQL(), conn))
            {
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Categories(
                            reader.GetInt32("ID"),
                            reader.GetString("CategoryName"),
                            reader.GetString("Description")
                        ));
                    }
                    return categories;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading categories from READALLCATEGORIES: {e.Message}");
            Console.WriteLine(e.ToString());
            return null;
        }
    }
}