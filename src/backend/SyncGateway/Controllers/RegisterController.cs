using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SyncGateway.Contracts.In;
using SyncGateway.Contracts.Out;
using SyncGateway.Exceptions.Shields;
using SyncGateway.Services;

namespace SyncGateway.Controllers
{
    [Route("/register")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly IUserRegistrationService _registrationService;
        private readonly IExceptionShield<ApiResponse> _shield;

        public RegisterController(IUserRegistrationService registrationService, IExceptionShield<ApiResponse> shield)
        {
            _shield = shield;
            _registrationService = registrationService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody] RegistrationContract contract)
        {
            var result = _shield.Protect(() =>
            {
                _registrationService.Register(contract);

                return new ApiResponse { Data = new RegistrationResult { Success = true } };
            });

            return result.Error == null ? Ok(result) : BadRequest(result);
        }
    }
}
