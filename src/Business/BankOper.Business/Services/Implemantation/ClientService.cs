using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOper.Business.Models;
using BankOper.Data.Models;
using BankOper.Data.Repositories;

namespace BankOper.Business.Services.Implemantation
{
    public class ClientService : IClientService
    {
        protected readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public virtual async Task<ApiResponse<business_Customer>> ExecuteTransactionAsync(int customerId, decimal amount, string type)
        {
            try
            {
                if (amount <= 0)
                {
                    return ApiResponse<business_Customer>.ValidationError("Amount must be positive");
                }

                
                decimal finalAmount = (type == "Withdraw") ? -amount : amount;

                data_Customer updated = await _clientRepository.ProcessTransactionAsync(customerId, finalAmount, type);

                business_Customer result = MapperDataToBusiness(updated);
                return ApiResponse<business_Customer>.SuccessResponse(result, $"{type} processed successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<business_Customer>.ErrorResponse("Transaction failed", ex.Message);
            }
        }

        public virtual async Task<ApiResponse<IEnumerable<data_Operations>>> GetCustomerHistoryAsync(int customerId, int page, int size)
        {
            try
            {
                var history = await _clientRepository.GetHistoryAsync(customerId, page, size);

                return ApiResponse<IEnumerable<data_Operations>>.SuccessResponse(history, "History retrieved");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<data_Operations>>.ErrorResponse("Failed to load history", ex.Message);
            }
        }

        private static data_Customer MapperBusinessToData(business_Customer business_Customer)
        {
            return new data_Customer
            {
                CustomerId = business_Customer.CustomerId,
                Balance = business_Customer.Balance,
                Email = business_Customer.Email,
                FullName = business_Customer.FullName
            };
        }

        private static business_Customer MapperDataToBusiness(data_Customer data_Customer)
        {
            return new business_Customer
            {
                CustomerId = data_Customer.CustomerId,
                Balance = data_Customer.Balance,
                Email = data_Customer.Email,
                FullName = data_Customer.FullName
            };
        }
    }
}
