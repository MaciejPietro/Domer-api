using System;

namespace Domer.Application.Common.Exceptions;

public class InternalException : Exception
{
    public InternalException() : base()
    {

    }

    public InternalException(string message) : base(message)
    {

    }

    public InternalException(string message, Exception exp) : base(message, exp)
    {

    }
}