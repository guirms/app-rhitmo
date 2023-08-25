using Infra.Data.Context;
using Infra.Data.Interfaces;

namespace Infra.Data.Repositories;

public class BaseRepository<T>: IBaseRepository<T> where T : class
{
    protected readonly ConfigContext _context;

    public BaseRepository(ConfigContext context)
    {
        _context = context;
    }

    public int Save(T modelObject)
    {
        _context.Set<T>().Add(modelObject);
        return _context.SaveChanges();
    }

    public async Task<int> SaveAsync(T modelObject)
    {
        _context.Set<T>().Add(modelObject);
        return await _context.SaveChangesAsync();
    }

    public int Update(T modelObject)
    {
        _context.Set<T>().Update(modelObject);
        return _context.SaveChanges();
    }

    public T? GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public IList<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public void Delete(int id)
    {
        var modelObject = GetById(id);

        if (modelObject != null) 
            _context.Set<T>().Remove(modelObject);

        _context.SaveChanges();        
    }
        
    public void Dispose()
    {
        _context.Dispose();
    }
}