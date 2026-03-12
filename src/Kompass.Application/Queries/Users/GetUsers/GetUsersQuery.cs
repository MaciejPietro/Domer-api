using Kompass.Application.Common.Responses;
using Kompass.Application.DTOs.Queries.Users;
using MediatR;

namespace Kompass.Application.Queries.Users.GetUsers;

public class GetUsersQuery: IRequest<PaginatedResponse<UserListDto>>
{
    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 10;
}