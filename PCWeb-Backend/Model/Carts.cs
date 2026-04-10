public class Carts : iData
{
    public int ID { get; set; }
    public int? UserID { get; set; }
    public DateTime CreateDateTime { get; set; }

    public Carts(int id)
    {
        ID = id;
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