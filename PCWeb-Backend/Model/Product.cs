using MySql.Data.MySqlClient;

public class Product : iData
{
    public int ID {get; set;}
    public int? CategoryID {get; set;}
    public string Name {get; set;}
    public string Manufacturer {get; set;}
    public string Description {get; set;}
    public double? Price {get; set;}
    public int Stock {get; set;}
    public int MinimalStock {get; set;}
    public bool Discontinued {get; set;}
    public DateTime CreateDateTime {get; set;}
    public DateTime? UpdateDateTime {get; set;}
    public int? CreateUserID {get; set;}
    public int? UpdateUserID {get; set;}

    
    // deze zit nog niet in database
    public eColor[]? Colors {get; set;}

    public Product(int id, int category_id, string name, string? manufacturer, string? description, double? price, int stock, int minimal_stock, bool discontinued, eColor[]? colors)
    {
        ID = id;
        CategoryID = category_id;
        Name = name;
        Manufacturer = manufacturer;
        Description = description;
        Price = price;
        Stock = stock;
        MinimalStock = minimal_stock;
        Discontinued = discontinued;
        Colors = colors;
    }

    public Product(int id, int category_id, string name, double? price, eColor[]? colors)
    {
        ID = id;
        CategoryID = category_id;
        Name = name;
        Price = price;
        Colors = colors;
    }

    private List<ProductWithFieldsDTO>? ReadAllProductsWithCategory(int categoryID)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(Product.ReadAllWithCategorySQL(categoryID), conn))
            {
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<ProductWithFieldsDTO> products = new List<ProductWithFieldsDTO>();

                    while (reader.Read())
                    {
                        products.Add(new ProductWithFieldsDTO(
                            reader["CategoryName"]?.ToString() ?? string.Empty,
                            Convert.ToInt32(reader["ProductID"]),
                            reader["Name"]?.ToString() ?? string.Empty,
                            reader["Price"] is not DBNull ? Convert.ToDouble(reader["Price"]) : null,
                            reader["FieldName"]?.ToString() ?? string.Empty,
                            reader["FieldValue"]?.ToString() ?? string.Empty
                        ));
                    }

                    return products;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR in ReadAllProductsWithCategory:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public virtual string InsertSQL()
    {
        throw new NotImplementedException();
    }

    public virtual string UpdateSQL()
    {
        throw new NotImplementedException();
    }

    public virtual string DeleteSQL()
    {
        throw new NotImplementedException();
    }

    public virtual string ReadSQL()
    {
        throw new NotImplementedException();
    }

    public virtual string ReadSQL(int id)
    {
        throw new NotImplementedException();
    }

    public static string ReadAllSQL()
    {
        throw new NotImplementedException();
    }

    public static string ReadAllWithCategorySQL(int categoryId, int pageSize = 100, int offset = 0)
    {
        return $@"SELECT
            c.CategoryName,
            p.ID AS ProductID,
            p.Name AS Name,
            p.Price AS Price,
            cf.Name AS FieldName,
            COALESCE(
                pf.StringValue,
                pf.IntValue,
                pf.DoubleValue,
                pf.BooleanValue,
                pf.DateTimeValue
            ) AS FieldValue
        FROM Categories c
        INNER JOIN (
            SELECT ID, CategoryID, Name, Price
            FROM Products
            WHERE CategoryID = {categoryId}
            ORDER BY ID
            LIMIT {pageSize} OFFSET {offset}
        ) p ON c.ID = p.CategoryID
        INNER JOIN ProductFields pf ON p.ID = pf.ProductID
        INNER JOIN CategoryFields cf ON pf.FieldID = cf.ID
        ORDER BY p.ID, cf.Name";
    }

    public static List<ProductWithFieldsDTO>? ReadAllProductsWithCategory(int categoryID, int pageSize = 100, int offset = 0)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(Product.ReadAllWithCategorySQL(categoryID, pageSize, offset), conn))
            {
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<ProductWithFieldsDTO> products = new List<ProductWithFieldsDTO>();

                    while (reader.Read())
                    {
                        products.Add(new ProductWithFieldsDTO(
                            reader["CategoryName"]?.ToString() ?? string.Empty,
                            Convert.ToInt32(reader["ProductID"]),
                            reader["Name"]?.ToString() ?? string.Empty,
                            reader["Price"] is not DBNull ? Convert.ToDouble(reader["Price"]) : null,
                            reader["FieldName"]?.ToString() ?? string.Empty,
                            reader["FieldValue"]?.ToString() ?? string.Empty
                        ));
                    }

                    return products;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR in ReadAllProductsWithCategory:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }
}