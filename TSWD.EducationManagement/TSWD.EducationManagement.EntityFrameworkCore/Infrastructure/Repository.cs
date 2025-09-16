using Microsoft.EntityFrameworkCore;

namespace TSWD.EducationManagement.EntityFrameworkCore.Infrastructure
{
    // EfRepository.cs (in EF Core Layer)
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EducationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(EducationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<IQueryable<T>> AsQueryable() => _dbSet.AsQueryable();

    }

}
