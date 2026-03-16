using MySql.Data.MySqlClient;

public static class DBHandler
{
    public static DBConfig DBConfig {private get; set;}
    private static string _mConnectionString() => DBConfig.GetConnectionSTR();

    public static iData? Create(iData data)
    {
        try
        {
            //Create the connection and the MySQL Command objects
            MySqlConnection conn = new MySqlConnection(_mConnectionString());
            MySqlCommand cmd = new MySqlCommand();

            //Open the connection
            conn.Open();
            //Set the command connection as the just opened connection and set the command text as our insert for the data
            cmd.Connection = conn;
            cmd.CommandText = data.InsertSQL();
            //Execute the Non Query, so it will insert our object
            cmd.ExecuteNonQuery();

            //Change the command to the 
            cmd.CommandText = "SELECT last_insert_id() AS id";

            int insertedObjectID = Convert.ToInt32(cmd.ExecuteScalar());
            if (insertedObjectID == 0) return null;

            data.ID = insertedObjectID;
            return data;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public static bool Update(iData data) => BasicNonQueryExecution(data.UpdateSQL());

    public static bool Delete(iData data) => BasicNonQueryExecution(data.DeleteSQL());

    private static bool BasicNonQueryExecution(string sqlCommand)
    {
        try
        {
            MySqlConnection conn = new MySqlConnection(_mConnectionString());
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