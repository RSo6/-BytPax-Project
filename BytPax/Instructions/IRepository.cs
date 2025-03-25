namespace BytPax.Instructions;

public interface IRepository<T> where T: class
{
    IEnumerable<T> GetAll();
    T GetEntityById(long id);
    void Add(T entity);
    void Update(T entity);
    void Delete(long id);
}