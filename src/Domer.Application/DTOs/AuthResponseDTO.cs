using System;

namespace Domer.Application.DTOs;

public class AuthResponseDTO
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public bool  IsEmailConfirmed { get; set; }
}