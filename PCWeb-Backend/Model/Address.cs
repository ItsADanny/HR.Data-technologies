using System;
using MySql.Data.MySqlClient;

public class Address : iData
{
    public int ID { get; set; }
    public int AddressId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int HouseNumber { get; set; }           
    public string HouseNumberAddition { get; set; } 
    public string PostCode { get; set; }
    public int UserId { get; set; }               

	public Address() { }

	public Address(int addressId, string country, string city, string street, int house_number, string postcode)
	{
		AddressId = addressId;
		Country = country;
		City = city;
		Street = street;
		HouseNumber = house_number;
		PostCode = postcode;
	}

	public string InsertSQL()
    {
        return $@"INSERT INTO Addresses
                    (Country, City, Street, HouseNumber, HouseNumberAdditive, PostCode, UserID, CreateUserID, CreateDateTime)
                  VALUES
                    ('{Country}', '{City}', '{Street}', {HouseNumber}, '{HouseNumberAddition}', '{PostCode}', {UserId}, {UserId}, NOW())";
    }

	public string UpdateSQL()
	{
		return $@"UPDATE Addresses SET Country = '{Country}', City = '{City}', Street = '{Street}', HouseNumber = {HouseNumber}, HouseNumberAdditive = '{HouseNumberAddition}', PostCode = '{PostCode}', UpdateUserID = {UserId}, UpdateDateTime = NOW() WHERE ID = {AddressId}";
	}

	public string DeleteSQL()
	{
		return $@"DELETE FROM Addresses WHERE ID = {AddressId}";
	}

	public string ReadSQL()
	{
		return $@"SELECT * FROM Addresses WHERE ID = {AddressId}";
	}

	public string ReadSQL(int id)
	{
		return $@"SELECT * FROM Addresses WHERE ID = {id}";
	}

	public static string ReadAllSQL()
	{
		return $@"SELECT * FROM Addresses";
	}

    public static List<Address> GetByUserId(int userId)
    {
        // tabel heet "Addresses" niet "Address"
        using var conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
        conn.Open();
        using var cmd = new MySqlCommand($"SELECT * FROM Addresses WHERE UserID = {userId}", conn);
        using var reader = cmd.ExecuteReader();
        var list = new List<Address>();
        while (reader.Read())
        {
            list.Add(ReadAddress(reader));
        }
        return list;
    }

    public static List<Address> GetAll()
    {
        using var conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
        conn.Open();
        using var cmd = new MySqlCommand("SELECT * FROM Addresses", conn);
        using var reader = cmd.ExecuteReader();
        var list = new List<Address>();
        while (reader.Read())
        {
            list.Add(ReadAddress(reader));
        }
        return list;
    }

    public static Address? GetById(int id)
    {
        using var conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
        conn.Open();
        using var cmd = new MySqlCommand($"SELECT * FROM Addresses WHERE ID = {id}", conn);
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;
        return ReadAddress(reader);
    }

    private static Address ReadAddress(MySqlDataReader reader)
    {
        return new Address
        {
            AddressId      = Convert.ToInt32(reader["ID"]),
            Country        = reader["Country"].ToString(),
            City           = reader["City"].ToString(),
            Street         = reader["Street"].ToString(),
            HouseNumber    = Convert.ToInt32(reader["HouseNumber"]),
            HouseNumberAddition = reader["HouseNumberAdditive"].ToString(),
            PostCode       = reader["PostCode"].ToString(),
            UserId         = Convert.ToInt32(reader["UserID"])
        };
    }
}
