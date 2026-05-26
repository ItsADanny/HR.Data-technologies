using MySql.Data.MySqlClient;

public class UserSalt : iData
{
    public int ID {get; set;}
    public int UserID {get; private set;}
    public string Salt {get; set;}
    public DateTime CreateDateTime {get; private set;}
    public DateTime? UpdateDateTime {get; private set;}

    public UserSalt(int userID, string salt)
    {
        UserID = userID;
        Salt = salt;
    }

    public UserSalt(int id, int userID, string salt, DateTime createDateTime, DateTime? updateDateTime=null)
    {
        ID = id;
        UserID = userID;
        Salt = salt;
        CreateDateTime = createDateTime;
        UpdateDateTime = updateDateTime;
    }

    public static UserSalt? GetByID(int id)
    {
        MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
        MySqlCommand cmd = new MySqlCommand();

        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = ReadFromIDSQL(id);

        MySqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            DateTime CreateDateTime = DateTime.Now;
            DateTime? UpdateDateTime = null;

            string? CreateDateTime_STR = reader["CreateDateTime"].ToString();
            string? UpdateDateTime_STR = reader["UpdateDateTime"].ToString();

            if (CreateDateTime_STR is not null && CreateDateTime_STR != "") CreateDateTime = GeneralMethods.ParseDBDateTime(CreateDateTime_STR);
            if (UpdateDateTime_STR is not null && UpdateDateTime_STR != "") UpdateDateTime = GeneralMethods.ParseDBDateTime(UpdateDateTime_STR);

            UserSalt returnValue = new(Convert.ToInt32(reader["ID"].ToString()), Convert.ToInt32(reader["UserID"].ToString()),
                                 reader["Salt"].ToString(), CreateDateTime,
                                 UpdateDateTime);

            conn.Close();
            return returnValue;
        }

        conn.Close();
        return null;
    }

    public static List<UserSalt> GetAll()
    {
        List<UserSalt> returnValue = [];

        MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
        MySqlCommand cmd = new MySqlCommand();

        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = ReadAllSQL();

        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            DateTime CreateDateTime = DateTime.Now;
            DateTime? UpdateDateTime = null;

            string? CreateDateTime_STR = reader["CreateDateTime"].ToString();
            string? UpdateDateTime_STR = reader["UpdateDateTime"].ToString();

            if (CreateDateTime_STR is not null && CreateDateTime_STR != "") CreateDateTime = GeneralMethods.ParseDBDateTime(CreateDateTime_STR);
            if (UpdateDateTime_STR is not null && UpdateDateTime_STR != "") UpdateDateTime = GeneralMethods.ParseDBDateTime(UpdateDateTime_STR);

            UserSalt newUserSalt = new(Convert.ToInt32(reader["ID"].ToString()), Convert.ToInt32(reader["UserID"].ToString()),
                                 reader["Salt"].ToString(), CreateDateTime,
                                 UpdateDateTime);

            returnValue.Add(newUserSalt);
        }

        conn.Close();
        return returnValue;
    }

    public static UserSalt? GetSalt(int userID)
    {
        MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
        MySqlCommand cmd = new MySqlCommand();

        conn.Open();
        string query = $"SELECT * FROM UserSalt WHERE UserID = {userID}";
        cmd.Connection = conn;
        cmd.CommandText = query;

        MySqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            DateTime CreateDateTime = DateTime.Now;
            DateTime? UpdateDateTime = null;

            string? CreateDateTime_STR = reader["CreateDateTime"].ToString();
            string? UpdateDateTime_STR = reader["UpdateDateTime"].ToString();

            if (CreateDateTime_STR is not null && CreateDateTime_STR != "") CreateDateTime = GeneralMethods.ParseDBDateTime(CreateDateTime_STR);
            if (UpdateDateTime_STR is not null && UpdateDateTime_STR != "") UpdateDateTime = GeneralMethods.ParseDBDateTime(UpdateDateTime_STR);

            string? salt = reader["Salt"].ToString();
            if (salt is null) salt = "";

            UserSalt returnValue = new(
                Convert.ToInt32(reader["ID"].ToString()), 
                Convert.ToInt32(reader["UserID"].ToString()), 
                salt, CreateDateTime,
                UpdateDateTime);
            conn.Close();

            return returnValue;
        }

        conn.Close();
        return null;
    }

    public string InsertSQL() => $"INSERT INTO UserSalt (UserID, Salt) VALUES (\"{UserID}\", \"{Salt}\")";

    public string UpdateSQL() => $"UPDATE UserSalt SET UserID = \"{UserID}\", Salt = \"{Salt}\" WHERE ID = {ID}";

    public string DeleteSQL() => $"DELETE FROM UserSalt WHERE ID = {ID}";

    public string ReadSQL() => $"SELECT * FROM UserSalt WHERE ID = {ID}";

    public string ReadSQL(int id) => $"SELECT * FROM UserSalt WHERE ID = {id}";
    public static string ReadFromIDSQL(int id) => $"SELECT * FROM UserSalt WHERE ID = {id}";

    public static string ReadAllSQL() => "SELECT * FROM UserSalt";
}