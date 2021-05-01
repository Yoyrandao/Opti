using System.Threading.Tasks;

using CommonTypes.Contracts;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Serilog;

using SyncGateway.Contracts.Out;
using SyncGateway.Exceptions.Shields;
using SyncGateway.Helpers;
using SyncGateway.Services;

using Utils.Http;

namespace SyncGateway.Controllers
{
    [ApiController]
    public class UpdateController : Controller
    {
        public UpdateController(
            IUpdateUserStorageService     updateUserStorageService,
            IExceptionShield<ApiResponse> shield)
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
                _logger.Information($"Updating user FS triggered for {changeSet.Identity}.");

                Task.Run(() => _updateUserStorageService.Update(changeSet)).ContinueWith(
                    (_, state) =>
                    {
                        (state as ILogger)?.Information(
                            $"Successfully updated ({changeSet.Identity}).");
                    }, _logger);

                return new ApiResponse { Data = new Result { Success = true } };
            });

            return result.Error == null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route(Routes.Fs.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromBody] DeleteSet deleteSet)
        {
            var result = _shield.Protect(() =>
            {
                _logger.Information($"Deleting entity from FS for {deleteSet.Identity}");

                Task.Run(() => _updateUserStorageService.Delete(deleteSet)).ContinueWith(
                    (_, state) =>
                    {
                        (state as ILogger)?.Information(
                            $"Successfully deleted {deleteSet.Filename} ({deleteSet.Identity})");
                    }, _logger);

                return new ApiResponse { Data = new Result { Success = true } };
            });

            return result.Error == null ? Ok(result) : BadRequest(result);
        }

        private readonly IUpdateUserStorageService _updateUserStorageService;
        private readonly IExceptionShield<ApiResponse> _shield;

        private readonly ILogger _logger = Log.ForContext<UpdateController>();
    }
}