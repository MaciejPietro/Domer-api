using System.ComponentModel.DataAnnotations;

namespace Domer.Domain.Entities.Auth;

public class ResetPasswordLink
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string ClientUri { get; set; }

}