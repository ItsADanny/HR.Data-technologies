public class OS : Product
{
    public int[] Mode {get; set;}
    public int? MaxMemory {get; set;}

    public OS(int id, string name, double? price, int[] mode, int? maxMemory) : base(id, name, price, null)
    {
        Mode = mode;
        MaxMemory = maxMemory;
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