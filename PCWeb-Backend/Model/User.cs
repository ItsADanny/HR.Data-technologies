using System;

public class User
{
	public int ID { get; set; }
	public int Role_ID { get; set; }
	public int? Shipping_Address { get; set; }
	public int? Billing_Address { get; set; }
	public string First_Name { get; set; }
	public string Last_Name { get; set; }	
	public string Email { get; set; }
	public string Password { get; set; }
	public DateTime Registered_At { get; set; }
	public string Phone { get; set; }
	public string Country { get; set; }

	public User(int id, int role_id, int? shipping_address, int? billing_address, string first_name, string last_name, string email, string password, DatetTime resgistered_at, string phone, string country)
	{
		ID = id;
		Role_ID = role_id;
		Shipping_Address = shipping_address;
		Billing_Address = billing_address;
		First_Name = first_name;	
		last_name = last_name;
		Email = email;
		Password = password;
		Registered_At = resgistered_at;
		Phone = phone;
		Country = country;
	}
}
