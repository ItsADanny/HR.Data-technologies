using MySql.Data.MySqlClient;
using StackExchange.Redis;

public static class DBHandler
{
    public static DBConfig DBConfig_MySQL {get; set;}
    public static DBConfig DBConfig_REDIS {get; set;}

    private static string _myConnectionString() => DBConfig_MySQL.GetConnectionSTR();
    private static ConfigurationOptions _reConnectionConfig() => DBConfig_REDIS.GetRedisConfig();

    public static iData? Create(iData data)
    {
        try
        {
            //Create the connection and the MySQL Command objects
            MySqlConnection conn = new MySqlConnection(_myConnectionString());
            MySqlCommand cmd = new MySqlCommand();

            //Open the connection
            conn.Open();
            //Set the command connection as the just opened connection and set the command text as our insert for the data
            cmd.Connection = conn;
            cmd.CommandText = data.InsertSQL();

            // logging the SQL command for debugging purposes
            Console.WriteLine("Executing SQL Command:");
            Console.WriteLine(cmd.CommandText);
            
            //Execute the Non Query, so it will insert our object
            cmd.ExecuteNonQuery();

            //Change the command to the 
            cmd.CommandText = "SELECT last_insert_id() AS id";

            int insertedObjectID = Convert.ToInt32(cmd.ExecuteScalar());

            conn.Close();

            if (insertedObjectID == 0) return null;

            data.ID = insertedObjectID;
            return data;
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public static bool Create(UserSession session)
    {
        try
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_reConnectionConfig());
            IDatabase db = redis.GetDatabase();

            return db.StringSet(session.SessionToken, session.UserID, session.Expiration - DateTime.Now);
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public static bool Update(iData data) => BasicNonQueryExecution(data.UpdateSQL());

    public static bool Delete(iData data) => BasicNonQueryExecution(data.DeleteSQL());

    public static bool Delete(UserSession session)
    {
        try
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_reConnectionConfig());
            IDatabase db = redis.GetDatabase();

            return db.KeyDelete(session.SessionToken);
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private static bool BasicNonQueryExecution(string sqlCommand)
    {
        try
        {
            MySqlConnection conn = new MySqlConnection(_myConnectionString());
            MySqlCommand cmd = new MySqlCommand();

            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = sqlCommand;
            cmd.ExecuteNonQuery();

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}

//ItsDanny Note:
//IMPLEMENTED HERE BECAUSE VSCode WAS BUGGING OUT AND NOT RECOGNIZING THE FILE,
//SO I MOVED IT HERE TO TEST IF IT WAS A PROBLEM WITH THE FILE OR WITH THE CODE
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
}