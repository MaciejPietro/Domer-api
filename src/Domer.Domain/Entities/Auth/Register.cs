using Domer.Domain.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domer.Domain.Entities.Auth;

public class Register
{
    public string? Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}