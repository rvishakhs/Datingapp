using Datingapp.Data;
using Datingapp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Datingapp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
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
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        var users = _context.Users.ToList();
        return users;
    }

    /// <summary>
    /// This method is to find single user from the database 
    /// </summary>
    /// <param name="Id">Id of the user</param>
    /// <returns>Single user</returns>
    [HttpGet("{Id}")]
    public ActionResult<AppUser?> GetUser(int Id)
    {
        return _context.Users.Find(Id);
    }
    
    
}