using Microsoft.AspNetCore.Identity;
using System;

namespace Domer.Infrastructure;

public class ApplicationUser : IdentityUser<Guid>
{
    // public override string? UserName { get; set; }
    public override Guid Id { get; set; } = Guid.CreateVersion7();
}

public class ApplicationRole : IdentityRole<Guid>
{
    public override Guid Id { get; set; } = Guid.CreateVersion7();
}