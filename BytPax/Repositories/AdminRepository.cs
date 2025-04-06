using BytPax.Models;

namespace BytPax.Repositories
{
    public class AdminRepository<T> : Repository<T> where T : class
    {
        private readonly List<T> _entities = new List<T>();

        public IEnumerable<T> GetAll()
        {
            return _entities;
        }

        public T GetEntityById(long id)
        {
            return _entities.FirstOrDefault(e => (e as dynamic).Id == id);
        }

        public void Add(T entity)
        {
            (entity as dynamic).Id = _entities.Count > 0 ? (dynamic)_entities.Max(e => (e as dynamic).Id) + 1 : 1;
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            var existingEntity = _entities.FirstOrDefault(e => (e as dynamic).Id == (entity as dynamic).Id);
            if (existingEntity != null)
            {
                int index = _entities.IndexOf(existingEntity);
                _entities[index] = entity;
            }
        }

        public void Delete(long id)
        {
            var entity = _entities.FirstOrDefault(e => (e as dynamic).Id == id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
        }
    }
}
