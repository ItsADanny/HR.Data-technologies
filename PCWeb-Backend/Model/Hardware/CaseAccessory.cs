public class CaseAccessory : Product
{
    public string Type {get; set;}
    public double? FormFactor {get; set;}

    public CaseAccessory(int id, string name, double? price, string type, double? formFactor)
    : base(id, name, price, null)
    {
        Type = type;
        FormFactor = formFactor;
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