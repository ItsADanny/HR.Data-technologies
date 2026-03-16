public class Case : Product
{
    public string Type {get; set;}
    public int? PSU {get; set;}
    public string SidePanel {get; set;}
    public double ExternalVolume {get; set;}
    public int Internal35Bays {get; set;}

    public Case(int id, string name, double? price, eColor[]? colors, string type, int? psu, string sidePanel, double externalVolume, int internal35bays) 
    : base(id, name, price, colors)
    {
        Type = type;
        PSU = psu;
        SidePanel = sidePanel;
        ExternalVolume = externalVolume;
        Internal35Bays = internal35bays;
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