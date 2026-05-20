using System;

public class Address
{
	public int AddressId { get; set; }
	public string Country { get; set; }
	public string City { get; set; }
	public string Street { get; set; }
	public string House_Number { get; set; }
	public string Postcode { get; set; }

	public Address(int addressId, string country, string city, string street, string house_number, string postcode)
	{
		AddressId = addressId;
		Country = country;
		City = city;
		Street = street;
		House_Number = house_number;
		Postcode = postcode;
	}
}
