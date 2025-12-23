using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOper.Business.Models;
using BankOper.Data.Models;

namespace BankOper.Business.Services
{
    public interface IClientService
    {
        Task<ApiResponse<business_Customer>> ExecuteTransactionAsync
            (int customerId, decimal amount, string type);
        Task<ApiResponse<IEnumerable<data_Operations>>> GetCustomerHistoryAsync
            (int customerId, int page, int size);
    }
}
