using System.Security.Cryptography.Pkcs;
using BankOper.Business.Models;
using BankOper.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankOper.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public BusinessController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("CreateCustomer")]
        public async Task<ActionResult<ApiResponse<int>>> CreateCustomer(business_Customer business_Customer)
        {
            ApiResponse<int> id = await _customerService.CreateAsync(business_Customer);
            return StatusCode(id.StatusCode, id);
        }

        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<ApiResponse<IEnumerable<business_Customer>>>> GetAllCustomers
            ([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            ApiResponse<IEnumerable<business_Customer>> customers = await _customerService.GetAllAsync(pageNumber, pageSize);
            return StatusCode(customers.StatusCode, customers);
        }

        [HttpGet("GetByIdCustomer/{id}")]
        public async Task<ActionResult<ApiResponse<business_Customer?>>> GetByIdCustomer(int id)
        {
            ApiResponse<business_Customer?> customer = await _customerService.GetByIdAsync(id);
            return StatusCode(customer.StatusCode, customer);
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAsync(int id)
        {
            ApiResponse<bool> deleted = await _customerService.DeleteAsync(id);
            return StatusCode(deleted.StatusCode, deleted);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<business_Customer>>> UpdateCustomer(business_Customer business_Customer)
        {
            var updated = await _customerService.UpdateAsync(business_Customer);
            return StatusCode(updated.StatusCode, updated);
        }
    }
}
