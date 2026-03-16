public class InternalHardDrive : Product
{
    public int Capacity {get; set;}
    public double? PricePerGB {
        get
        {
            return Price is null ? null : Price / Capacity;
        }
    }
    public string Type {get; set;}
    public int? Cache {get; set;}
    public string? FormFactor {get; set;}
    public string? ConnectionInterface {get; set;}
    
    public InternalHardDrive(int id, string name, double? price, int capacity, string type, int? cache,
                             string? formFactor, string? connectionInterface)
    : base(id, name, price, null)
    {
        Capacity = capacity;
        Type = type;
        Cache = cache;
        FormFactor = formFactor;
        ConnectionInterface = connectionInterface;
    }
}