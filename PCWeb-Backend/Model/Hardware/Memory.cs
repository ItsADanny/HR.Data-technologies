public class Memory : Product
{
    public int DDRFormat {get; set;}
    public int Speed {get; set;}
    public int ModuleAmount {get; set;}
    public int GBAmount {get; set;}
    public int GBAmountPerModule {
        get
        {
            return GBAmount / ModuleAmount;
        }
    }
    public double? PricePerGB
    {
        get
        {
            return Price is null ? null : Price / GBAmount;
        }
    }
    public double? FirstWordLatency {get; set;}
    public int? CASLatency {get; set;}

    public Memory(int id, string name, double? price, eColor[]? colors, int ddrFormat, int speed, int moduleAmount, int gbAmount, 
                  double? firstWordLatency, int? casLatency)
    : base(id, name, price, colors)
    {
        DDRFormat = ddrFormat;
        Speed = speed;
        ModuleAmount = moduleAmount;
        GBAmount = gbAmount;
        FirstWordLatency = firstWordLatency;
        CASLatency = casLatency;
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