﻿using Domer.Domain.Heroes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Common;

public interface IContext : IAsyncDisposable, IDisposable
{
    public DatabaseFacade Database { get; }
    
    public DbSet<Hero> Heroes { get; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}