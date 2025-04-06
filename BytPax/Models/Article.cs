using BytPax.Models.core;

namespace BytPax.Models;

public class Article : BaseEntity {
    public string Topic { get; set; }
    public string BodyText { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string  ImagePath { get; set; }
    
    public Article(string topic, string text, int id, string image, Category category)
        : base(id) 
    {
        Topic = topic;
        BodyText = text;
        CategoryId = id;
        ImagePath = image;
        Category = category;
    }
}