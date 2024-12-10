using System.ComponentModel.DataAnnotations;

namespace Domer.Domain.Entities.Auth;

public class ResetPassword
{
    
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Token is required.")]
    public string Token { get; set; }
    
    [Required(ErrorMessage = "New password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}