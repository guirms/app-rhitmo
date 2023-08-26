namespace Infra.Data.Interfaces;

public interface IBaseRepository<T> : IDisposable where T : class
{
    Task SaveAsync(T modelObject);
    Task UpdateAsync(T modelObject);
    Task<T?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}