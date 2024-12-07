using System;

namespace Domer.Domain.Auth.Interfaces;

public interface IApplicationUser
{
    Guid Id { get; set; }
    string Email { get; set; }
    string UserName { get; set; }
}