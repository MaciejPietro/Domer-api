using System;
using System.ComponentModel.DataAnnotations;

namespace Domer.Domain.Auth.Entities;

public class UpdateUser
{
    public string Id { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }


}