using System;

namespace Domer.Domain.Auth.Entities;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

}