using api1.Data;
using api1.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api1.Controllers;
[Route("api/[Controller]")]
[ApiController]

    public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    public UserController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{EmailAddress}")]
    public async Task<IActionResult> GetUser(string EmailAddress)
    {
        var user = await _context.Users.Where(
            k => k.EmailAddress == EmailAddress).Select(
            k => new
            {
                k.Id,
                k.Username,
                k.Password,
                k.EmailAddress,
                k.ProfilePictureUrl
            }
            ).ToListAsync();
        return Ok(user);
    }

    
    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {var existingUser = await _context.Users.FirstOrDefaultAsync(
        u=> u.EmailAddress == user.EmailAddress
        );
        if (existingUser != null) 
        {
            return BadRequest("Bu e-posta adresi zaten kayıtlı.");
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsers), new {id = user.Id},user);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == request.Email && u.Password == request.Password);
        if(user == null)
        {
            return Unauthorized("Geçersiz e-posta veya şifre.");
        }
        return Ok(new {message = "Giriş Başarılı!" , user });
    }
}
public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

}
