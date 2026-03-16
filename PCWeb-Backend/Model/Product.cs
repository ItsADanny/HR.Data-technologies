public class Product
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
}