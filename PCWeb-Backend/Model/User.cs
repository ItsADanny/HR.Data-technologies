using System;
using MySql.Data.MySqlClient;

public class Account : iData
{
	public int ID { get; set; }
	public int Role_ID { get; set; }
	public int? Shipping_Address { get; set; }
	public int? Billing_Address { get; set; }
	public string First_Name { get; set; }
	public string Last_Name { get; set; }	
	public string Email { get; set; }
	public string Password { get; set; }
	public string Phone { get; set; }
	public string Country { get; set; }
	public DateTime CreateDateTime { get; set; }
	public DateTime? UpdateDateTime { get; set; }
	public int CreateUserID { get; set; }
	public int UpdateUserID { get; set; }

	public Account(int id, int role_id, int? shipping_address, int? billing_address, string first_name, string last_name, string email, string password, string phone, string country, DateTime createDateTime, DateTime? updateDateTime, int createUserID, int updateUserID)
	{
		ID = id;
		Role_ID = role_id;
		Shipping_Address = shipping_address;
		Billing_Address = billing_address;
		First_Name = first_name;	
		Last_Name = last_name;
		Email = email;
		Password = password;
		Phone = phone;
		Country = country;
		CreateDateTime = createDateTime;
		UpdateDateTime = updateDateTime;
		CreateUserID = createUserID;
		UpdateUserID = updateUserID;
	}

	public Account(int roleID, int shippingAddress, int billingAddress, string firstName, string lastName, string email, string password, string phone, string country, int createUserID)
    {
		Role_ID = roleID;
		Shipping_Address = shippingAddress;
		Billing_Address = billingAddress;
		First_Name = firstName;	
		Last_Name = lastName;
		Email = email;
		Password = password;
		Phone = phone;
		Country = country;
		CreateUserID = createUserID;
    }

	public Account(string firstName, string lastName, string email, string password, string phone, string country)
	{
		Role_ID = 2;
		Shipping_Address = null;
		Billing_Address = null;
		First_Name = firstName;	
		Last_Name = lastName;
		Email = email;
		Password = password;
		Phone = phone;
		Country = country;
		CreateUserID = 2;
	}

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
        return $"INSERT INTO Users (RoleID, PrimaryShippingAddressID, PrimaryBillingAddressID, FirstName, LastName, Email, Password, Phone, Country, CreateUserID) VALUES ({Role_ID}, {Shipping_Address}, {Billing_Address}, '{First_Name}', '{Last_Name}', '{Email}', '', '{Phone}', '{Country}', {CreateUserID})";
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
        return $"UPDATE Users SET RoleID = {Role_ID}, PrimaryShippingAddressID = {Shipping_Address}, PrimaryBillingAddressID = {Billing_Address}, FirstName = '{First_Name}', LastName = '{Last_Name}', Email = '{Email}', Password = '', Phone = '{Phone}', Country = '{Country}', UpdateUserID = {UpdateUserID} WHERE ID = {ID}";
    }

	public static Account? GetByEmail(string email)
	{
		MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
		MySqlCommand cmd = new MySqlCommand();

		conn.Open();
		cmd.Connection = conn;
		cmd.CommandText = $"SELECT * FROM Users WHERE Email = '{email}'";

		MySqlDataReader reader = cmd.ExecuteReader();
		if (reader.Read())
		{
			DateTime CreateDateTime = DateTime.Now;
			DateTime? UpdateDateTime = null;

			string? CreateDateTime_STR = reader["CreateDateTime"].ToString();
			string? UpdateDateTime_STR = reader["UpdateDateTime"].ToString();

			if (CreateDateTime_STR is not null && CreateDateTime_STR != "") CreateDateTime = GeneralMethods.ParseDBDateTime(CreateDateTime_STR);
			if (UpdateDateTime_STR is not null && UpdateDateTime_STR != "") UpdateDateTime = GeneralMethods.ParseDBDateTime(UpdateDateTime_STR);

			Account returnValue = new(
				Convert.ToInt32(reader["ID"].ToString()), 
				Convert.ToInt32(reader["RoleID"].ToString()), 
				reader["PrimaryShippingAddressID"] as int?, 
				reader["PrimaryBillingAddressID"] as int?, 
				reader["FirstName"].ToString(), 
				reader["LastName"].ToString(), 
				reader["Email"].ToString(), 
				reader["Password"].ToString(),
				reader["Phone"].ToString(), 
				reader["Country"].ToString(), 
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
}