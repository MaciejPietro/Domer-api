using Domer.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Api.Common;


public sealed class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);
        
        
        
        ProblemDetails problemDetails = exception switch
        {
            NotFoundException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = exception.Message
            },
            BadRequestException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = exception.Message
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = exception.Message
            }
        };

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}

