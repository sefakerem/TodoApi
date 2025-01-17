using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.DTOs;
using TodoAPI.Models;
using TodoAPI.Services;

namespace TodoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthService _authService;

    public AuthController(ApplicationDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register(RegisterDto request)
    {
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            return BadRequest("Username already exists");

        _authService.CreatePasswordHash(request.Password,
            out byte[] passwordHash,
            out byte[] passwordSalt);

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _authService.CreateToken(user);

        return Ok(new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Token = token
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponseDto>> Login(LoginDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null)
            return BadRequest("User not found");

        if (!_authService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            return BadRequest("Wrong password");

        var token = _authService.CreateToken(user);

        return Ok(new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Token = token
        });
    }
}
