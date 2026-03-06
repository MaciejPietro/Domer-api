using Ardalis.Result;
using Kompass.Application.Commands.Project.CreateProject;
using Kompass.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Kompass.Api.Configurations;

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