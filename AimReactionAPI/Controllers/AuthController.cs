using AimReactionAPI.Data;
using AimReactionAPI.DTOs;
using AimReactionAPI.Models;
using AimReactionAPI.Services;
using AimReactionAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AimReactionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly AuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(AppDbContext context, AuthService authService, ILogger<AuthController> logger)
    {
        _context = context;
        _authService = authService;
        _logger = logger;
    }


    // Serilog, nlog?????
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto userDto)
    {
        try {
            if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
            {
                throw new UserAlreadyExistsException("Email is already registered.");
            }

            if (string.IsNullOrEmpty(userDto.Password))
            {
                throw new PasswordEmptyException("Password cannot be empty.");
            }

            var hashedPassword = _authService.HashPassword(userDto.Password);

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);

        } catch (UserAlreadyExistsException ex) {
            _logger.LogError(ex, "User Registration failed: {Email}",userDto.Email);
            return BadRequest(ex.Message);
        } catch (PasswordEmptyException ex) {
            _logger.LogError(ex, "Password is empty for email: {Email}", userDto.Email);
            return BadRequest(ex.Message);
        } catch (Exception ex) {
            _logger.LogError(ex, "Unexpected error during registration for email: {Email}", userDto.Email);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDto loginDto)
    {
        try {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                throw new UserNotFoundException("Invalid email or password");
            }

            var isValidPassword = _authService.VerifyPassword(loginDto.Password, user.PasswordHash);
            if (!isValidPassword)
            {
                throw new InvalidPasswordException("Invalid email or password");
            }

            return Ok(user.UserId);
        } catch (UserNotFoundException ex) {
            _logger.LogWarning(ex, "User not found during login attempt for email {Email}.", loginDto.Email);
            return Unauthorized(ex.Message);
        } catch (InvalidPasswordException ex) {
            _logger.LogWarning(ex, "Invalid password during login attempt for email {Email}.", loginDto.Email);
            return Unauthorized(ex.Message);
        } catch (Exception ex) {
            _logger.LogError(ex, "Unexpected error occurred during login for email {Email}.", loginDto.Email);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return user;
    }
}

