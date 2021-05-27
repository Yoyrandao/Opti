using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public class InfoController : Controller
    {
        public InfoController(IExceptionShield<ApiResponse> shield, IFileInfoService fileInfoService)
        {
            _shield = shield;
            _fileInfoService = fileInfoService;
        }
        
        [HttpGet]
        [Route(Routes.Info.FileSize)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetFileSize([FromQuery] string filename)
        {
            var result = _shield.Protect(() =>
            {
                _logger.Information($"Size info triggered for {filename}.");

                var size = _fileInfoService.GetSize(filename, "aaron");
                
                _logger.Information($"Successfully gathered info ({filename}).");

                return new ApiResponse { Data = size };
            });

            return result.Error == null ? Ok(result) : BadRequest(result);
        }
        
        private readonly IExceptionShield<ApiResponse> _shield;
        private readonly IFileInfoService _fileInfoService;

        private readonly ILogger _logger = Log.ForContext<InfoController>();
    }
}