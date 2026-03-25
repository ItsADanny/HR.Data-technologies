public class Product : iData
{
    public int ID {get; set;}
    public int Category_ID { get; set;}
    public string Name {get; set;}
    public string? Manufacturer { get; set;}
    public string? Description { get; set;}
    public double? Price {get; set;}
    public int Stock { get; set;}
    public int? Minimal_stock { get; set;}
    public bool Discontinued { get; set;}
    public eColor[]? Colors {get; set;}

    public Product(int id, int category_id, string name, string? manufacturer, string? description, double? price, int stock, int? minimal_stock, bool discontinued, eColor[]? colors)
    {
        ID = id;
        Category_ID = category_id;
        Name = name;
        Manufacturer = manufacturer;
        Description = description;
        Price = price;
        Stock = stock;
        Minimal_Stock = minimal_stock;
        Discontinued = discontinued;
        Colors = colors;
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
}