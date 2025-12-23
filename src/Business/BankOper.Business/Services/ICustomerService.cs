using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOper.Business.Models;
using BankOper.Data.Models;

namespace BankOper.Business.Services
{
    public interface ICustomerService
    {
        Task<ApiResponse<int>> CreateAsync(business_Customer business_Customer);
        Task<ApiResponse<IEnumerable<business_Customer>>> GetAllAsync(int pageNumber, int pageSize);
        Task<ApiResponse<business_Customer?>> GetByIdAsync(int id);

        Task<ApiResponse<bool>> DeleteAsync(int id);

        Task<ApiResponse<business_Customer>> UpdateAsync(business_Customer business_Customer);
    }
}
