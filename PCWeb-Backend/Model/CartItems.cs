using System;

public class CartItems : iData
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public int CartID { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }
    public int? CreateUserID { get; set; }
    public int? UpdateUserID { get; set; }

    public CartItems() { }

    public CartItems(int id, int productID, string productName, int cartID, double price, int quantity)
    {
        ID = id;
        ProductID = productID;
        ProductName = productName;
        CartID = cartID;
        Price = price;
        Quantity = quantity;
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

    public string UpdateSQL()
    {
        throw new NotImplementedException();
    }
}