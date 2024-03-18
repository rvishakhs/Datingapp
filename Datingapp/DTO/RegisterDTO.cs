using System.ComponentModel.DataAnnotations;

namespace Datingapp.DTO;

public class RegisterDTO 
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}