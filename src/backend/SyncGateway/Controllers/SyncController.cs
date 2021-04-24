using System.Linq;

using AutoMapper;

using CommonTypes.Contracts;

using DataAccess.Repositories;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Serilog;

using SyncGateway.Exceptions.Shields;
using SyncGateway.Helpers;

using Utils.Http;

namespace SyncGateway.Controllers
{
    [ApiController]
    public class SyncController : Controller
    {
        public SyncController(
            IExceptionShield<ApiResponse> shield, IFilePartRepository filePartRepository, IMapper mapper)
        {
            _shield = shield;
            _filePartRepository = filePartRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(Routes.User.ResourceState)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetState([FromQuery] string filename)
        {
            var result = _shield.Protect(() =>
            {
                _logger.Information($"Retrieving infromation about file ({filename}).");

                var fileState = _filePartRepository.GetFileByName(filename).Select(fp => _mapper.Map<FileState>(fp));

                return new ApiResponse { Data = fileState };
            });

            return result.Error == null ? Ok(result) : BadRequest(result);
        }

        private readonly IExceptionShield<ApiResponse> _shield;
        private readonly IFilePartRepository _filePartRepository;
        private readonly IMapper _mapper;

        private readonly ILogger _logger = Log.ForContext<SyncController>();
    }
}