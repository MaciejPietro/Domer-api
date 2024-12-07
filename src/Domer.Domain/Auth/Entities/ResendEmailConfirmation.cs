using System.ComponentModel.DataAnnotations;

namespace Domer.Domain.Auth.Entities;

public class ResendEmailConfirmation
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string Email { get; set; }
    public string ClientUri { get; set; }
}