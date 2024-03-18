using Datingapp.Data;
using Datingapp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Datingapp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : BasicApiController
{
    // Here we are using dependency injection to inject the datacontext into the controller so while constructor loading
    // we will get the datacontext here and use it in the controller
    private readonly DataContext _context;
    public UsersController(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// This method will return all the users we getting from database
    /// </summary>
    /// <returns>List of users </returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    /// <summary>
    /// This method is to find single user from the database 
    /// </summary>
    /// <param name="Id">Id of the user</param>
    /// <returns>Single user</returns>
    [HttpGet("{Id}")]
    public async Task<ActionResult<AppUser?>> GetUser(int Id)
    {
        return await _context.Users.FindAsync(Id);
    }
    
    
}