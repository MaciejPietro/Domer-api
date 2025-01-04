using Ardalis.Result;
using Domer.Application.Commands.Project.CreateProject;
using Domer.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Domer.Api.Configurations;

public static class MediatRSetup
{
    public static IServiceCollection AddMediatRSetup(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining(typeof(Application.IAssemblyMarker));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}