using BytPax.Models.core;
using System.ComponentModel.DataAnnotations;

namespace BytPax.Models
{
    public class Athlete : BaseEntity
    {
        [Required(ErrorMessage = "³� ����'�������")]
        public int Age { get; set; }

        [Required(ErrorMessage = "�������� ����'������")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; } // nullable


        [Required(ErrorMessage = "��'� ����'������")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "����� ����'������")]
        public string Country { get; set; }

        [Required(ErrorMessage = "̳��� ����'������")]
        public string City { get; set; }

        [Required(ErrorMessage = "���� ����'�������")]
        public string Description { get; set; }

        // ������������
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
