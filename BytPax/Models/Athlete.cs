using BytPax.Models.core;
using System.ComponentModel.DataAnnotations;

namespace BytPax.Models
{
    public class Athlete : BaseEntity
    {
        [Required(ErrorMessage = "Вік обов'язковий")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Категорія обов'язкова")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; } // nullable


        [Required(ErrorMessage = "Ім'я обов'язкове")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Країна обов'язкова")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Місто обов'язкове")]
        public string City { get; set; }

        [Required(ErrorMessage = "Опис обов'язковий")]
        public string Description { get; set; }

        // Конструктори
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
