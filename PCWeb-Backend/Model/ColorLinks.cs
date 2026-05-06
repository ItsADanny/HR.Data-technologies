public class ColorLinks : iData
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public string ColorID { get; set; }
    public DateTime CreateDateTime { get; set; }
    public int? CreateUserID { get; set; }

    public ColorLinks(int id, int productID, string colorID)
    {
        ID = id;
        ProductID = productID;
        ColorID = colorID;
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