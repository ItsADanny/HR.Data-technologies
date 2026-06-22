public class AdminOrderDTO
{
    public int OrderID { get; set; }
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string OrderStatus { get; set; }
    public DateTime CreateDateTime { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; }
    public string BillingAddress { get; set; }
}