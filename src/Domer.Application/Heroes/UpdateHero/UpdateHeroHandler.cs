﻿using Ardalis.Result;
using Domer.Application.Common;
using Domer.Application.Heroes;
using Domer.Application.Heroes.UpdateHero;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Features.Heroes.UpdateHero;

public class UpdateHeroHandler : IRequestHandler<UpdateHeroRequest, Result<GetHeroResponse>>
{
    private readonly IContext _context;

    public UpdateHeroHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetHeroResponse>> Handle(UpdateHeroRequest request,
        CancellationToken cancellationToken)
    {
        var originalHero = await _context.Heroes
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalHero == null) return Result.NotFound();

        originalHero.Name = request.Name;
        originalHero.Nickname = request.Nickname;
        originalHero.Team = request.Team;
        originalHero.Individuality = request.Individuality;
        originalHero.Age = request.Age;
        originalHero.HeroType = request.HeroType;
        _context.Heroes.Update(originalHero);
        await _context.SaveChangesAsync(cancellationToken);
        return Application.Heroes.Mapper.ToHeroDto(originalHero);
    }
}