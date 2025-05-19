using BytPax.Data.Database;
using BytPax.Instructions;
using BytPax.Models.core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class EfStorage<T> : IDataStorage<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly ILogger<EfStorage<T>> _logger;
    private readonly DbSet<T> _dbSet;

    public EfStorage(AppDbContext context, ILogger<EfStorage<T>> logger)
    {
        _context = context;
        _logger = logger;
        _dbSet = _context.Set<T>();
    }

    public List<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(int id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null)
            _dbSet.Remove(entity);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}