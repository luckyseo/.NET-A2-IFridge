using System.Data.Common;
using System.Dynamic;
using System.IO.Pipelines;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Backend.AppData;
using Backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("/[controller]")]
public class LoginController : ControllerBase
{
    private readonly AppDbContext _context; //underscore shows it's a private field
    public LoginController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet("check/{id}")]
    public async Task<ActionResult> CheckIdExists(string id)
    {
        var exists = await _context.Users.AnyAsync(u => u.id == id);

        return Ok(new { exitst = exists });
    }

    [HttpPost("auth")] //login/auth
    public async Task<ActionResult<User>> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.id == request.id && u.pw == request.pw);

        if (user == null)
        {
            return Unauthorized(new { message = "invalid ID/PW" });
        }

        return Ok(new
        {
            id = user.Id,
            firstName = user.firstName,
            lastName = user.lastName,
            preferredName = user.preferredName,

        });
    }

    [HttpPost("register")] //login/register
    public async Task<ActionResult<User>> Register([FromBody] RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.id == request.id))
        {
            return BadRequest(new { message = "Already Exist Id" });
        }

        var user = new User
        {
            id = request.id,
            pw = request.pw,
            firstName = request.firstName,
            lastName = request.lastName,
            Allergies = request.Allergies ?? new List<string>()
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            firstName = user.firstName
        });

    }

    //Instead of DTO
    public class LoginRequest
    {
        public string id { get; set; } = string.Empty;
        public string pw { get; set; } = string.Empty;
    }
    public class RegisterRequest
    {
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string preferredName { get; set; } = string.Empty;

        public string id { get; set; } = string.Empty;

        public string pw { get; set; } = string.Empty;
        public List<string>? Allergies { get; set; }

    }
}