using Datingapp.Entities;

namespace Datingapp.Interface;

public interface ITokenService
{
    /// <summary>
    /// This is an interface to create a user token
    /// </summary>
    /// <param name="user">User details </param>
    /// <returns>Token</returns>
    string CreateToken(AppUser user);
}