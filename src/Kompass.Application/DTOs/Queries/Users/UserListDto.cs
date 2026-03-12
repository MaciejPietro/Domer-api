using System;
using System.Collections.Generic;

namespace Kompass.Application.DTOs.Queries.Users;

public class UserListDto
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}