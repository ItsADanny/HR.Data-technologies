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
}