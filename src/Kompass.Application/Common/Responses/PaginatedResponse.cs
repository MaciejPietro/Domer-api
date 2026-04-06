using System.Collections.Generic;

namespace Kompass.Application.Common.Responses;

public record PaginatedResponse<T>
{
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int TotalItems { get; init; }

    public List<T> Results { get; init; } = new List<T>();
}