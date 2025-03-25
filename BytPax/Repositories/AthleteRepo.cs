using BytPax.Models;


namespace BytPax.Repositories;

public class AthleteRepo<T> : Repository<T> where T : Athlete
{
    public IEnumerable<T> GetAthletesSortedByAge()
    {
        return _entities.OrderBy(a => a.Age).ToList();
    }

    public IEnumerable<T> GetAthletesSortedByAgeDescending()
    {
        return _entities.OrderByDescending(a => a.Age).ToList();
    }
    
}