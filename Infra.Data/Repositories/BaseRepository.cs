using Infra.Data.Context;
using Infra.Data.Interfaces;

namespace Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly ConfigContext _context;

    public BaseRepository(ConfigContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(T modelObject)
    {
        _context.Set<T>().Add(modelObject);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T modelObject)
    {
        _context.Set<T>().Update(modelObject);
        await _context.SaveChangesAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task DeleteAsync(int id)
    {
        var modelObject = await GetByIdAsync(id);

        if (modelObject != null)
            _context.Set<T>().Remove(modelObject);

        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}