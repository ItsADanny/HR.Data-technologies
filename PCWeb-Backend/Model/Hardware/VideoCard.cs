public class VideoCard : Product
{
    public string Chipset {get; set;}
    public int? Memory {get; set;}
    public int? CoreClock {get; set;}
    public int? BoostClock {get; set;}
    public int? Length {get; set;}

    public VideoCard(int id, string name, double? price, eColor[]? colors, string chipset, int? memory, int? coreClock, int? boostClock, int? length)
    : base(id, name, price, colors)
    {
        Chipset = chipset;
        Memory = memory;
        CoreClock = coreClock;
        BoostClock = boostClock;
        Length = length;
    }
}