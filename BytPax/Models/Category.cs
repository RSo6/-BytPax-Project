namespace BytPax.Models;

public class Category  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string TypeOfSport { get; set; }
    public string Description { get; set; }
    public List<Article> Articles { get; set; } = new List<Article>();
    public List<Athlete> Athletes { get; set; } = new List<Athlete>();
    
}










