public class Motherboard : Product
{
    public string Socket { get; set; }
    public string FormFactor { get; set; }
    public int? MaxMemory { get; set; }
    public int? MemorySlots { get; set; }

    public Motherboard(int id, string name, double? price, eColor[]? colors, string socket, string formFactor, int? maxMemory, int? memorySlots) 
    : base(id, name, price, colors)
    {
        Socket = socket;
        FormFactor = formFactor;
        MaxMemory = maxMemory;
        MemorySlots = memorySlots;
    }
}