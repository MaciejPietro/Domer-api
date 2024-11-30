using Ardalis.Result;
using Domer.Domain.Entities.Common;
using MediatR;

namespace Domer.Application.Features.Heroes.GetHeroById;

public record GetHeroByIdRequest(HeroId Id) : IRequest<Result<GetHeroResponse>>;