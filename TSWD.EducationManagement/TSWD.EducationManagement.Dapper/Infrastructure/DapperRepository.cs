using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace TSWD.EducationManagement.Dapper.Infrastructure
{
    public class DapperRepository : IDapperRepository
    {
        private readonly string _connectionString;

        public DapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new ArgumentNullException("Connection string not found.");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
        {
            using var conn = Connection;
            return await conn.QueryAsync<T>(sql, param);
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null)
        {
            using var conn = Connection;
            return await conn.QueryFirstOrDefaultAsync<T>(sql, param);
        }

        public async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            using var conn = Connection;
            return await conn.ExecuteAsync(sql, param);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object? param = null)
        => await Connection.QueryMultipleAsync(sql, param);
    }
}
