using System;
using MySql.Data.MySqlClient;
using System.Globalization;



public class Order : iData
{
    public int ID { get; set; }
    public int? CartID { get; set; }
    public int UserID { get; set; }
    public int ShippingAddressID { get; set; }
    public int BillingAddressID { get; set; }
    public string OrderStatus { get; set; }

    public Order() { }

    public Order(int user_id, int shipping_address, int billing_address, string order_status)
    {
        UserID = user_id;
        ShippingAddressID = shipping_address;
        BillingAddressID = billing_address;
        OrderStatus = order_status;
    }

    public string InsertSQL()
    {
        return $@"INSERT INTO Orders
                    (UserID, ShippingAddressID, BillingAddressID, OrderStatus, CreateUserID, CreateDateTime)
                  VALUES
                    ('{UserID}', '{ShippingAddressID}', '{BillingAddressID}', '{OrderStatus}', {UserID}, NOW())";
    }

    public string UpdateSQL()
    {
        return $@"UPDATE Orders SET UserID = '{UserID}', ShippingAddressID = '{ShippingAddressID}', BillingAddressID = '{BillingAddressID}', OrderStatus = '{OrderStatus}', UpdateUserID = {UserID}, UpdateDateTime = NOW() WHERE ID = {ID}";
    }

    public string DeleteSQL()
    {
        return $@"DELETE FROM Orders WHERE ID = {ID}";
    }

    public string ReadSQL()
    {
        return $@"SELECT * FROM Orders WHERE ID = {ID}";
    }

    public string ReadSQL(int id)
    {
        return $@"SELECT * FROM Orders WHERE ID = {id}";
    }

    public static string ReadAllSQL()
    {
        return $@"SELECT * FROM Orders";
    }

    public string InsertOrderLineSQL(List<CartItems> cartItems)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

        var sql = $@"INSERT INTO Orderlines (OrderID, ProductID, Quantity, Price, CreateDateTime) VALUES ";
        var values = cartItems.Select(item => $@"({ID}, {item.ProductID}, {item.Quantity}, {item.Price}, NOW())");
        sql += string.Join(", ", values);

        Console.WriteLine(sql);

        using var conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
        conn.Open();
        using var cmd = new MySqlCommand(sql, conn);
        cmd.ExecuteNonQuery();

        return sql;
    }

    public string UpdateProductStockSQL(List<CartItems> cartItems)
    {
        var sql = $@"UPDATE Products SET Stock = Stock - CASE ID ";
        var productIds = cartItems.Select(item => item.ProductID);
        foreach (var item in cartItems)
        {
            sql += $@"WHEN {item.ProductID} THEN {item.Quantity} ";
        }
        sql += $@"END WHERE ID IN ({string.Join(", ", productIds)})";

        Console.WriteLine(sql);

        using var conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR());
        conn.Open();
        using var cmd = new MySqlCommand(sql, conn);
        cmd.ExecuteNonQuery();

        return sql;
    }
}
