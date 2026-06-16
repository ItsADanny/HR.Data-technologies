using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCWeb_Backend.DTO;

namespace PCWeb_Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] CreateOrderDTO dto)
        {
            try
            {
                var order = new Order
                {
                    UserID = dto.userId,
                    ShippingAddressID = dto.shippingAddressId,
                    BillingAddressID = dto.billingAddressId,
                    OrderStatus = "Pending"
                };
                if (order == null)
                    return BadRequest(new { message = "Order data is required" });

                string? validationError = ValidateOrder(order);
                if (validationError != null)
                    return BadRequest(new { message = validationError });

                var result = DBHandler.Create(order);

                if (result == null)
                    return StatusCode(500, new { message = "Error creating order in database" });

                var cartItems = dto.cartItems.Select(item => new CartItems(
                    0,
                    item.id,
                    item.name,
                    0,
                    item.price,
                    item.quantity
                )).ToList();

                string orderLineSQL = order.InsertOrderLineSQL(cartItems);
                string productStockSQL = order.UpdateProductStockSQL(cartItems);

                var productIds = cartItems.Select(item => item.ProductID);
                var checkSql = $@"SELECT ID, Stock FROM Products WHERE Stock < 0 AND ID IN ({string.Join(", ", productIds)})";

                string orderTransactionSQL = order.OrderTransactionSQL(orderLineSQL, productStockSQL, checkSql );

                return Ok(new { message = "Order created successfully", address = result });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Internal server error", error = e.Message });
            }
        }

        private static string? ValidateOrder(Order order)
        {
            if (order.UserID <= 0)
                return "A valid user ID is required";

            if (order.ShippingAddressID <= 0)
                return "A valid shipping address ID is required";

            if (order.BillingAddressID <= 0)
                return "A valid billing address ID is required";

            if (string.IsNullOrWhiteSpace(order.OrderStatus))
                return "Order status is required";

            return null;
        }
    }
}

