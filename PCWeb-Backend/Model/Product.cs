public class Product : iData
{
    public int ID {get; set;}
    public string Name {get; set;}
    public double? Price {get; set;}
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