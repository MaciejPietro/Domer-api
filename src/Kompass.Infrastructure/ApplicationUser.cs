
using Kompass.Domain.Interfaces;
using Kompass.Domain.Interfaces.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kompass.Infrastructure;

public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
{
    public override Guid Id { get; set; } = Guid.CreateVersion7();


    [NotMapped]
    public IList<string> Roles { get; set; } = new List<string>();
}


public class ApplicationRole : IdentityRole<Guid>
{
    public override Guid Id { get; set; } = Guid.CreateVersion7();
}