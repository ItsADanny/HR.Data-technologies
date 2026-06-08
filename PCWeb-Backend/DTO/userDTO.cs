public record UserFromPanelDTO(int RoleID, int ShippingAddress, int BillingAddress, string FirstName, string LastName, string Email, string Password, string Phone, string Country, int CreateUserID);
public record UserDTO(string FirstName, string LastName, string Email, string Password, string Phone, string Country);
public record UserLoginDTO(string Email, string Password);
public record UserListDTO(int id, string first_Name, string last_Name, string email, string role, string phone, string country);