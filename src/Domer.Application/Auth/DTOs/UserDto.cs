using System;

namespace Domer.Application.Auth.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public bool  IsEmailConfirmed { get; set; }
}