using BytPax.Models.core;

namespace BytPax.Models;

public class Athlete : BaseEntity {
    public int Age { get; }
    public int CategoryId { get; }
    public Category Category { get; }
    public string FullName { get; }
    public string Country { get; }
    public string City { get;  }
    public string Description { get; }
    
    public Athlete(int age, string fullName, string country, int categoryId, Category category, string city, string description)
        : base()
    {
        Age = age;
        FullName = fullName;
        Country = country;
        CategoryId = categoryId;
        Category = category;
        City = city;
        Description = description;
    }
    
}
















