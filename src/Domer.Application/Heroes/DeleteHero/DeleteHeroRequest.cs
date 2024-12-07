using Ardalis.Result;
using Domer.Domain.Common;
using MediatR;

namespace Domer.Application.Heroes.DeleteHero;

public record DeleteHeroRequest(HeroId Id) : IRequest<Result>;