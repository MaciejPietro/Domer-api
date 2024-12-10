using System;

namespace Domer.Application.DTOs.Queries;

public class UserDto
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public bool  IsEmailConfirmed { get; set; }
}