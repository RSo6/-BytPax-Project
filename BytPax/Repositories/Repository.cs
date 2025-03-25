using BytPax.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BytPax.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _entities = new();

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