using Domer.Domain.Common;
using System;

namespace Domer.Domain.Interfaces;


public interface ISession
{
    public UserId UserId { get; }

    public DateTime Now { get; }
}