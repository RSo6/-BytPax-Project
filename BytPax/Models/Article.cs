using BytPax.Models.core;

namespace BytPax.Models;

public class Article : BaseEntity {
    public string Topic { get; }
    public string BodyText { get; }
    public int CategoryId { get;  }
    public Category Category { get; }
    public string  ImagePath {get; }
    
    public Article(string topic, string text, int categoryId, string image, Category category)
        : base() 
    {
        Topic = topic;
        BodyText = text;
        CategoryId = categoryId;
        ImagePath = image;
        Category = category;
    }
}