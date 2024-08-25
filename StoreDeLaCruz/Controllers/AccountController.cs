using Microsoft.AspNetCore.Mvc;
using StoreDeLaCruz.Core.Aplication.DTOs.Account;
using StoreDeLaCruz.Core.Aplication.Interfaces.Service;

namespace StoreDeLaCruz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request )
        {
            return Ok(await _accountService.AuthenticationAsync(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterBasicUserAsync(request,origin));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> RegisterAsync([FromQuery] string userId, [FromQuery] string token)
        {
            return Ok(await _accountService.ConfirmAccountAsync(userId, token));
        }
    }
}
