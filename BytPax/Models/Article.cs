using BytPax.Models.core;

namespace BytPax.Models;

public class Article : BaseEntity {
    public string Topic { get; }
    public string BodyText { get; }
    public int CategoryId { get;  }
    public Category Category { get; }
    public string  ImagePath {get; }
    
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