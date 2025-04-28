using BytPax.Models.core;

namespace BytPax.Models;

public class HistoricalEvent : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime EventDate { get; set; }
    public int SportId { get; set; }
    public int ImportanceLevel { get; set; }
    public string? ImagePath { get; set; }
    public int CategoryId { get; set; }
}