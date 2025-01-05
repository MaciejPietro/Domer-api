using Domer.Application.Common.Responses;
using Domer.Application.Features.Heroes;
using Domer.Domain.Heroes.Enums;
using MediatR;

namespace Domer.Application.Heroes.GetAllHeroes;

public record GetAllHeroesRequest
    (string? Name = null, string? Nickname = null, int? Age = null, string? Individuality = null, HeroType? HeroType = null, string? Team = null, int CurrentPage = 1, int PageSize = 15) : IRequest<PaginatedResponse<GetHeroResponse>>;