using BytPax.Models.core;

namespace BytPax.Instructions;

public interface IDataStorage<T> where T : class
{
    List<T> GetAll();
    T? GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(int id);
    void Save();
}