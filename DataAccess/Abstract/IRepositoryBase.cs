namespace DataAccess.Abstract;
public interface IRepositoryBase<T> where T : class
{
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> Get(int id);
    Task<IEnumerable<T>> GetAll();
}
