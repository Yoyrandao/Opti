using DataAccess.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace SyncGateway.Controllers
{
    [Route("/register")]
    [ApiController]
    public class RegisterController : Controller
    {
        public RegisterController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return Ok(_userRepository.GetById(1));
        }

        private readonly IUserRepository _userRepository;
    }
}