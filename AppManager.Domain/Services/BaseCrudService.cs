namespace AppManager.Domain.Services;

public interface BaseCrudService<T> where T : class
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    T? GetById(int id);
    IEnumerable<T> GetAll(string query);
}