using System;
using System.Collections.Generic;

namespace Kompass.Domain.Interfaces.Users;

public interface IApplicationUser
{
    Guid Id { get; set; }
    string Email { get; set; }
    string UserName { get; set; }
    bool EmailConfirmed { get; set; }
    IList<string> Roles { get; set; }
}