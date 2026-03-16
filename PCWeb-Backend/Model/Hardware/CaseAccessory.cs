public class CaseAccessory : Product
{
    public string Type {get; set;}
    public double? FormFactor {get; set;}

    public CaseAccessory(int id, string name, double? price, string type, double? formFactor)
    : base(id, name, price, null)
    {
        Type = type;
        FormFactor = formFactor;
    }
}