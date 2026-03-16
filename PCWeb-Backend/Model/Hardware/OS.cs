public class OS : Product
{
    public int[] Mode {get; set;}
    public int? MaxMemory {get; set;}

    public OS(int id, string name, double? price, int[] mode, int? maxMemory) : base(id, name, price, null)
    {
        Mode = mode;
        MaxMemory = maxMemory;
    }
}