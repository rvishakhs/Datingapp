using System.Security.Cryptography;
using System.Text;
using Datingapp.Data;
using Datingapp.DTO;
using Datingapp.Entities;
using Datingapp.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Datingapp.Controllers;

public class AccountController : BasicApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
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

        return new UserDTO
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(user => user.UserName == loginDto.Username.ToLower());
        
        if (user == null ) return Unauthorized("Invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }

        return new UserDTO
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        }; 
    }

    private async Task<bool> UserExists(string username)
    {
       return await _context.Users.AnyAsync(user => user.UserName == username.ToLower());
    }
}