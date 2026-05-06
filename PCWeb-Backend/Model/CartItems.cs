public class CartItems : iData
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public int CartID { get; set; }
    public int Quantity { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }
    public int? CreateUserID { get; set; }
    public int? UpdateUserID { get; set; }

    public CartItems(int id, int productID, int cartID, int quantity)
    {
        ID = id;
        ProductID = productID;
        CartID = cartID;
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