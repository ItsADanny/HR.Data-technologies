public class ProductFields : iData
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public string FieldID { get; set; }
    public int LinkedProductField { get; set; }
    public string? StringValue { get; set; }
    public int? IntValue { get; set; }
    public double? DoubleValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
    public bool? BooleanValue { get; set; }
    public bool? IsArray { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }
    public int? CreateUserID { get; set; }
    public int? UpdateUserID { get; set; }

    // deze geen idee
    public ProductFields(int id, int productID, string fieldID, string fieldValue)
    {
        ID = id;
        ProductID = productID;
        FieldID = fieldID;
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