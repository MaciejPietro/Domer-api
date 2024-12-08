﻿using Ardalis.Result;
using Domer.Application.Common;
using Domer.Application.Heroes;
using Domer.Application.Heroes.GetHeroById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Features.Heroes.GetHeroById;

public class GetHeroByIdHandler : IRequestHandler<GetHeroByIdRequest, Result<GetHeroResponse>>
{
    private readonly IContext _context;


    public GetHeroByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetHeroResponse>> Handle(GetHeroByIdRequest request, CancellationToken cancellationToken)
    {
        var hero = await _context.Heroes.FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        if (hero is null) return Result.NotFound();
        return Application.Heroes.Mapper.ToHeroDto(hero);
    }
}