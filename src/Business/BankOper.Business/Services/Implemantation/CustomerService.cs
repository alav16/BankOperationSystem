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
    public class CustomerService : ICustomerService
    {
        protected readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public virtual async Task<ApiResponse<int>> CreateAsync(business_Customer business_Customer)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(business_Customer.FullName))
                {
                    return ApiResponse<int>.ValidationError("FullName is required");
                }

                if (string.IsNullOrWhiteSpace(business_Customer.Email))
                {
                    return ApiResponse<int>.ValidationError("Email is required");
                }

                data_Customer customer = MapperBusinessToData(business_Customer);

                var id = await _customerRepository.CreateAsync(customer);

                return ApiResponse<int>.SuccessResponse(id);
            }
            catch (Exception ex)
            {
                return ApiResponse<int>.ErrorResponse("Failed to create customer", ex.Message);
            }

        }

        public virtual async Task<ApiResponse<IEnumerable<business_Customer>>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber <= 0)
                {
                    return ApiResponse<IEnumerable<business_Customer>>.ValidationError("PageNumber must be possitive");
                }

                if (pageSize <= 0)
                {
                    return ApiResponse<IEnumerable<business_Customer>>.ValidationError("PageSize must be possitive");
                }
                IEnumerable<data_Customer> authors = await _customerRepository.GetAllAsync(pageNumber, pageSize);

                IEnumerable<business_Customer> businessCustomers = authors.Select(MapperDataToBusiness);

                return ApiResponse<IEnumerable<business_Customer>>.SuccessResponse(businessCustomers, "Customers retrieved successfully");

            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<business_Customer>>.ErrorResponse("Failed to load authors", ex.Message);
            }
        }

        public virtual async Task<ApiResponse<business_Customer?>> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ApiResponse<business_Customer?>.ValidationError("Is must be possitive");
                }

                data_Customer? customer = await _customerRepository.GetByIdAsync(id);

                if (customer == null)
                {
                    return ApiResponse<business_Customer?>.NotFoundResponse("Customer not found");
                }

                business_Customer businessCustomer = MapperDataToBusiness(customer);

                return ApiResponse<business_Customer?>.SuccessResponse(businessCustomer);
            }
            catch (Exception ex)
            {
                return ApiResponse<business_Customer?>.ErrorResponse("Failed to load customer", ex.Message);
            }

        }

        public virtual async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ApiResponse<bool>.ValidationError("Id must be possitive");
                }

                bool deleted = await _customerRepository .DeleteAsync(id);

                if (!deleted)
                {
                    return ApiResponse<bool>.NotFoundResponse("Author not found");
                }

                return ApiResponse<bool>.SuccessResponse(deleted, "Custoemr deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Failed to delete Custoemr", ex.Message);
            }
        }

        public virtual async Task<ApiResponse<business_Customer>> UpdateAsync(business_Customer business_Customer)
        {
            try
            {
                if (business_Customer.CustomerId <= 0)
                {
                    return ApiResponse<business_Customer>.ValidationError("Invalid Id");
                }

                data_Customer customer = MapperBusinessToData(business_Customer);

                data_Customer updatedCustomer = await _customerRepository.UpdateAsync(customer);

                if (updatedCustomer == null)
                {
                    return ApiResponse<business_Customer>.NotFoundResponse("Customer not found for update");
                }

                business_Customer businessCustomer = MapperDataToBusiness(updatedCustomer);

                return ApiResponse<business_Customer>.SuccessResponse(businessCustomer, "Customer updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<business_Customer>.ErrorResponse("Failed to update customer", ex.Message);
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
