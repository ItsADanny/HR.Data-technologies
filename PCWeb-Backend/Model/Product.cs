using MySql.Data.MySqlClient;

// public class Product : iData
public class Product
{
    public int ID {get; set;}
    public int? CategoryID {get; set;}
    public string Name {get; set;}
    public string Manufacturer {get; set;}
    public string Description {get; set;}
    public double? Price {get; set;}
    public List<ProductFields>? Fields {get; set;}
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

    public virtual string UpdateSQL()
    {
        throw new NotImplementedException();
    }

    public virtual string DeleteSQL()
    {
        throw new NotImplementedException();
    }

    public static string ReadSQL()
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
        ) p ON c.ID = p.CategoryID
        INNER JOIN ProductFields pf ON p.ID = pf.ProductID
        INNER JOIN CategoryFields cf ON pf.FieldID = cf.ID
        ORDER BY p.ID, cf.Name";
    }

    public static string ReadSQL(int pageSize = 100, int offset = 0)
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
            ORDER BY ID
            LIMIT {pageSize} OFFSET {offset}
        ) p ON c.ID = p.CategoryID
        INNER JOIN ProductFields pf ON p.ID = pf.ProductID
        INNER JOIN CategoryFields cf ON pf.FieldID = cf.ID
        ORDER BY p.ID, cf.Name";
    }

    public static string ReadSQL(int id)
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
            WHERE ID = {id}
        ) p ON c.ID = p.CategoryID
        INNER JOIN ProductFields pf ON p.ID = pf.ProductID
        INNER JOIN CategoryFields cf ON pf.FieldID = cf.ID
        ORDER BY p.ID, cf.Name";
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

    public static string ReadAllWithCategoryWithBrandSQL(int categoryId, string brand, int pageSize = 100, int offset = 0)
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
            AND Manufacturer = '{brand}'
            ORDER BY ID
            LIMIT {pageSize} OFFSET {offset}
        ) p ON c.ID = p.CategoryID
        INNER JOIN ProductFields pf ON p.ID = pf.ProductID
        INNER JOIN CategoryFields cf ON pf.FieldID = cf.ID
        ORDER BY p.ID, cf.Name";
    }

    public static string SearchSQL(string query)
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
            WHERE Name LIKE %{query}%
            ORDER BY ID
        ) p ON c.ID = p.CategoryID
        INNER JOIN ProductFields pf ON p.ID = pf.ProductID
        INNER JOIN CategoryFields cf ON pf.FieldID = cf.ID
        ORDER BY p.ID, cf.Name";
    }

    public static string SearchSQL(string query, int pageSize = 100, int offset = 0)
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
            WHERE Name LIKE %{query}%
            ORDER BY ID
            LIMIT {pageSize} OFFSET {offset}
        ) p ON c.ID = p.CategoryID
        INNER JOIN ProductFields pf ON p.ID = pf.ProductID
        INNER JOIN CategoryFields cf ON pf.FieldID = cf.ID
        ORDER BY p.ID, cf.Name";
    }

    public virtual string InsertSQL()
    {
        // SECURITY: Escape single quotes in user input to prevent SQL Injection attacks
        // Similar to User.cs pattern - prevents malicious input like "'); DROP TABLE Products; --"
        string escapedName = GeneralMethods.SQLInjectionSanitizer(Name);
        string escapedManufacturer = GeneralMethods.SQLInjectionSanitizer(Manufacturer ?? string.Empty);
        string escapedDescription = GeneralMethods.SQLInjectionSanitizer(Description ?? string.Empty);

        // Format DateTime to MySQL compatible format
        string formattedCreateDateTime = CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        // Build the INSERT statement
        // Using string.Empty for escaped fields that are empty strings, NULL for actual null values
        string manufacturerValue = string.IsNullOrEmpty(Manufacturer) ? "NULL" : $"'{escapedManufacturer}'";
        string descriptionValue = string.IsNullOrEmpty(Description) ? "NULL" : $"'{escapedDescription}'";
        string priceValue = Price.HasValue ? Price.ToString() : "NULL";

        return $@"INSERT INTO Products 
            (CategoryID, Name, Manufacturer, Description, Price, Stock, MinimalStock, Discontinued, CreateDateTime, CreateUserID) 
            VALUES 
            ({CategoryID}, '{escapedName}', {manufacturerValue}, {descriptionValue}, {priceValue}, {Stock}, {MinimalStock}, {(Discontinued ? 1 : 0)}, '{formattedCreateDateTime}', {CreateUserID});";
    }

    public static List<ProductWithFieldsDTO>? ReadAllProductsWithCategory(int categoryID, int pageSize = 100, int offset = 0)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(ReadAllWithCategorySQL(categoryID, pageSize, offset), conn))
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

    public static List<ProductWithFieldsDTO>? ReadAllProductsWithCategoryWithBrand(int categoryID, string brand, int pageSize = 100, int offset = 0)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(ReadAllWithCategoryWithBrandSQL(categoryID, brand, pageSize, offset), conn))
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
            Console.WriteLine("ERROR in ReadAllProductsWithCategoryWithBrand:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public static List<ProductWithFieldsDTO>? ReadProductByID(int id)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(ReadSQL(id), conn))
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
            Console.WriteLine("ERROR in ReadProductByID:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public static List<ProductWithFieldsDTO>? ReadProducts()
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(ReadSQL(), conn))
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
            Console.WriteLine("ERROR in ReadProductByID:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public static List<ProductWithFieldsDTO>? ReadProducts(int pageSize = 100, int offset = 0)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(ReadSQL(pageSize, offset), conn))
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
            Console.WriteLine("ERROR in ReadProductByID:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public static List<ProductWithFieldsDTO>? SearchProducts(string query)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(SearchSQL(query), conn))
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
            Console.WriteLine("ERROR in SearchProducts:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public static List<ProductWithFieldsDTO>? SearchProducts(string query, int pageSize = 100, int offset = 0)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(SearchSQL(query, pageSize, offset), conn))
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
            Console.WriteLine("ERROR in SearchProducts:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public static List<ProductWithFieldsDTO>? CreateProduct(int categoryId, Product product)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            {
                conn.Open();

                // ========== STEP 1: INSERT THE PRODUCT ==========
                using (MySqlCommand cmd = new MySqlCommand(product.InsertSQL(), conn))
                {
                    // Log the command for debugging
                    Console.WriteLine("Executing SQL Command:");
                    Console.WriteLine(cmd.CommandText);
                    
                    // Execute the insert
                    cmd.ExecuteNonQuery();
                }

                // ========== STEP 2: GET THE NEWLY INSERTED PRODUCT ID ==========
                int productID;
                using (MySqlCommand cmd = new MySqlCommand("SELECT last_insert_id() AS id", conn))
                {
                    productID = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // ========== STEP 3: INSERT ALL PRODUCT FIELDS ==========
                // Each field in the product's Fields list needs to be inserted separately
                // The field values are stored in different columns based on the CategoryFields.ValueType
                if (product.Fields != null && product.Fields.Count > 0)
                {
                    foreach (ProductFields field in product.Fields)
                    {
                        // Set the product ID for this field (it wasn't set when the Product object was created)
                        field.ProductID = productID;
                        field.CreateDateTime = DateTime.Now;
                        field.CreateUserID = product.CreateUserID;

                        // Escape field ID in case it contains special characters
                        string escapedFieldID = GeneralMethods.SQLInjectionSanitizer(field.FieldID ?? string.Empty);
                        
                        // Format DateTime if present
                        string dateTimeValue = field.DateTimeValue.HasValue 
                            ? field.DateTimeValue.Value.ToString("yyyy-MM-dd HH:mm:ss") 
                            : "NULL";
                        
                        // Build the INSERT statement for this field
                        // Only one of the value columns will have a value; others stay NULL
                        string fieldInsertSQL = $@"INSERT INTO ProductFields 
                            (ProductID, FieldID, LinkedProductField, StringValue, IntValue, DoubleValue, DateTimeValue, BooleanValue, IsArray, CreateDateTime, CreateUserID) 
                            VALUES 
                            ({productID}, '{escapedFieldID}', {field.LinkedProductField}, {(field.StringValue != null ? $"'{GeneralMethods.SQLInjectionSanitizer(field.StringValue)}'" : "NULL")}, {field.IntValue?.ToString() ?? "NULL"}, {field.DoubleValue?.ToString() ?? "NULL"}, {(field.DateTimeValue.HasValue ? $"'{dateTimeValue}'" : "NULL")}, {(field.BooleanValue.HasValue ? (field.BooleanValue.Value ? "1" : "0") : "NULL")}, {(field.IsArray.HasValue ? (field.IsArray.Value ? "1" : "0") : "NULL")}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', {field.CreateUserID});";

                        using (MySqlCommand cmd = new MySqlCommand(fieldInsertSQL, conn))
                        {
                            // Log the command for debugging
                            Console.WriteLine("Executing SQL Command:");
                            Console.WriteLine(cmd.CommandText);
                            
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // ========== STEP 4: RETRIEVE AND RETURN THE CREATED PRODUCT WITH ALL ITS FIELDS ==========
                // Use the existing ReadProductByID method to get the full product data with all fields
                return ReadProductByID(productID);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR in CreateProduct:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public static bool UpdateProduct(int id, Product updatedProduct)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand(updatedProduct.UpdateSQL(), conn))
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if a row was updated
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR in UpdateProduct:");
            Console.WriteLine(e.ToString());
            return false;
        }
    }

    public static bool UpdateProductFields(int productId, List<ProductFields> updatedFields)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            {
                conn.Open();

                foreach (ProductFields field in updatedFields)
                {
                    // Set the product ID for this field (it may not be set when the ProductFields object is created)
                    field.ProductID = productId;
                    field.UpdateDateTime = DateTime.Now;

                    // Escape field ID in case it contains special characters
                    string escapedFieldID = GeneralMethods.SQLInjectionSanitizer(field.FieldID ?? string.Empty);

                    // Format DateTime if present
                    string dateTimeValue = field.DateTimeValue.HasValue 
                        ? field.DateTimeValue.Value.ToString("yyyy-MM-dd HH:mm:ss") 
                        : "NULL";

                    // Build the UPDATE statement for this field
                    // Only one of the value columns will have a value; others stay NULL
                    string fieldUpdateSQL = $@"UPDATE ProductFields SET 
                        LinkedProductField = {field.LinkedProductField},
                        StringValue = {(field.StringValue != null ? $"'{GeneralMethods.SQLInjectionSanitizer(field.StringValue)}'" : "NULL")},
                        IntValue = {field.IntValue?.ToString() ?? "NULL"},
                        DoubleValue = {field.DoubleValue?.ToString() ?? "NULL"},
                        DateTimeValue = {(field.DateTimeValue.HasValue ? $"'{dateTimeValue}'" : "NULL")},
                        BooleanValue = {(field.BooleanValue.HasValue ? (field.BooleanValue.Value ? "1" : "0") : "NULL")},
                        IsArray = {(field.IsArray.HasValue ? (field.IsArray.Value ? "1" : "0") : "NULL")},
                        UpdateDateTime = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',
                        UpdateUserID = {field.UpdateUserID}
                        WHERE ProductID = {productId} AND FieldID = '{escapedFieldID}';";

                    using (MySqlCommand cmd = new MySqlCommand(fieldUpdateSQL, conn))
                    {
                        // Log the command for debugging
                        Console.WriteLine("Executing SQL Command:");
                        Console.WriteLine(cmd.CommandText);
                        
                        cmd.ExecuteNonQuery();
                    }
                }

                return true; // Return true if all updates were attempted
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR in UpdateProductFields:");
            Console.WriteLine(e.ToString());
            return false;
        }
    }

    public static bool DeleteProduct(int id)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand($@"DELETE FROM Products WHERE ID = {id};", conn))
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if a row was deleted
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR in DeleteProduct:");
            Console.WriteLine(e.ToString());
            return false;
        }
    }

    public static List<string>? ReadAllBrandsInSameCategory(int categoryID)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(DBHandler.DBConfig_MySQL.GetConnectionSTR()))
            using (MySqlCommand cmd = new MySqlCommand($@"SELECT DISTINCT Manufacturer FROM Products WHERE CategoryID = {categoryID} AND Manufacturer IS NOT NULL", conn))
            {
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<string> brands = new List<string>();

                    while (reader.Read())
                    {
                        brands.Add(reader["Manufacturer"]?.ToString() ?? string.Empty);
                    }

                    return brands;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR in ReadAllBrandsInSameCategory:");
            Console.WriteLine(e.ToString());
            return null;
        }
    }
}
