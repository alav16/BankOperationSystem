using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOper.Data.DB;
using BankOper.Data.Models;
using Microsoft.Data.SqlClient;

namespace BankOper.Data.Repositories.Implementation
{
    public class ClientRepository : BaseRepository, IClientRepository
    {
        public ClientRepository(IDbConnectionFactory _connectionFactory) : base(_connectionFactory) { }
   
        public virtual async Task<data_Customer> ProcessTransactionAsync(int customerId, decimal amount, string type)
        {
            var parameters = new[]
            {
                new SqlParameter("@CustomerId", customerId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@OperationType", type)
            };

            var result = await ExecuteReaderAsync(
                "core.CustomerTransaction",
                reader => new data_Customer
                {
                    CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Balance = reader.GetDecimal(reader.GetOrdinal("Balance"))
                },
                parameters);

            return result.FirstOrDefault()
                ?? throw new Exception("Transaction failed: Customer not found");
        }

        public virtual async Task<IEnumerable<data_Operations>> GetHistoryAsync(int customerId, int pageNumber, int pageSize)
        {
            var parameters = new[]
            {
                new SqlParameter("@CustomerId", customerId),
                new SqlParameter("@PageNumber", pageNumber),
                new SqlParameter("@PageSize", pageSize)
            };

            return await ExecuteReaderAsync(
                "core.GetOperationHistory",
                reader => new data_Operations
                {
                    OperaOperationsId = reader.GetInt32(reader.GetOrdinal("OperationsId")),
                    CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                    Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                    OperatonType = reader.GetString(reader.GetOrdinal("OperatonType"))
                },
                parameters);
        }
    }
}
