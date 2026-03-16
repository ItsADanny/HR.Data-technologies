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
}