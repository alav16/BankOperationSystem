using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOper.Data.DB;
using Microsoft.Data.SqlClient;

namespace BankOper.Data.Repositories.Implementation
{
    public abstract class BaseRepository
    {
        protected readonly IDbConnectionFactory _connectionFactory;
        protected BaseRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        protected virtual async Task<T?> ExecuteScalarAsync<T>(string spName, SqlParameter[] parameters)
        {
            await using var connection = await _connectionFactory.CreateConnectionAsync();
            await using var command = new SqlCommand(spName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            var result = await command.ExecuteScalarAsync();
            if (result is DBNull || result is null)
            {
                return default;
            }
            return (T)Convert.ChangeType(result, typeof(T));
        }
        protected virtual async Task<IEnumerable<T>> ExecuteReaderAsync<T>(
            string spName,
            Func<SqlDataReader, T> mapper,
            SqlParameter[]? parameters = null)
        {
            var list = new List<T>();
            await using var connection = await _connectionFactory.CreateConnectionAsync();
            await using var command = new SqlCommand(spName, connection)
            {
                CommandType = CommandType.StoredProcedure,
            };
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(mapper(reader));
            }
            return list;
        }
    }
}
