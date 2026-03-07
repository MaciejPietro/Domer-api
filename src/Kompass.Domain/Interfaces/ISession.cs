using Kompass.Domain.Common;
using System;

namespace Kompass.Domain.Interfaces;


public interface ISession
{
    public UserId UserId { get; }

    public DateTime Now { get; }
}