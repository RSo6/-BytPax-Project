// using BytPax.Data;
// using BytPax.Models;
//
// namespace BytPax.Repositories
// {
//     public class AthleteRepository: Repository<Athlete>
//     {
//         
//         public AthleteRepository() 
//             : base(new JsonStorage<Athlete>("Data/athletes.json")) 
//         { }
//         
//         public IEnumerable<Athlete> GetAthletesSortedByAge() => GetAll().OrderBy(a => a.Age).ToList();
//         public IEnumerable<Athlete> GetAthletesSortedByAgeDescending() => GetAll().OrderByDescending(a => a.Age).ToList();
//     }
// }