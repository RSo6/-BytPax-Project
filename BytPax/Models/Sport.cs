using BytPax.Models.core;

namespace BytPax.Models;

public class Sport : BaseEntity
{
    public string Name { get; set; }           
    public string Description { get; set; }
    public int? OriginYear { get; set; }
    public List<HistoricalEvent> HistoricalEvents { get; set; } = new List<HistoricalEvent>();
}