using MySql.Data.MySqlClient;
using System;

public class UserRole : iData
{
    public int? ID {get; set;}
    public string Name {get; set;}
    public string Description {get; set;}
    public int GlobalReadWriteUser {get; set;}
    public int GlobalReadWriteAddress {get; set;}
    public int GlobalReadWriteProduct {get; set;}
    public int GlobalReadWriteCategory {get; set;}
    public int GlobalReadWriteRole {get; set;}
    public int ReadWriteUser {get; set;}
    public int ReadWriteAddress {get; set;}
    public DateTime CreateDateTime {get; set;}
    public DateTime? UpdateDateTime {get; set;}
    public int CreateUserID {get; set;}
    public int? UpdateUserID {get; set;}
    int iData.ID { get => throw new NotImplementedException(); set => ID = value; }

    public UserRole(int? id, string name, string description, int globalReadWriteUser, int globalReadWriteAddress, int globalReadWriteProduct, int globalReadWriteCategory, int globalReadWriteRole, int readWriteUser, int readWriteAddress, DateTime createDateTime, DateTime? updateDateTime, int createUserID, int? updateUserID)
    {
        ID = id;
        Name = name;
        Description = description;
        GlobalReadWriteUser = globalReadWriteUser;
        GlobalReadWriteAddress = globalReadWriteAddress;
        GlobalReadWriteProduct = globalReadWriteProduct;
        GlobalReadWriteCategory = globalReadWriteCategory;
        GlobalReadWriteRole = globalReadWriteRole;
        ReadWriteUser = readWriteUser;
        ReadWriteAddress = readWriteAddress;
        CreateDateTime = createDateTime;
        UpdateDateTime = updateDateTime;
        CreateUserID = createUserID;
        UpdateUserID = updateUserID;
    }

    public string InsertSQL()
    {
        throw new NotImplementedException();
    }

    public string UpdateSQL()
    {
        throw new NotImplementedException();
    }

    public string DeleteSQL()
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

    public static string ReadAllSQL()
    {
        return "SELECT * FROM Roles";
    }

    public static UserRole? GetUserRoleByID(int id)
    {
        MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
		MySqlCommand cmd = new MySqlCommand();

		conn.Open();
		cmd.Connection = conn;
		cmd.CommandText = $"SELECT * FROM UserRoles WHERE ID = {id}";

		MySqlDataReader reader = cmd.ExecuteReader();
		if (reader.Read())
		{
			DateTime CreateDateTime = DateTime.Now;
			DateTime? UpdateDateTime = null;

			string? CreateDateTime_STR = reader["CreateDateTime"].ToString();
			string? UpdateDateTime_STR = reader["UpdateDateTime"].ToString();

			if (CreateDateTime_STR is not null && CreateDateTime_STR != "") CreateDateTime = GeneralMethods.ParseDBDateTime(CreateDateTime_STR);
			if (UpdateDateTime_STR is not null && UpdateDateTime_STR != "") UpdateDateTime = GeneralMethods.ParseDBDateTime(UpdateDateTime_STR);

			UserRole returnValue = new(
				Convert.ToInt32(reader["ID"].ToString()), 
				reader["Name"].ToString(), 
				reader["Description"].ToString(), 
				Convert.ToInt32(reader["GlobalReadWriteUser"].ToString()), 
				Convert.ToInt32(reader["GlobalReadWriteAddress"].ToString()), 
				Convert.ToInt32(reader["GlobalReadWriteProduct"].ToString()), 
				Convert.ToInt32(reader["GlobalReadWriteCategory"].ToString()), 
				Convert.ToInt32(reader["GlobalReadWriteRole"].ToString()), 
				Convert.ToInt32(reader["ReadWriteUser"].ToString()), 
				Convert.ToInt32(reader["ReadWriteAddress"].ToString()), 
				CreateDateTime, 
				UpdateDateTime, 
				Convert.ToInt32(reader["CreateUserID"].ToString()), 
				Convert.ToInt32(reader["UpdateUserID"].ToString()));
			conn.Close();

			return returnValue;
		}

		conn.Close();
		return null;
    }

    public static List<UserRole> userRoles()
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(ReadAllSQL(), conn))
            {
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<UserRole> userRoles = new List<UserRole>();

                    while (reader.Read())
                    {
                        userRoles.Add(new UserRole(
                            reader["ID"] is not DBNull ? Convert.ToInt32(reader["ID"]) : null,
                            reader["Name"]?.ToString() ?? string.Empty,
                            reader["Description"]?.ToString() ?? string.Empty,
                            reader["GlobalReadWriteUser"] is not DBNull ? Convert.ToInt32(reader["GlobalReadWriteUser"]) : 0,
                            reader["GlobalReadWriteAddress"] is not DBNull ? Convert.ToInt32(reader["GlobalReadWriteAddress"]) : 0,
                            reader["GlobalReadWriteProduct"] is not DBNull ? Convert.ToInt32(reader["GlobalReadWriteProduct"]) : 0,
                            reader["GlobalReadWriteCategory"] is not DBNull ? Convert.ToInt32(reader["GlobalReadWriteCategory"]) : 0,
                            reader["GlobalReadWriteRole"] is not DBNull ? Convert.ToInt32(reader["GlobalReadWriteRole"]) : 0,
                            reader["ReadWriteUser"] is not DBNull ? Convert.ToInt32(reader["ReadWriteUser"]) : 0,
                            reader["ReadWriteAddress"] is not DBNull ? Convert.ToInt32(reader["ReadWriteAddress"]) : 0,
                            reader["CreateDateTime"] is not DBNull ? Convert.ToDateTime(reader["CreateDateTime"]) : DateTime.MinValue,
                            reader["UpdateDateTime"] is not DBNull ? Convert.ToDateTime(reader["UpdateDateTime"]) : DateTime.MinValue,
                            reader["CreateUserID"] is not DBNull ? Convert.ToInt32(reader["CreateUserID"]) : 0,
                            reader["UpdateUserID"] is not DBNull ? Convert.ToInt32(reader["UpdateUserID"]) : 0
                        ));
                    }

                    return userRoles;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR in ReadAllUserRoles:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public static bool CreateUserRole(UserRole userRole) {
        return DBHandler.Create(userRole) != null;
    }

    public static bool UpdateUserRole(UserRole userRole) {
        return DBHandler.Update(userRole);
    }

    public static bool DeleteUserRole(UserRole userRole) {
        return DBHandler.Delete(userRole);
    }

    public static bool DeleteUserRole(int id) {
        UserRole userRole = GetUserRoleByID(id);
        if (userRole == null) return false;
        return DeleteUserRole(userRole);
    }
}