public class UserSession : iData
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public string SessionToken { get; set; }
    public DateTime Expiration { get; set; }

    public string InsertSQL()
    {
        return "NOT IMPLEMENTED BECAUSE THIS OBJECT IS NOT MEANT TO BE INSERTED INTO A SQL DATABASE";
    }

    public string UpdateSQL()
    {
        return "NOT IMPLEMENTED BECAUSE THIS OBJECT IS NOT MEANT TO BE UPDATED IN A SQL DATABASE";
    }

    public string DeleteSQL()
    {
        return "NOT IMPLEMENTED BECAUSE THIS OBJECT IS NOT MEANT TO BE DELETED FROM A SQL DATABASE";
    }

    public string ReadSQL()
    {
        throw new NotImplementedException();
    }

    public string ReadSQL(int id)
    {
        throw new NotImplementedException();
    }

    public static string ReadAllSQL()
    {
        throw new NotImplementedException();
    }

    public static UserSession? GetUserSessionByToken(string token)
    {
        MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
        MySqlCommand cmd = new MySqlCommand();

        conn.Open();
        string query = $"SELECT * FROM UserSession WHERE SessionToken = \"{token}\"";
        cmd.Connection = conn;
        cmd.CommandText = query;

        MySqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            UserSession newUserSession = new()
            {
                ID = Convert.ToInt32(reader["ID"].ToString()),
                UserID = Convert.ToInt32(reader["UserID"].ToString()),
                SessionToken = reader["SessionToken"].ToString() ?? "",
                Expiration = GeneralMethods.ParseDBDateTime(reader["Expiration"].ToString() ?? "")
            };

            conn.Close();
            return newUserSession;
        }

        conn.Close();
        return null;
    }

    public static List<UserSession> GetAllUserSessions()
    {
        List<UserSession> returnValue = new List<UserSession>();

        MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
        MySqlCommand cmd = new MySqlCommand();

        conn.Open();
        string query = $"SELECT * FROM UserSession";
        cmd.Connection = conn;
        cmd.CommandText = query;

        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            UserSession newUserSession = new()
            {
                ID = Convert.ToInt32(reader["ID"].ToString()),
                UserID = Convert.ToInt32(reader["UserID"].ToString()),
                SessionToken = reader["SessionToken"].ToString() ?? "",
                Expiration = GeneralMethods.ParseDBDateTime(reader["Expiration"].ToString() ?? "")
            };
            returnValue.Add(newUserSession);
        }     
        return returnValue;
    }
}