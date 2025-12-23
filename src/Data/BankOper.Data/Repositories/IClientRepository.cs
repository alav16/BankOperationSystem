using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOper.Data.Models;

namespace BankOper.Data.Repositories
{
    public interface IClientRepository
    {
        Task<data_Customer> ProcessTransactionAsync(int customerId, decimal amount, string type);
        Task<IEnumerable<data_Operations>> GetHistoryAsync(int customerId, int pageNumber, int pageSize);

    }
}