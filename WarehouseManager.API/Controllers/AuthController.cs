using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WarehouseManager.API.Data;
using WarehouseManager.API.DTOs;
using WarehouseManager.API.Models;
using WarehouseManager.API.Services.Interfaces;
using WarehouseManager.API.Utility;

namespace WarehouseManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(UserContext userContext, ITokenService tokenService, ILogger<AuthController> logger) : ControllerBase
{
    private readonly UserContext _userContext = userContext;
    private readonly ITokenService _tokenService = tokenService;
    private readonly ILogger<AuthController> _logger = logger;

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDTO loginRequest)
    {
        if (loginRequest == null)
        {
            return BadRequest("Invalid client request.");
        }

        var user = _userContext.Users.FirstOrDefault(u => u.UserName == loginRequest.UserName);

        if (user == null || user.Password == null || !PasswordHasher.VerifyPassword(loginRequest.Password, user.Password))
        {
            _logger.LogWarning($"Failed login attempt for user: {loginRequest.UserName}");
            return Unauthorized("Invalid username or password.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        _userContext.SaveChanges();

        return Ok(new TokenResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequestDTO registerRequest)
    {
        if (registerRequest == null)
        {
            return BadRequest("Invalid client request.");
        }

        if (_userContext.Users.Any(u => u.UserName == registerRequest.UserName))
        {
            return Conflict("User already exists.");
        }

        if (!IsValidPassword(registerRequest.Password))
        {
            return BadRequest("Password does not meet complexity requirements.");
        }

        var hashedPassword = PasswordHasher.HashPassword(registerRequest.Password);

        var user = new User
        {
            UserName = registerRequest.UserName,
            Password = hashedPassword,
            Role = registerRequest.Role ?? "User",
            RefreshToken = string.Empty,
            RefreshTokenExpiryTime = DateTime.MinValue
        };

        _userContext.Users.Add(user);
        _userContext.SaveChanges();

        return Ok(new { UserId = user.Id, Message = "User registered successfully." });
    }

    private bool IsValidPassword(string password)
    {
        return password.Length >= 8
               && password.Any(char.IsUpper)
               && password.Any(char.IsDigit);
    }
}