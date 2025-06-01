using BytPax.Models.core;

namespace BytPax.Models
{
    public class Category : BaseEntity
    {
        public string TypeOfSport { get; set; }
        public string Description { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Athlete> Athletes { get; set; } = new List<Athlete>();
        
        public string Name { get; set; } 

        public Category() { }

        public Category(int id, string name, string type, string description)
            : base(id)
        {
            Name = name;
            TypeOfSport = type;
            Description = description;
        }

    }
}