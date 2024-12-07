using Ardalis.Result.AspNetCore;
using Domer.Application.Heroes.CreateHero;
using Domer.Application.Heroes.DeleteHero;
using Domer.Application.Heroes.GetAllHeroes;
using Domer.Application.Heroes.GetHeroById;
using Domer.Application.Heroes.UpdateHero;
using Domer.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Domer.Api.Endpoints;

public static class HeroEndpoints
{
    public static void MapHeroEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/Hero")
            .WithTags("Hero");
        
        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllHeroesRequest request) =>
        {
            var result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, HeroId id) =>
        {
            var result = await mediator.Send(new GetHeroByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", async (IMediator mediator, CreateHeroRequest request) =>
        {
            var result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, HeroId id, UpdateHeroRequest request) =>
        {
            var result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", async (IMediator mediator, HeroId id) =>
        {
            var result = await mediator.Send(new DeleteHeroRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}