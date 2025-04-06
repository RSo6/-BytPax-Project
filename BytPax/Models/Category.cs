using BytPax.Models.core;

namespace BytPax.Models;

public class Category : BaseEntity
{
    public string TypeOfSport { get; set; }
    public string Description { get; set; }
    public List<Article> Articles { get; set; } = new List<Article>();
    public List<Athlete> Athletes { get; set; } = new List<Athlete>();

    // Додаємо для зручності властивість Name, яка буде використовуватись у відображенні
    public string Name => TypeOfSport;

    public Category() { }

    public Category(int id, string type, string description)
        : base(id)
    {
        TypeOfSport = type;
        Description = description;
    }
}
