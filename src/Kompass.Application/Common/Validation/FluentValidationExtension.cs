using FluentValidation;
using System;

namespace Kompass.Application.Common.Validation;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, object?> MustBeGuidObject<T>(
        this IRuleBuilder<T, object?> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage($"'{{PropertyName}}' is required")
            .Must(value => Guid.TryParse(value?.ToString(), out _))
            .WithMessage(value =>
                $"'{{PropertyName}}' must be a valid Guid");
    }
}