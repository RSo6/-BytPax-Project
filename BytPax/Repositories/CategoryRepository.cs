using BytPax.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BytPax.Repositories
{
    public class CategoryRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "category.json");

        public List<Category> GetAll()
        {
            if (File.Exists(_filePath))
            {
                var jsonData = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<Category>>(jsonData) ?? new List<Category>();
            }
            return new List<Category>();
        }

        public void Add(Category category)
        {
            var categories = GetAll();
            categories.Add(category);
            SaveToFile(categories);
        }

        public void SaveToFile(List<Category> categories)
        {
            var jsonData = JsonConvert.SerializeObject(categories, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
        public Category GetEntityById(long id)
        {
            return GetAll().FirstOrDefault(c => c.Id == id);
        }
    }
}