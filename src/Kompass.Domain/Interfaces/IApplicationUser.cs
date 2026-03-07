using System;

namespace Kompass.Domain.Interfaces;

public interface IApplicationUser
{
    Guid Id { get; set; }
    string Email { get; set; }
    string UserName { get; set; }
    bool EmailConfirmed { get; set; }
}