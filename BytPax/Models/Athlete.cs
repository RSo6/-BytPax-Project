using BytPax.Models.core;
using System.ComponentModel.DataAnnotations;

namespace BytPax.Models
{
    public class Athlete : BaseEntity
    {
        public int Age { get; set; }
        
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        
        public string FullName { get; set; }
        
        public string Country { get; set; }
        
        public string City { get; set; }
        
        public string Description { get; set; }

 
        public Athlete() { }

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
}
