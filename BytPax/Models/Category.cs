using BytPax.Models.core;

namespace BytPax.Models;

public class Category : BaseEntity {
    // public string Name { get; }
    public string TypeOfSport { get; }
    public string Description { get; }
    public List<Article> Articles { get; } = new List<Article>();
    public List<Athlete> Athletes { get; } = new List<Athlete>();
    
    public Category(int id , string type, string description) 
        : base(id)
    {
        // Name = name;
        TypeOfSport = type;
        Description = description;
        Articles = new List<Article>();
        Athletes = new List<Athlete>();
    }
}










