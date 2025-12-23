using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOper.Data.DB;
using BankOper.Data.Models;
using Microsoft.Data.SqlClient;

namespace BankOper.Data.Repositories.Implementation
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(IDbConnectionFactory _connectionFactory) : base(_connectionFactory) { }

        public virtual async Task<int> CreateAsync(data_Customer data_Customer)
        {
            var parameters = new[]
            {
                new SqlParameter("@FullName", data_Customer.FullName),
                new SqlParameter("@Email", data_Customer.Email)
            };

            int id = await ExecuteScalarAsync<int>("core.CreateCustomer", parameters);
            return id;
        }


        public virtual async Task<IEnumerable<data_Customer>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;

            var parameters = new[]
            {
                new SqlParameter("@Skip", skip),
                new SqlParameter("@Take", take)
            };

            return await ExecuteReaderAsync(
                "core.CustomerGetAll",
                reader => new data_Customer
                {
                    CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Balance = reader.GetDecimal(reader.GetOrdinal("Balance"))
                },
                parameters);
        }

        public virtual async Task<data_Customer?> GetByIdAsync(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@CustomerId", id)
            };

            IEnumerable<data_Customer> customer = await ExecuteReaderAsync(
                "core.CustomerGetById",
                reader => new data_Customer
                {
                    CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Balance = reader.GetDecimal(reader.GetOrdinal("Balance"))
                },
                parameters);

            return customer.FirstOrDefault();

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@CustomerId", id)
            };

            int res = await ExecuteScalarAsync<int>("core.CustomerDelete", parameters);
            return res == 1;
        }

        public virtual async Task<data_Customer> UpdateAsync(data_Customer data_Customer)
        {
            var parameters = new[]
            {
                new SqlParameter("@CustomerId", data_Customer.CustomerId),
                new SqlParameter("@FullName", data_Customer.FullName),
                new SqlParameter("@Email", data_Customer.Email),
                new SqlParameter("@Balance", data_Customer.Balance)
            };

            var author = await ExecuteReaderAsync(
                "core.CustomerUpdate",
                reader => new data_Customer
                {
                    CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Balance = reader.GetDecimal(reader.GetOrdinal("Balance"))
                },
                parameters);

            return author.FirstOrDefault()
                ?? throw new InvalidOperationException(" not found after update");
        }
    }
}
