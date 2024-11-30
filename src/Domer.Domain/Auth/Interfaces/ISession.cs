using Domer.Domain.Entities.Common;
using System;

namespace Domer.Domain.Auth.Interfaces;

public interface ISession
{
    public UserId UserId { get; }

    public DateTime Now { get; }
}