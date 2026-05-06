public class Orders : iData
{
    public int ID { get; set; }
    public int CartID { get; set; }
    public int UserID { get; set; }
    public string ShippingAddressID { get; set; }
    public string BillingAddressID { get; set; }
    public string OrderEmail { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string OrderPhone { get; set; }
    public string OrderStatus { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }
    public int? CreateUserID { get; set; }
    public int? UpdateUserID { get; set; }

    public Orders(int id, int userID, string shippingAddressID, string billingAddressID, string orderEmail, string firstName, string lastName, string orderPhone, string orderStatus)
    {
        ID = id;
        UserID = userID;
        ShippingAddressID = shippingAddressID;
        BillingAddressID = billingAddressID;
        OrderEmail = orderEmail;
        FirstName = firstName;
        LastName = lastName;
        OrderPhone = orderPhone;
        OrderStatus = orderStatus;
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