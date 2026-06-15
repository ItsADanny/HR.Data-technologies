using System;

public class Order
{
	public int ID { get; set; }
	public int? CartID { get; set; }
	public int UserID { get; set; }
	public int ShippingAddressID { get; set; }
	public int BillingAddressID { get; set; }
	public DateTime OrderedAt { get; set; }
	public string OrderStatus { get; set; }

	public Order(int id, int? cart_id, int user_id, int shipping_address, int billing_address, DateTime ordered_at, string order_status)
	{
		ID = id;
		CartID = cart_id;
		UserID = user_id;
		ShippingAddressID = shipping_address;
		BillingAddressID = billing_address;
		OrderedAt = ordered_at;
		OrderStatus = order_status;
	}
}
