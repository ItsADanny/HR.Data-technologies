public class UserSession : iData
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public string SessionToken { get; set; }
    public DateTime Expiration { get; set; }

    public string InsertSQL()
    {
        return "NOT IMPLEMENTED BECAUSE THIS OBJECT IS NOT MEANT TO BE INSERTED INTO A SQL DATABASE";
    }

    public string UpdateSQL()
    {
        return "NOT IMPLEMENTED BECAUSE THIS OBJECT IS NOT MEANT TO BE UPDATED IN A SQL DATABASE";
    }

    public string DeleteSQL()
    {
        return "NOT IMPLEMENTED BECAUSE THIS OBJECT IS NOT MEANT TO BE DELETED FROM A SQL DATABASE";
    }

    public string ReadSQL()
    {
        throw new NotImplementedException();
    }

    public string ReadSQL(int id)
    {
        throw new NotImplementedException();
    }

    public static string ReadAllSQL()
    {
        throw new NotImplementedException();
    }
}