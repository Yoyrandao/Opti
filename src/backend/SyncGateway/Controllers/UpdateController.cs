using Microsoft.AspNetCore.Mvc;

using SyncGateway.Contracts.Common;
using SyncGateway.Helpers;

namespace SyncGateway.Controllers
{
    [ApiController]
    public class UpdateController : Controller
    {
        [HttpPost]
        [Route(Routes.Fs.Update)]
        public IActionResult Update([FromBody] ChangeSet changeSet)
        {
            return Ok();
        }
    }
}
