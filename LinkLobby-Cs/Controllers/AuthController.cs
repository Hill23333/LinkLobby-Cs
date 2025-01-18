using LinkLobby.Helper;
using LinkLobby.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkLobby.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        private readonly IConfiguration _configuration;
        
        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            // 检测是否为空
            if (string.IsNullOrEmpty(loginRequest.identifier))
            {
                return BadRequest(new Error(400, "Invalid Identifier"));
            }

            // 检测是否符合格式
            if (!IdentifierHelper.Check(loginRequest.identifier))
            {
                return BadRequest(new Error(400, "Invalid Identifier"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, loginRequest.identifier),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(

                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: credentials
            );

            return Created("/api/auth/login", new
            {
                status = "201",
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                expire_in = "172800"
            });
        }
    }
}
