public class CPU : Product
{
    public int CoreCount {get; set;}
    public double CoreClock {get; set;}
    public double BoostClock {get; set;}
    public string MicroArchitecture {get; set;}
    public int TDP {get; set;}
    public string Graphics {get; set;}

    public CPU(int id, string name, double? price, int coreCount, double coreClock, 
               double boostClock, string microArch, int tdp, string graphics) 
    : base(id, name, price, null)
    {
        CoreCount = coreCount;
        CoreClock = coreClock;
        BoostClock = boostClock;
        MicroArchitecture = microArch;
        Graphics = graphics;
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