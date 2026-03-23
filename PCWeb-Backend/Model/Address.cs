using System;

public class Address
{
	public int Address {  get; set; }
	public string Country { get; set; }
	public string City { get; set; }
	public string Street { get; set; }
	public string House_Number { get; set; }
	public string Postcode { get; set; }

	public Address(int address, string country, string city, string street, string house_number, string postcode)
	{
		Address = address;
		Country = country;
		City = city;
		Street = street;
		House_Number = house_number;
		Postcode = postcode;
	}
}
