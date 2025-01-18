using LinkLobby.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkLobby.Controllers
{
    [Route("api/lobbies")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private readonly ILogger<LobbyController> _logger;
        public LobbyController(ILogger<LobbyController> logger)
        {
            _logger = logger;
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult Create(LobbyCreateRequest createRequest)
        {
            // 检测是否为空
            if (string.IsNullOrEmpty(createRequest.username))
            {
                return BadRequest(new Error(400));
            }

            // 没写完...
            return Ok();
        }
    }
}
