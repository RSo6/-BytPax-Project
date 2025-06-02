using BytPax.Models.core;

namespace BytPax.Models;

public class RecordHistory : BaseEntity
{
    public int SportId { get; set; } 
    public string AthleteName { get; set; }  
    public string RecordDescription { get; set; } 
    public double RecordValue { get; set; }  
    public DateTime DateAchieved { get; set; }  
}