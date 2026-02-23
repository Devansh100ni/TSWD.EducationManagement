using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TSWD.EducationManagement.EntityFrameworkCore.Infrastructure
{
    // EfRepository.cs (in EF Core Layer)
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EducationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(EducationDbContext context, CancellationToken ct = default)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default) => await _dbSet.FindAsync(id, ct);

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default) => await _dbSet.ToListAsync(cancellationToken: ct);

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct = default) => await _dbSet
                .Select(selector)
                .ToListAsync(ct);

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(predicate)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TResult>> GetAllNoTrackingAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllNoTrackingAsync(CancellationToken ct = default)
        => await _dbSet
                .AsNoTracking()
                .ToListAsync(ct);

        public async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            var addedEntry = await _dbSet.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
            return addedEntry.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entity, CancellationToken ct = default)
        {
            await _dbSet.AddRangeAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(T entity, CancellationToken ct = default)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
        {
            if (entities == null || !entities.Any())
                return; // Nothing to delete

            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default) => await _context.SaveChangesAsync(ct);

        public async Task<IQueryable<T>> AsQueryable(CancellationToken ct = default) => _dbSet.AsQueryable();

        public async Task<T?> FindAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync(id, ct);
        }
    }

}
