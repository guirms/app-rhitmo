namespace Infra.Data.Interfaces;

public interface IBaseRepository<T>: IDisposable where T: class
{
    int Save(T modelObject);
    Task<int> SaveAsync(T modelObject);
    int Update(T modelObject);
    T? GetById(int id);
    IList<T> GetAll();
    void Delete(int id);
}