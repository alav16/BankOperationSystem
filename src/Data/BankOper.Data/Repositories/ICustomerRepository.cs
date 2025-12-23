using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOper.Data.Models;

namespace BankOper.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> CreateAsync(data_Customer data_Customer);
        Task<IEnumerable<data_Customer>> GetAllAsync(int pageNumber, int pageSize);
        Task<data_Customer?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<data_Customer> UpdateAsync(data_Customer data_Customer);
    }
}
