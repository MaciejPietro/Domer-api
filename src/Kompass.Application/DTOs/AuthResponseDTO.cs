using System;
using System.Collections.Generic;

namespace Kompass.Application.DTOs;

public class AuthResponseDTO
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public bool  EmailConfirmed { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}