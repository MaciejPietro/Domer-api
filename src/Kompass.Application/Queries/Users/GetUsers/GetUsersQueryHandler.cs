using AutoMapper;
using Kompass.Application.Common.Interfaces;
using Kompass.Application.Common.Responses;
using Kompass.Application.DTOs.Queries.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Queries.Users.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedResponse<UserListDto>>
{

    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<UserListDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        
        var allUsers = await _identityService.GetAllUsersAsync();

        

        var page = request.Page > 0 ? request.Page : 1;
        var perPage = request.PerPage > 0 ? request.PerPage : 10;

        var totalItems = allUsers.Count;
        var totalPages = (int)Math.Ceiling(totalItems / (double)perPage);

        var paginatedUsers = allUsers
            .Skip((page - 1) * perPage)
            .Take(perPage)
            .ToList();

        var mappedUsers = _mapper.Map<List<UserListDto>>(paginatedUsers);

        return new PaginatedResponse<UserListDto>
        {
            CurrentPage = page,
            TotalPages = totalPages,
            TotalItems = totalItems,
            Results = mappedUsers
        };
    }
}