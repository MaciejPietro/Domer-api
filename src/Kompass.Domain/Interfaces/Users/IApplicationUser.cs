using System;
using System.Collections.Generic;

namespace Kompass.Domain.Interfaces.Users;

public interface IApplicationUser
{
    Guid Id { get;  }
    string Email { get;  }
    string UserName { get; set; }
    bool EmailConfirmed { get;  }
    IList<string> Roles { get;  }
}