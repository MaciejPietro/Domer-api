﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Domer.Domain.Entities.User;

public class Delete
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}