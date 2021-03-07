using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Serilog;

using SyncGateway.Contracts.In;
using SyncGateway.Contracts.Out;
using SyncGateway.Exceptions.Shields;
using SyncGateway.Helpers;
using SyncGateway.Services;

namespace SyncGateway.Controllers
{
    [ApiController]
    public class RegisterController : Controller
    {
        public RegisterController(IUserRegistrationService registrationService, IExceptionShield<ApiResponse> shield)
        {
            _shield = shield;
            _registrationService = registrationService;
        }

        [HttpPost]
        [Route(Routes.User.Registration)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody] RegistrationContract contract)
        {
            var result = _shield.Protect(() =>
            {
                _logger.Information($"Registration triggered for {contract.Username}.");
                
                _registrationService.Register(contract);
                
                _logger.Information($"Successfully registered ({contract.Username}).");

                return new ApiResponse { Data = new Result { Success = true } };
            });

            return result.Error == null ? Ok(result) : BadRequest(result);
        }

        private readonly IExceptionShield<ApiResponse> _shield;
        private readonly IUserRegistrationService _registrationService;

        private readonly ILogger _logger = Log.ForContext<RegisterController>();
    }
}