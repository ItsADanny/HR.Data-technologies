using System;

public class Order
{
	public int ID { get; set; }
	public int Cart_ID { get; set; }
	public int? User_ID { get; set; }
	public int Shipping_Address { get; set; }
	public int Billing_Address { get; set; }
	public string Order_Email { get; set; }
	public string First_Name { get; set; }
	public string Last_Name { get; set; }
	public string Order_Phone { get; set; }
	public DateTime Ordered_At { get; set; }
	public string Order_Status { get; set; }

	public Order(int id, int cart_id, int? user_id, int shipping_address, int billing_address, string order_email, string first_name, string Last_name, string order_phone, DateTime ordered_at, string order_status)
	{
		ID = id;
		Cart_ID = cart_id;
		User_ID = user_id;
		Shipping_Address = shipping_address;
		Billing_Address = billing_address;
		Order_Email = order_email;
		First_Name = first_name;
		Last_name = Last_name; Order_Phone = order_phone;
		Order_Phone = order_phone;
		Ordered_At = ordered_at;
		Order_Status = order_status;
	}
}
