using System.Security.Cryptography;
using System.Text;
using Datingapp.Data;
using Datingapp.DTO;
using Datingapp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Datingapp.Controllers;

public class AccountController : BasicApiController
{
    private readonly DataContext _context;

    public AccountController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDto)
    {

        if (await UserExists(registerDto.Username))
        {
            return BadRequest("Username is taken");
        }
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    private async Task<bool> UserExists(string username)
    {
       return await _context.Users.AnyAsync(user => user.UserName == username.ToLower());
    }
}