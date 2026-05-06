using System;

public class Cart
{
	public int ID { get; set; }
	public int? User_ID {  get; set; }
	public double Total_Price { get; set; }

	public Cart(int id, int? user_id, double total_price)
	{
		ID = id;
		User_ID = user_id;
		Total_Price = total_price;
	}
}
