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