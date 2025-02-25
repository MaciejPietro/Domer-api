﻿using Domer.Application.Common;
using Domer.Application.Common.Responses;
using Domer.Application.Extensions;
using Domer.Application.Heroes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Heroes.GetAllHeroes;

public class GetAllHeroesHandler : IRequestHandler<GetAllHeroesRequest, PaginatedResponse<GetHeroResponse>>
{
    private readonly IContext _context;
    
    public GetAllHeroesHandler(IContext context)
    {
        _context = context;
    }
    public async Task<PaginatedResponse<GetHeroResponse>> Handle(GetAllHeroesRequest request, CancellationToken cancellationToken)
    {
        var heroes = _context.Heroes
            .WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name, $"%{request.Name}%"))
            .WhereIf(!string.IsNullOrEmpty(request.Nickname), x => EF.Functions.Like(x.Nickname!, $"%{request.Nickname}%"))
            .WhereIf(request.Age != null, x => x.Age == request.Age)
            .WhereIf(request.HeroType != null, x => x.HeroType == request.HeroType)
            .WhereIf(!string.IsNullOrEmpty(request.Team), x => x.Team == request.Team)
            .WhereIf(!string.IsNullOrEmpty(request.Individuality), x => EF.Functions.Like(x.Individuality!, $"%{request.Individuality}%"));
        return await heroes.ProjectToResponse()
            .OrderBy(x => x.Name)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}