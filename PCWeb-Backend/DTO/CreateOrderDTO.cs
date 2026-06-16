namespace PCWeb_Backend.DTO
{
    public class CreateOrderDTO
    {
        public int userId { get; set; }
        public int shippingAddressId { get; set; }
        public int billingAddressId { get; set; }
        public List<CartItemDTO> cartItems { get; set; }
    }
}