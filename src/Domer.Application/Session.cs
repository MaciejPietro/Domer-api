using Domer.Domain.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using ISession = Domer.Domain.Interfaces.ISession;


namespace Domer.Application;

public class Session : ISession
{
    public UserId UserId { get; private init; }

    public DateTime Now => DateTime.Now;

    public Session(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        var nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);

        if(nameIdentifier != null)
        {
            UserId = new Guid(nameIdentifier.Value);
        }
    }

}