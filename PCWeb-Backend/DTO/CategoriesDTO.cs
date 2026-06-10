public class CategoriesDTO
{
    public int ID { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }

    public CategoriesDTO(int id, string categoryName, string description)
    {
        ID = id;
        CategoryName = categoryName;
        Description = description;
    }
}