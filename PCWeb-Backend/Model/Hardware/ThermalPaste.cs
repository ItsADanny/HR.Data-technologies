public class ThermalPaste : Product
{
    public int Amount {get; set;}

    public ThermalPaste(int id, string name, double? price, int amount) : base(id, name, price, null)
    {
        Amount = amount;
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