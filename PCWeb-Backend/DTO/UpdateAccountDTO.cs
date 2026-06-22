public class UpdateAccountDTO
{
    public int? Shipping_Address { get; set; }
    public int? Billing_Address { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }

    public UpdateAddressDTO ShippingAddress { get; set; }
    public UpdateAddressDTO BillingAddress { get; set; }
}