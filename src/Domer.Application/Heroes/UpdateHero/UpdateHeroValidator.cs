using Domer.Application.Common;
using Domer.Application.Heroes.UpdateHero;
using FluentValidation;

namespace Domer.Application.Features.Heroes.UpdateHero;

public class UpdateHeroValidator : AbstractValidator<UpdateHeroRequest>
{
    public UpdateHeroValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.Individuality)
            .NotEmpty();

        RuleFor(x => x.HeroType)
            .IsInEnum();

        RuleFor(x => x.Age)
            .GreaterThan(0);
        
        RuleFor(x => x.Nickname)
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.Team)
            .MaximumLength(StringSizes.Max);
    }
}