using Notes.Data.Models;

namespace Notes.Data.Services
{
    public interface IAsyncCrudService<T> where T : class
    {
        Task<T> CreateAsync(T model);
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T model);
        Task DeleteAsync(T model);
        Task<IEnumerable<T>> FindAsync(Func<Note, bool> predicate);
    }
}
