public class PowerSupply : Product
{
    public string? Type {get; set;}
    public string? Efficiency {get; set;}
    public int Wattage {get; set;}
    public string? Modular {get; set;}

    public PowerSupply(int id, string name, double? price, eColor[]? colors, string? type, string? efficiency, int wattage, string? modular)
    : base(id, name, price, colors)
    {
        Type = type;
        Efficiency = efficiency;
        Wattage = wattage;
        Modular = modular;
    }

    public override string InsertSQL()
    {
        throw new NotImplementedException();
    }

    public override string UpdateSQL()
    {
        throw new NotImplementedException();
    }

    public override string DeleteSQL()
    {
        throw new NotImplementedException();
    }

    public override string ReadSQL()
    {
        throw new NotImplementedException();
    }

    public override string ReadSQL(int id)
    {
        throw new NotImplementedException();
    }

    public new static string ReadAllSQL()
    {
        throw new NotImplementedException();
    }
}