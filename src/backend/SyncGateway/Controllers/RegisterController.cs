using DataAccess.Domain;
using DataAccess.Repositories;

using Microsoft.AspNetCore.Mvc;

using SyncGateway.Processors;

namespace SyncGateway.Controllers
{
    [Route("/register")]
    [ApiController]
    public class RegisterController : Controller
    {
        public RegisterController(IFileProcessor repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return Ok(_repository.GetFilesFromFolder("folder"));
        }

        private readonly IFileProcessor _repository;
    }
}