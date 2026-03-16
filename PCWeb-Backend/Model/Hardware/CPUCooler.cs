public class CPUCooler : Product
{
    public double[]? RPM {get; set;}
    public double[]? NoiseLevel {get; set;}
    public int Size {get; set;}

    public CPUCooler(int id, string name, double? price, eColor[]? colors, double[]? rpm, double[]? noiseLevel, int size) 
    : base(id, name, price, colors)
    {
        RPM = rpm;
        NoiseLevel = noiseLevel;
        Size = size;
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