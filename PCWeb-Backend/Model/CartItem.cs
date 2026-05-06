using System;

public class CartItem
{
	public int ID {  get; set; }
	public int Product_ID { get; set; }
	public int Cart_ID { get; set; }
	public int Quantity { get; set; }
	public double Price { get; set; }

	public CartItem(int id, int product_id, int cart_id, int quantity, double price)
	{
		id = id;
		Product_ID = product_id;
		Cart_ID = cart_id;
		Quantity = quantity;
		Price = price;
	}
}
