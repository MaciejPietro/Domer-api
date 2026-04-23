using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Kompass.Api.Common;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => new OkObjectResult(result.Value),
            ResultStatus.NotFound => new NotFoundObjectResult(new
            {
                message = result.Errors.Any()
                    ? string.Join(", ", result.Errors)
                    : "Resource not found"
            }),
            ResultStatus.Invalid => new BadRequestObjectResult(new
            {
                errors = result.ValidationErrors
            }),
            ResultStatus.Unauthorized => new UnauthorizedObjectResult(new
            {
                message = "Unauthorized"
            }),
            ResultStatus.Forbidden => new ObjectResult(new { message = "Forbidden" })
            {
                StatusCode = 403
            },
            ResultStatus.Error => new ObjectResult(new
            {
                message = result.Errors.Any()
                    ? string.Join(", ", result.Errors)
                    : "An error occurred"
            })
            {
                StatusCode = 500
            },
            _ => new ObjectResult(new { message = "An unexpected error occurred" })
            {
                StatusCode = 500
            }
        };
    }
}
