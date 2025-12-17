namespace Api.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
       public Task Add(T entity);
       public Task<T> GetById(int id);
       public Task<IEnumerable<T>> GetAll();
    }
}
