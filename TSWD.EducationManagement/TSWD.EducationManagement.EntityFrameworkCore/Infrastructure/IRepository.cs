using System.Linq.Expressions;

namespace TSWD.EducationManagement.EntityFrameworkCore.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<T?> FindAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct = default);
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllNoTrackingAsync(CancellationToken ct = default);
        Task<IEnumerable<TResult>> GetAllNoTrackingAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task DeleteAsync(T entity, CancellationToken ct = default);

        Task<int> SaveChangesAsync(CancellationToken ct = default);

        Task<IQueryable<T>> AsQueryable(CancellationToken ct = default);
    }
}
