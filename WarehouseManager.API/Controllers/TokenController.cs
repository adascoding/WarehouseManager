using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.API.Data;
using WarehouseManager.API.DTOs;
using WarehouseManager.API.Services.Interfaces;

namespace WarehouseManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController(UserContext userContext, ITokenService tokenService, ILogger<TokenController> logger) : ControllerBase
    {
        private readonly UserContext _userContext = userContext;
        private readonly ITokenService _tokenService = tokenService;
        private readonly ILogger<TokenController> _logger = logger;

        [HttpPost("refresh")]
        public IActionResult Refresh(TokenRequestDTO tokens)
        {
            if (tokens == null)
                return BadRequest("Invalid client request.");

            string accessToken = tokens.AccessToken;
            string refreshToken = tokens.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                _logger.LogWarning("Invalid access token.");
                return BadRequest("Invalid access token.");
            }

            var username = principal.Identity.Name;
            var user = _userContext.Users.SingleOrDefault(u => u.UserName == username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                _logger.LogWarning($"Invalid refresh token for user: {username}");
                return BadRequest("Invalid client request.");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            _userContext.SaveChanges();

            return Ok(new TokenResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var user = _userContext.Users.SingleOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            user.RefreshToken = null;
            _userContext.SaveChanges();

            _logger.LogInformation($"Refresh token revoked for user: {username}");

            return NoContent();
        }
    }
}