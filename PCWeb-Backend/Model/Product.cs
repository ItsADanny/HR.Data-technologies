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

    public Product(int id, string name, double? price, eColor[]? colors)
    {
        ID = id;
        Name = name;
        Price = price;
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