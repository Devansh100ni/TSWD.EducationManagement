using Dapper;

namespace TSWD.EducationManagement.Dapper.Infrastructure
{
    // IDapperRepository.cs
    public interface IDapperRepository
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);
        Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null);
        Task<int> ExecuteAsync(string sql, object? param = null);

        Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object? param = null);
    }

}
