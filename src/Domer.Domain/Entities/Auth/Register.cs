using Domer.Domain.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domer.Domain.Entities.Auth;

public class Register
{
    
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public string? ClientUri { get; set; }

}