namespace BytPax.Models;

public class Articles {
    public int Id { get; set; }
    public string Topic { get; set; }
    public string BodyText { get; set; }
    public string CategoryId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string  ImagePath {get; set;}
}