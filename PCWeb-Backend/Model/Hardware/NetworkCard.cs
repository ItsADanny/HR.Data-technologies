public class NetworkCard : Product
{
    public string? Protocol {get; set;}
    public string ConnectorInterface {get; set;}
    public bool WirelessCard
    {
        get
        {
            return Protocol is null;
        }
    }

    public NetworkCard(int id, string name, double? price, eColor[]? colors, string? protocol, string connectorInterface)
    : base (id, name, price, colors)
    {
        Protocol = protocol;
        ConnectorInterface = connectorInterface;
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