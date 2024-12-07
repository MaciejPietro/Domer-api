using Ardalis.Result;
using Domer.Application.Features.Heroes;
using Domer.Domain.Common;
using MediatR;

namespace Domer.Application.Heroes.GetHeroById;

public record GetHeroByIdRequest(HeroId Id) : IRequest<Result<GetHeroResponse>>;