public class Colors : iData
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string HexValue { get; set; }
    public DateTime CreateDateTime { get; set; }
    public int? CreateUserID { get; set; }

    public Colors(int id, string name, string hexValue)
    {
        ID = id;
        Name = name;
        HexValue = hexValue;
    }

    public static string ReadAllSQL()
    {
        throw new NotImplementedException();
    }

    public string DeleteSQL()
    {
        throw new NotImplementedException();
    }

    public string InsertSQL()
    {
        throw new NotImplementedException();
    }

    public string ReadSQL()
    {
        throw new NotImplementedException();
    }

    public string ReadSQL(int id)
    {
        throw new NotImplementedException();
    }

    public string UpdateSQL()
    {
        throw new NotImplementedException();
    }
}