namespace Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<int> AddAsync(T entity);

        Task<int> DeleteAsync(int id);

        Task<int> UpdateAsync(T entity);
    }
}
