using Domer.Domain.Auth.Interfaces;
using Domer.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using System;

namespace Domer.Infrastructure;

public class ApplicationUser : IdentityUser<Guid>
{
    public override Guid Id { get; set; } = Guid.CreateVersion7();
}

public class ApplicationRole : IdentityRole<Guid>
{
    public override Guid Id { get; set; } = Guid.CreateVersion7();
}