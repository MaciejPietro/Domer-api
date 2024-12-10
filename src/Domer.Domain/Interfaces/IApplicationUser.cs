using System;

namespace Domer.Domain.Interfaces;

public interface IApplicationUser
{
    Guid Id { get; set; }
    string Email { get; set; }
    string UserName { get; set; }
    bool EmailConfirmed { get; set; }
}