using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Domer.Domain.Auth.Entities;

public class UpdateUser
{
    public string Id { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    public string? ClientUri { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [DataType(DataType.Password)]
    public string? CurrentPassword { get; set; }
}