
using Kompass.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Kompass.Infrastructure;

public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
{
    public override Guid Id { get; set; } = Guid.CreateVersion7();

    // Ensure properties from IApplicationUser are properly implemented
    public string Email 
    { 
        get => base.Email; 
        set => base.Email = value; 
    }

    string IApplicationUser.UserName 
    { 
        get => base.UserName; 
        set => base.UserName = value; 
    }
}


public class ApplicationRole : IdentityRole<Guid>
{
    public override Guid Id { get; set; } = Guid.CreateVersion7();
}