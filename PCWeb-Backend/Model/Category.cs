using System;

public class Category
{
	public int ID { get; set; }
	public string Category_Name { get; set; }
	public string? Description { get; set; }

	public Category(int id, string category_name, string? description)
	{
		id = id;
		Category_Name = category_name;
		Description = description;
	}
}
