namespace BytPax.Models;

public class Article {
    public int Id { get; set; }
    public string Topic { get; set; }
    public string BodyText { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string  ImagePath {get; set;}
}