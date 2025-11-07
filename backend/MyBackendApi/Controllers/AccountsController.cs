using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using MyBackendApi.DTO.Account;
using MyBackendApi.Services;

namespace MyBackendApi.Controllers
{
    [ApiController] // Tells ASP.NET Core this is part of an API, not mvc, affecting behavior
    // 1. automatic model binding - returns 400 if ModelState.IsValid == 0
    // 2. Automatic Request Binding - Framework infers where the data comes from, no [FromBody]
    // 3. Standardized responses for invalid input or missing data
    // 4. Improved parameter inference
    [Route("api/[controller]")] // Controller gets replaced by class name - create api route
    
    public class AccountsController : ControllerBase
    {

        private readonly IAccountService _service;
        private readonly ILogger<AccountsController> _logger; // Injects .NETs built in logger

        public AccountsController(IAccountService service, ILogger<AccountsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccountById(int id)
        {
            try
            {
                var account = await _service.GetAccountByIdAsync(id);
                if (account == null)
                {
                    _logger.LogWarning("No Accounts Found");
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception ex)
            {

                //Create a logger here to return message, dont expose message to api user
                _logger.LogError(ex, $"Error retreiving account with Message : {ex.Message}");
                return StatusCode(500, $"Internal Server error");
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAllAccounts()
        {
            try
            {
                var accounts = await _service.GetAllAccounts();
                if (accounts == null)
                {
                    _logger.LogWarning("No Accounts found");
                    return NotFound(); // return a 404 for null
                } 

                return Ok(accounts);
            }catch (Exception ex){
                return StatusCode(500, $"Internal Server error with mesage : {ex.Message}");
            }
        }
    }
}