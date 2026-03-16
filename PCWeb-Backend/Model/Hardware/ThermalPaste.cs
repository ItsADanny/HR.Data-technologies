public class ThermalPaste : Product
{
    public int Amount {get; set;}

    public ThermalPaste(int id, string name, double? price, int amount) : base(id, name, price, null)
    {
        Amount = amount;
    }
}