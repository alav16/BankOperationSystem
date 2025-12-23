using BankOper.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankOper.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }


        [HttpPost("ExecuteTransaction/{customerId}")]
        public async Task<ActionResult> ExecuteTransaction(int customerId, [FromQuery]decimal amount)
        {
            var result = await _clientService.ExecuteTransactionAsync(customerId, amount, "Deposit");
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("ExecuteTransaction1/{customerId}")]
        public async Task<ActionResult> ExecuteTransaction1(int customerId, [FromQuery]decimal amount)
        {
            var result = await _clientService.ExecuteTransactionAsync(customerId, amount, "Withdraw");
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("GetHistory/{customerId}")]
        public async Task<ActionResult> GetHistory(int customerId, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _clientService.GetCustomerHistoryAsync(customerId, pageNumber, pageSize);
            return StatusCode(result.StatusCode, result);
        }
    }


}
