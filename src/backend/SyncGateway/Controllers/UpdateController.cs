using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SyncGateway.Contracts.Common;
using SyncGateway.Contracts.Out;
using SyncGateway.Exceptions.Shields;
using SyncGateway.Helpers;
using SyncGateway.Services;

namespace SyncGateway.Controllers
{
    [ApiController]
    public class UpdateController : Controller
    {
        public UpdateController(IUpdateUserStorageService updateUserStorageService, IExceptionShield<ApiResponse> shield)
        {
            _shield = shield;
            _updateUserStorageService = updateUserStorageService;
        }
        
        [HttpPost]
        [Route(Routes.Fs.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] ChangeSet changeSet)
        {
            var result = _shield.Protect(() =>
            {
                _updateUserStorageService.Update(changeSet);
                
                return new ApiResponse { Data = new RegistrationResult { Success = true } };
            });
            
            return result.Error == null ? Ok(result) : BadRequest(result);
        }

        private readonly IUpdateUserStorageService _updateUserStorageService;
        private readonly IExceptionShield<ApiResponse> _shield;
    }
}
