namespace Thunders.TechTest.ApiService.Repositories.Interfaces
{
    public interface IRepository<T, TKey> where T : class
    {
        Task<IEnumerable<T?>> GetAllAsync();
        Task<T?> GetByIdAsync(TKey id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TKey id);
    }
}
