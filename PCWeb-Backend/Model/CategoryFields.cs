public class CategoryFields : iData
{
    public int ID { get; set; }
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public string ValueType { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }
    public int? CreateUserID { get; set; }
    public int? UpdateUserID { get; set; }

    public CategoryFields(int id, int categoryID, string name, string valueType)
    {
        ID = id;
        CategoryID = categoryID;
        Name = name;
        ValueType = valueType;
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