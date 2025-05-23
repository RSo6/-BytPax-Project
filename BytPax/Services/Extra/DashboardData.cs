using BytPax.Models;

namespace BytPax.Services.Extra;

public class DashboardData
{
    public int AthletesCount { get; set; }
    public int ArticlesCount { get; set; }
    public int RecordsCount { get; set; }
    public int EventsCount { get; set; }

    public IEnumerable<Athlete> Athletes { get; set; }
    public IEnumerable<Article> Articles { get; set; }
    public IEnumerable<RecordHistory> Records { get; set; }
    public IEnumerable<HistoricalEvent> Events { get; set; }
}
