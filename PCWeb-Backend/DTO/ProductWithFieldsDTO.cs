public class ProductWithFieldsDTO
{
    public string CategoryName { get; set; }
    public int ProductID { get; set; }
    public string Name { get; set; }
    public double? Price { get; set; }
    public string FieldName { get; set; }
    public string FieldValue { get; set; }

    public ProductWithFieldsDTO(string categoryName, int productID, string name, double? price, string fieldName, string fieldValue)
    {
        CategoryName = categoryName;
        ProductID = productID;
        Name = name;
        Price = price;
        FieldName = fieldName;
        FieldValue = fieldValue;
    }
}
