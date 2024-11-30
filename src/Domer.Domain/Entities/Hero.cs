﻿using Domer.Domain.Entities.Common;
using Domer.Domain.Entities.Enums;
using System;

namespace Domer.Domain.Entities;

public class Hero : Entity<HeroId>
{
    public override HeroId Id { get; set; } = Guid.CreateVersion7();
    public string Name { get; set; } = null!;

    public string? Nickname { get; set; }
    public string? Individuality { get; set; } = null!;
    public int? Age { get; set; }

    public HeroType HeroType { get; set; }

    public string? Team { get; set; }
}