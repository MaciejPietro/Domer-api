using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Common;

public interface IContext : IAsyncDisposable, IDisposable
{
    public DatabaseFacade Database { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}